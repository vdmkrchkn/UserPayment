using Moq;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpResponse : Mock<HttpResponseBase>
    {        
        public MockHttpResponse(MockBehavior mockBehavior) : base(mockBehavior)
        {            
        }
    }
}