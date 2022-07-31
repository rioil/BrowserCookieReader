using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserCookieReader
{
    public interface IBrowserCookieReader : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="host"></param>
        /// <param name="path"></param>
        /// <param name="originAttributes"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        string GetValue(string name, string host, string path = "/");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <param name="host"></param>
        /// <param name="path"></param>
        /// <param name="originAttributes"></param>
        /// <returns></returns>
        bool TryGetValue([NotNullWhen(true)] out string? value, string name, string host, string path = "/");
    }
}
