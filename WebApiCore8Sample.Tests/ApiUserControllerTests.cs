using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WebApiCore8Sample.Controllers;
using System.Security.Principal;

namespace WebApiCore8Sample.Tests
{
    public class ApiUserControllerTests
    {
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly ApiUserController _controller;

        public ApiUserControllerTests()
        {
            _httpContextMock = new Mock<HttpContext>();
            _controller = new ApiUserController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenIdentityIsNull()
        {
            _httpContextMock.Setup(x => x.User.Identity).Returns((IIdentity)null);

            var result = _controller.Get();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ReturnsOk_WhenIdentityIsNotNull()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "testUser"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            _httpContextMock.Setup(x => x.User.Identity).Returns(identity);

            var result = _controller.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        // TODO: Add more tests for other methods when they are implemented
    }
}