using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Restaurants.Api;

public class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsPrinciple = new ClaimsPrincipal();

        claimsPrinciple.AddIdentity(new ClaimsIdentity(new[]{
            new Claim(ClaimTypes.NameIdentifier,"1"),
            new Claim(ClaimTypes.Role,"Admin"),
        },"Test"));

        var tickent = new AuthenticationTicket(claimsPrinciple, "Test");


       return Task.FromResult(AuthenticateResult.Success(tickent));
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        return Task.FromResult(PolicyAuthorizationResult.Success());
    }
}

