using Moq;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpCachePolicy : Mock<HttpCachePolicyBase>
    {
        public MockHttpCachePolicy(MockBehavior behavior) : base(behavior)
        {
        }
    }
}
