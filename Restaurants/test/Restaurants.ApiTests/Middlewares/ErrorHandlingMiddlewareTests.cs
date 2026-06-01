using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using FluentAssertions;

namespace Restaurants.Api.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_NoException_ShouldCallNextDelegate()
        {
           //arrange

            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context  = new DefaultHttpContext();
            var next = new Mock<RequestDelegate>();
            //act
            await middleware.InvokeAsync(context, next.Object);
            //assert
            next.Verify(x => x.Invoke(context), Times.Once);
        }



        [Fact()]
        public async Task InvokeAsync_NoFoundException_ShouldReturn404() {
            //arrange

            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            RequestDelegate next = cfx => throw new NotFoundException(nameof(Restaurant), "1");
            //act
            await middleware.InvokeAsync(context,next);
            //assert
            context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }



        [Fact()]
        public async Task InvokeAsync_ForbidException_ShouldReturn403()
        {
            //arrange

            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            RequestDelegate next = cfx => throw new ForbidException();
            //act
            await middleware.InvokeAsync(context, next);
            //assert
            context.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }



        [Fact()]
        public async Task InvokeAsync_Exception_ShouldReturn500()
        {
            //arrange

            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            RequestDelegate next = cfx => throw new Exception("Something went wrong");
            //act
            await middleware.InvokeAsync(context, next);
            //assert
            context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}