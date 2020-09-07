using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvioApi.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        public ErrorResponse(List<ErrorModel> errors)
        {
            Errors = errors;
        }

        public ErrorResponse()
        {
        }
    }
}
