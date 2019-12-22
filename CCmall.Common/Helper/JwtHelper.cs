using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using CCmall.Common.Configurations;

namespace CCmall.Common.Helper
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发Jwt证书
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string IssueJwt(JwtTokenModel token)
        {
            //init
            var issure = Appsettings.Jwt.Issuer;
            var audince = Appsettings.Jwt.Audience;
            var secret = Appsettings.Jwt.Secret;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, token.Uid.ToStr()),
                new Claim(JwtRegisteredClaimNames.Iss, issure),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),//过期时间
            };
            claims.AddRange(token.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //issure
            var jwt = new JwtSecurityToken(issure, audince, claims, signingCredentials: creds);
            var handler = new JwtSecurityTokenHandler();
            var ret = handler.WriteToken(jwt);
            return ret;
        }
        /// <summary>
        /// 解析Jwt
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static JwtTokenModel ParsingJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var ret = new JwtTokenModel();
            var token = handler.ReadJwtToken(jwt);
            try
            {
                token.Payload.TryGetValue(ClaimTypes.Role, out var roles);
                if (roles == null)
                {
                    return ret;
                }
                ret.Role = roles.ToStr()
                                .Replace("\n", "")
                                .Replace(" ", "")
                                .Replace("\t", "")
                                .Replace("\r", "")
                                .Replace("\"", "")
                                .Trim('[', ']');
                ret.Uid = token.Id.ToInt();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return ret;
        }
    }

    public class JwtTokenModel
    {
        public long Uid { get; set; }
        public string Role { get; set; }
    }
}
