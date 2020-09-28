using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Web.Config
{
    public class CustomTokenValidator : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var request = context.Result.ValidatedRequest;
            //sets token to 30 days expiry
            request.AccessTokenLifetime = 86400 * 30;

            return Task.CompletedTask;
        }
    }
}