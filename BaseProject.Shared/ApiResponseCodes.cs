using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared
{
    public enum ApiResponseCodes
    {
        EXCEPTION = -5,

        [Description("Unauthorized Access")]
        UNAUTHORIZED = -4,

        [Description("Not Found")]
        NOT_FOUND = -3,

        [Description("Invalid Request")]
        INVALID_REQUEST = -2,

        [Description("Server error occured, please try again.")]
        ERROR = -1,

        [Description("FAILED")]
        FAILED = 2,

        [Description("SUCCESS")]
        OK = 1
    }

}