using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Order.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(ILoggerFactory loggerFactory, RequestDelegate next)
        {
            _loggerFactory = loggerFactory;
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
                var _logger = _loggerFactory.CreateLogger<GlobalErrorHandlingMiddleware>();
                if (ex is not InvalidRequestException
                    && ex is not OrderNotFoundException)
                    _logger.LogError(ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                string errorJson;
                switch (ex)
                {
                    case InvalidRequestException invalidRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorJson = JsonSerializer.Serialize(GetInvalidRequestExceptionResponse(invalidRequestException));
                        break;
                    case OrderNotFoundException orderNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorJson = JsonSerializer.Serialize(GetOrderNotFoundExceptionResponse(orderNotFoundException));
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorJson = JsonSerializer.Serialize(GetDefaultExceptionResponse(ex));
                        break;
                };

                await response.WriteAsync(errorJson);
            }
        }

        private object GetDefaultExceptionResponse(Exception ex)
        {
            return new
            {
                message = "Ocorreu um erro interno, por favor tente novamente. E caso o problema persista, entre em contato com o administrador.",
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
