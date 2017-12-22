using Moq;
using System.Collections.Specialized;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpContext : Mock<HttpContextBase>
    {
        //[Inject]
        public HttpCookieCollection Cookies { get; set; }

        public MockHttpCachePolicy Cache { get; set; }

        public MockHttpBrowserCapabilities Browser { get; set; }

        public MockHttpSessionState SessionState { get; set; }

        public MockHttpServerUtility ServerUtility { get; set; }

        public MockHttpResponse Response { get; set; }

        public MockHttpRequest Request { get; set; }

        public MockHttpContext(MockBehavior mockBehavior = MockBehavior.Strict)
            : base(mockBehavior)       
        {
            //request 
            Browser = new MockHttpBrowserCapabilities(mockBehavior);            

            Request = new MockHttpRequest(mockBehavior);
            Request.Setup(r => r.Cookies).Returns(Cookies);                        
            Request.Setup(r => r.Browser).Returns(Browser.Object);
            Setup(p => p.Request).Returns(Request.Object);

            //response
            Cache = new MockHttpCachePolicy(MockBehavior.Loose);

            Response = new MockHttpResponse(mockBehavior);
            Response.Setup(r => r.Cookies).Returns(Cookies);
            Response.Setup(r => r.Cache).Returns(Cache.Object);
            Setup(p => p.Response).Returns(Response.Object);

            //user
            //if (auth != null)
            //{
            //    this.Setup(p => p.User).Returns(() => auth.CurrentUser);
            //}
            //else
            //{
            //    this.Setup(p => p.User).Returns(new UserProvider("", null));
            //}

            //Session State
            SessionState = new MockHttpSessionState();
            Setup(p => p.Session).Returns(SessionState.Object);

            //Server Utility
            ServerUtility = new MockHttpServerUtility(mockBehavior);
            Setup(p => p.Server).Returns(ServerUtility.Object);

            //Items
            var items = new ListDictionary();
            Setup(p => p.Items).Returns(items);
        }
    }
}
