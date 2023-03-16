using Microsoft.AspNetCore.Mvc;

namespace UserApi.Models.Extensions
{
    public class BadRequestWithReasonResult : JsonResult
    { 

        public BadRequestWithReasonResult(string reason) : base(new {reason = reason})
        {
            StatusCode = 400;
        }

    }
}
