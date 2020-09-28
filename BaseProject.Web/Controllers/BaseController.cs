using BaseProject.Shared;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseProject.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        public BaseController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        protected List<string> ListModelErrors
        {
            get
            {
                return ModelState.Values
                  .SelectMany(x => x.Errors
                    .Select(ie => ie.ErrorMessage))
                    .ToList();
            }
        }

        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {
            _logger.Error(ex, ex.StackTrace);

            var rsp = new ApiResponse<string>();
            rsp.Code = ApiResponseCodes.ERROR;
#if DEBUG
            rsp.Description = $"Error: {(ex?.InnerException?.Message ?? ex.Message)} --> {ex?.StackTrace}";
            return Ok(rsp);
#else
            rsp.Description = customErrorMessage ?? "An error occurred while processing your request!";
            return Ok(rsp);
#endif
        }

        public IActionResult ApiResponse<T>(T data = default, string message = "",
            ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0, params string[] errors)
        {
            var response = new ApiResponse<T>(data, message, codes, totalCount, errors);
            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }
    }
}