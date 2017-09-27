using System;
using System.Collections.Generic;
using System.Text;

namespace API.CheckoutTest.ClientModels
{
    public enum Status
    {
        Fail = 0,
        Success = 1
    }

    public class Response : IDisposable
    {
        public Status Status { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ErrorTime { get; set; }
        public Response()
        {
            StartTime = DateTime.UtcNow;
        }

        public void Dispose()
        {
            EndTime = DateTime.UtcNow;
        }
    }

    public static class ResponseFactory
    {
        public static void SetSuccessResult(this Response result, string additionalDescription = "")
        {
            result.Status = Status.Success;

            result.Description = additionalDescription;
        }

        public static void SetFailureResult(this Response result, string additionalDescription = "")
        {
            result.Status = Status.Fail;

            result.ErrorTime = DateTime.UtcNow;

            result.Description = additionalDescription;
        }
    }

}
