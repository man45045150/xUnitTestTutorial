using System;
using Moq;
using Xunit;
using xUnitTest.Lib;

namespace xUnitTest.Test
{
    public class MockMethod
    {
        [Fact]
        public void OrdinaryMethodCalls(){
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);
        }
    }
}
