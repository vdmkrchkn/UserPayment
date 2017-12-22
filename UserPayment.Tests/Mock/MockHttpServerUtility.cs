using Moq;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpServerUtility : Mock<HttpServerUtilityBase>
    {
        public MockHttpServerUtility(MockBehavior mockBehavior) : base(mockBehavior)
        {            
        }
    }
}