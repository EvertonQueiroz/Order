using Microsoft.AspNetCore.Http;
using Order.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var errorResponse = GetErrorResponse(ex);
                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
        }

        private object GetErrorResponse(Exception ex)
        {
            switch (ex)
            {
                case InvalidRequestException invalidRequestException:
                    return GetInvalidRequestExceptionResponse(invalidRequestException);
                case OrderNotFoundException orderNotFoundException:
                    return GetOrderNotFoundExceptionResponse(orderNotFoundException);
                default:
                    return GetDefaultExceptionResponse(ex);
            };
        }

        private object GetDefaultExceptionResponse(Exception ex)
        {
            return new
            {
                message = ex.Message,
                statusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        private object GetInvalidRequestExceptionResponse(InvalidRequestException ex)
        {
            return new
            {
                message = ex.Message,
                errors = ex.Errors,
                statusCode = (int)HttpStatusCode.BadRequest
            };
        }

        private object GetOrderNotFoundExceptionResponse(OrderNotFoundException ex)
        {
            return new
            {
                message = ex.Message,
                statusCode = (int)HttpStatusCode.NotFound
            };
        }
    }
}
