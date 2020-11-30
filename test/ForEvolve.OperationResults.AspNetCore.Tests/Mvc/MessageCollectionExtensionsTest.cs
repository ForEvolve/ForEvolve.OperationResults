using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults.AspNetCore.Mvc
{
    public class MessageCollectionExtensionsTest
    {
        public class GetExceptionsOfType : MessageCollectionExtensionsTest
        {
            [Fact]
            public void Should_return_all_ExceptionMessage_Exception()
            {
                // Arrange
                var sut = new MessageCollection();
                var exception1 = new Exception();
                var exception2 = new ArgumentNullException();
                var exception3 = new ArgumentException();
                var exception4 = new ArgumentNullException();
                sut.Add(new ExceptionMessage(exception1));
                sut.Add(new ExceptionMessage(exception2));
                sut.Add(new ExceptionMessage(exception3));
                sut.Add(new ExceptionMessage(exception4));

                // Act
                var result = sut.GetExceptionsOfType<ArgumentNullException>();

                // Assert
                Assert.Collection(result,
                    ex => Assert.Same(exception2, ex),
                    ex => Assert.Same(exception4, ex)
                );
            }
        }
    }
}
