using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    #region Not
    //ClaimsPrincipalExtensions bu ne işe yarıyor bir ikişinin claimlerini ararken .net bizi biraz ugraştırıyor nedir bir kişinin jwt den gelen claimlerini okumak için
    //claimsPrincipal bunu genişletiyoruz burada claimsPrincipal bir kişinin jwt gelen kişinin claimlerini erişmek için .net de olan class bunu extend ediyoruz. 
    #endregion
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
