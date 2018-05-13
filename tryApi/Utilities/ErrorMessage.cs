using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace tryApi.Utilities
{
    public class ErrorMessage: IHttpActionResult
    {
        string _Message = "";
        HttpRequestMessage _Request;
        HttpStatusCode _StatusCode;

        public ErrorMessage(Exception ex, HttpStatusCode StatusCode, HttpRequestMessage Request)
        {
            _Request = Request;
            _StatusCode = StatusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_StatusCode)
            {
                Content = new StringContent(_Message),
                RequestMessage = _Request

            };
            return Task.FromResult(response);
        }
    }
}