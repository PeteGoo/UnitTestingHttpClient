using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace UnitTestingHttpClient
{
    [TestClass]
    public class MyCoolServiceTests
    {
        [TestMethod]
        public async Task ActuallyGoToGoogle()
        {
            var content = await new MyCoolService().GetGoogleContent();

            Assert.IsNotNull(content);
        }

        [TestMethod]
        public async Task MockGoToGoogle()
        {
            var handler = new Mock<HttpMessageHandler>();

            const string expected = "Yo! Just call me Test Google!!!";

            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => 
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(expected)
                    }));

            var actual = await new MyCoolService(handler.Object).GetGoogleContent();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
