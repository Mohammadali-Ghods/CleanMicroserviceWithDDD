using Application.Base.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();
        protected string[] GetAccessingStoresFromJWTToken()
        {
            string accessingstoresstring = "";
            string role = "";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                accessingstoresstring = claims.ToList()
                    .Where(x => x.Type ==
                    "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/accessigstores")
                    .Select(x => x.Value).FirstOrDefault();
            }

            return accessingstoresstring?.Split(',') ?? Array.Empty<string>();
        }
        protected string GetRoleFromJWTToken()
        {
            string role = "";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                role = claims.ToList()
                    .Where(x => x.Type ==
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    .Select(x => x.Value).FirstOrDefault();
            }

            return role;
        }
        protected string GetLanguage()
        {
            Request.Headers.TryGetValue("language", out var headerValue);
            var result = headerValue.ToString();
            if (result != "")
            {
                return result;
            }
            else return "tr";
        }
        protected ActionResult CustomResponse(object result)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", _errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse("");
        }

        protected ActionResult CustomResponse(ResultModel Result)
        {
            if (Result.FailedResults != null)
            {
                foreach (var error in Result.FailedResults.Errors)
                {
                    AddError(error.ErrorMessage);
                }
            }

            return CustomResponse(Result.ID);
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}
