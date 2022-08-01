using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrowserCookieReader.Exceptions;

namespace BrowserCookieReader.Readers
{
    public interface IBrowserCookieReader : IDisposable
    {
        /// <summary>
        /// Get cookie value from browser.
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="host">host</param>
        /// <param name="path">path</param>
        /// <returns>cookie value</returns>
        /// <exception cref="CookieNotFoundException"></exception>
        /// <exception cref="CookieExpiredException"></exception>
        string ReadValue(string name, string host, string path = "/");

        /// <summary>
        /// Get cookie value from browser. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="value">cookie value</param>
        /// <param name="name">name</param>
        /// <param name="host">host</param>
        /// <param name="path">path</param>
        /// <returns>true if <paramref name="value"/> was read successfully; otherwise false</returns>
        bool TryReadValue([NotNullWhen(true)] out string? value, string name, string host, string path = "/");

        /// <summary>
        /// Get cookie from browser.
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="host">host</param>
        /// <param name="path">path</param>
        /// <returns>cookie</returns>
        /// <exception cref="CookieNotFoundException"></exception>
        Cookie Read(string name, string host, string path = "/");

        /// <summary>
        /// Get cookie from browser. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="value">cookie</param>
        /// <param name="name">name</param>
        /// <param name="host">host</param>
        /// <param name="path">path</param>
        /// <returns>true if <paramref name="value"/> was read successfully; otherwise false</returns>
        bool TryRead([NotNullWhen(true)] out Cookie? value, string name, string host, string path = "/");
    }
}
