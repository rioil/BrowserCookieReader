using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrowserCookieReader.Readers.Chrome
{
    internal static class CookieDecryptionUtility
    {
        private static readonly byte[] AesHeadBytes = Encoding.ASCII.GetBytes("v10");   // TODO v12の場合があるという情報の確認
        private static readonly byte[] DpapiHeadBytes = new byte[] { 0x01, 0x00, 0x00, 0x00 };

        private static readonly string LocalStateFilePath
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Appdata/Local/Google/Chrome/User Data/Local State");

        public static string DecryptValue(byte[] valueBytes)
        {
            switch (GetEncryptionType(valueBytes))
            {
                case EEncryptionType.AES:
                    return DecryptWithAES(valueBytes);
                case EEncryptionType.DPAPI:
                    throw new NotSupportedException("Decryption of data encrypted with DPAPI is no longer supported.");
                default:
                    throw new NotSupportedException();
            }
        }

        private static string DecryptWithAES(byte[] valueBytes)
        {
            var nonce = valueBytes.AsSpan(3, 12);

            using var localStateFile = File.OpenRead(LocalStateFilePath);
            var localState = JsonSerializer.Deserialize<LocalState>(localStateFile);

            if (localState is null) { throw new Exception(); }  // TODO

            var encryptedKey = Convert.FromBase64String(localState.OsCrypt.EncryptedKey).Skip(5).ToArray();
            var key = ProtectedData.Unprotect(encryptedKey, null, DataProtectionScope.LocalMachine);
            var ciphertext = valueBytes.AsSpan()[15..^16];
            var authTag = valueBytes.AsSpan()[^16..];

            var aes = new AesGcm(key);
            var plaintext = new byte[ciphertext.Length];
            aes.Decrypt(nonce, ciphertext, authTag, plaintext);

            return Encoding.UTF8.GetString(plaintext);
        }

        private static EEncryptionType GetEncryptionType(byte[] valueBytes)
        {
            if (valueBytes.AsSpan(0, 3).SequenceEqual(AesHeadBytes))
            {
                return EEncryptionType.AES;
            }
            else if (valueBytes.AsSpan(0, 4).SequenceEqual(DpapiHeadBytes))
            {
                return EEncryptionType.DPAPI;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private record LocalState([property: JsonPropertyName("os_crypt")] OsCrypt OsCrypt);

        private record OsCrypt([property: JsonPropertyName("encrypted_key")] string EncryptedKey);
    }

    internal enum EEncryptionType
    {
        DPAPI,
        AES,
    }
}
