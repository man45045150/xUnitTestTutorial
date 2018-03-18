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
            mock.Setup(foo => foo.DoSomething(It.IsIn("pong","foo","test"))).Returns(false);

            Assert.True(mock.Object.DoSomething("ping"));
            Assert.False(mock.Object.DoSomething("pong"));
        }

        [Fact]
        public void ArgumentDependentMatching(){
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(false);
            mock.Setup(foo => foo.Add(It.Is<int>(x=> x % 2 == 0)))
                .Returns(true);
            mock.Setup(foo => foo.Add(It.IsInRange<int>(1,10,Range.Inclusive)))
                .Returns(false);
            mock.Setup(foo => foo.DoSomething(It.IsRegex("[a-z]+")))
                .Returns(false);

            //Assert.True(mock.Object.DoSomething("abc"));
            Assert.False(mock.Object.DoSomething("123"));
        }
        [Fact]
        public void OutAndRefArguments(){
            var mock = new Mock<IFoo>();
            var requiredOutput = "ok";
            mock.Setup(foo => foo.TryParse("ping",out requiredOutput))
                .Returns(true);

            string result;

            Assert.True(mock.Object.TryParse("ping",out result));  

            var thisShouldBeFalse = mock.Object.TryParse("pong",out result);
            Console.WriteLine(thisShouldBeFalse);  
            Assert.False(thisShouldBeFalse);

            var bar = new Bar();
            mock.Setup(foo => foo.Submit(ref bar))
                .Returns(true);

            var someOtherBar = new Bar();
            Assert.False(mock.Object.Submit(ref someOtherBar));
        }
        [Fact]
        public void MockReturn(){
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.ProcessString(It.IsAny<string>()))
                .Returns((string s)=>s.ToLowerInvariant());

            var calls = 0;
            mock.Setup(foo => foo.GetCount())
                .Returns(()=> calls)
                .Callback(()=> ++calls);

            mock.Object.GetCount();
            mock.Object.GetCount();
            
            Assert.Equal(mock.Object.GetCount(),2);
        }

        [Fact]
        public void MockException(){
            var mock = new Mock<IFoo>();
             mock.Setup(foo => foo.DoSomething("null"))
                .Throws(new ArgumentException("cmd"));

            Assert.Throws<ArgumentException>(
                () =>{
                    return mock.Object.DoSomething("null");
                }

            );
        }
        [Fact]
        public void PropertyMock(){
            var mock = new Mock<IFoo>();
             mock.Setup(foo => foo.Name)
                .Returns("bar");
            mock.Object.Name = "will not be assigned";
            Assert.Equal(mock.Object.Name ,"bar");

            bool setterCalled = false;
            mock.SetupSet(foo =>
            {
                foo.Name = It.IsAny<string>();

            })
            .Callback<string>(value => {
                setterCalled = true;
            });
            mock.Object.Name="def";

            mock.VerifySet(foo => {
                foo.Name = "def";
            },Times.AtLeastOnce());

        }
        [Fact]
        public void TrackingValue(){
            var mock = new Mock<IFoo>();
            mock.SetupAllProperties();
            IFoo foo = mock.Object;
            foo.Name = "abc";
            Assert.Equal(foo.Name,"abc");

            foo.Name = "abcd";
            foo.SomeOtherProperty = 123;
            Assert.Equal(foo.SomeOtherProperty,123);
            
        }
        public void TestMockBehavior(){
            var mockRepository = new MockRepository(MockBehavior.Strict){
                DefaultValue = DefaultValue.Mock
            };
            var fooMock = mockRepository.Create<IFoo>();
            var otherMock = mockRepository.Create<IBaz>(MockBehavior.Loose);

            mockRepository.Verify();
        }
    }
}
