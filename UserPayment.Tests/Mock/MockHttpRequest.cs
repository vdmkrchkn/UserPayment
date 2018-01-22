using Moq;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpRequest : Mock<HttpRequestBase>
    {        
        public MockHttpRequest(MockBehavior mockBehavior) : base(mockBehavior)
        {
            Setup(r => r.ValidateInput());
            Setup(r => r.UserAgent).Returns(
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11");
            Setup(r => r.ContentType).Returns("GET");
        }
    }
}