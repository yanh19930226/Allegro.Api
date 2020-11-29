using Allegro.SDK;
using Allegro.SDK.Models.AuthTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services
{
    public interface IAllegroAuthService
    {
        /// <summary>
        /// 获取应用Token
        /// </summary>
        /// <returns></returns>
        Task<AllegroResult<AuthTokenResponse>> GetTokenAppAsync();
        /// <summary>
        /// 获取应用Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AllegroResult<AuthTokenResponse>> GetTokenUserAsync(string code);
    }
}
