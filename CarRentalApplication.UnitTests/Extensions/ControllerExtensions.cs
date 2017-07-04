using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CarRentalApplication.UnitTests
{
    public static class ControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string id, string username)
        {
            var identity = new GenericIdentity(username);

            

            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));

            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", id));

            var principal = new GenericPrincipal(identity, null);

            //var mockHttpContext = new Mock<HttpContext>();
            //mockHttpContext.SetupGet(c => c.User).Returns(principal);

            //var mockControllerContext = new Mock<ControllerContext>();
            //mockControllerContext.SetupGet(c => c.HttpContext).Returns(mockHttpContext.Object);

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
            ctx.HttpContext == Mock.Of<HttpContext>(http =>
                http.User == principal));          

        }
    }
}
