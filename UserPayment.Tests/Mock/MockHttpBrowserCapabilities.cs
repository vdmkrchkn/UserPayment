using Moq;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpBrowserCapabilities : Mock<HttpBrowserCapabilitiesBase>
    {
        public MockHttpBrowserCapabilities(MockBehavior mockBehavior) : base(mockBehavior)
        {
            Setup(b => b.IsMobileDevice).Returns(false);
        }
    }
}