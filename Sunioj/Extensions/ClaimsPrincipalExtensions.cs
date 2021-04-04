﻿using Sunioj.Core.Constants;
using System.Linq;
using System.Security.Claims;

namespace Sunioj.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasId(this ClaimsPrincipal user, int id)
        {
            if (!user.Claims.Any(x => x.Type == AppClaims.Id))
            {
                return false;
            }

            return user.Claims.Single(x => x.Type == AppClaims.Id).Value == id.ToString();
        }

        public static int GetId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.Single(x => x.Type == AppClaims.Id);
            return int.Parse(claim.Value);
        }
    }
}
