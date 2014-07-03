using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestingHttpClient
{
    public class StubMessageHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _stub;

        public StubMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> stub)
        {
            _stub = stub;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_stub(request));
        }
    }
}