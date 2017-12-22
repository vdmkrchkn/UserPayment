using Moq;
using System.Collections.Generic;
using System.Web;

namespace UserPayment.Tests.Mock
{
    public class MockHttpSessionState : Mock<HttpSessionStateBase>
    {
        Dictionary<string, object> sessionStorage;

        public MockHttpSessionState(MockBehavior mockBehavior = MockBehavior.Strict)
            : base(mockBehavior)
        {
            sessionStorage = new Dictionary<string, object>();
            this.Setup(p => p[It.IsAny<string>()]).Returns((string index) => sessionStorage[index]);
            this.Setup(p => p.Add(It.IsAny<string>(), It.IsAny<object>())).Callback<string, object>((name, obj) =>
            {
                if (!sessionStorage.ContainsKey(name))
                {
                    sessionStorage.Add(name, obj);
                }
                else
                {
                    sessionStorage[name] = obj;
                }
            });
        }
    }
}
