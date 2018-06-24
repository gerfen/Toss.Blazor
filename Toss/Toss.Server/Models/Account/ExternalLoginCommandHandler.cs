﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Toss.Server.Models;
using MediatR;
using System.Threading;

namespace Toss.Server.Controllers
{
    public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, Microsoft.AspNetCore.Identity.SignInResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return null;
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            return result;
        }
    }
}
