using System.Net;
using Newtonsoft.Json;
using Shared.Responses;
using Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            ApiResult errorResponse;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case BusinessRuleViolationException _:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new ApiResult(response.StatusCode, exception.Message);
                    break;

                case NotFoundException _:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse = new ApiResult(response.StatusCode, exception.Message);
                    break;

                case UniqueConstraintViolationException _:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    errorResponse = new ApiResult(response.StatusCode, exception.Message);
                    break;

                case TransactionFailedException _:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new ApiResult(response.StatusCode, exception.Message);
                    break;

                case AuthenticationFailedException _:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse = new ApiResult(response.StatusCode, exception.Message);
                    break;

                case DomainValidationException _:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    errorResponse = new ApiResult(response.StatusCode, exception.Message);
                    break;

                default:
                    errorResponse = new ApiResult(response.StatusCode, "Ha ocurrido un error inesperado");
                    break;
            }

            var result = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(result);
        }
    }
}
