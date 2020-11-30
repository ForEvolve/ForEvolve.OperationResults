using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace ForEvolve.OperationResults
{
    public class OperationResultExtensionsTest
    {
        public class ConvertTo
        {
            [Fact]
            public void Should_convert_SuccessResult_to_SuccessValueResult()
            {
                // Arrange
                var success = OperationResult.Success();

                // Act
                var result = success.ConvertTo<OperationResult<object>, object>();

                // Assert
                Assert.NotNull(result);
                Assert.False(result.HasValue());
            }

            [Fact]
            public void Should_convert_FailureResult_to_FailureValueResult()
            {
                // Arrange
                var exception = new Exception("Some error");
                var failure = OperationResult.Failure(new TestExMessage(exception));

                // Act
                var result = failure.ConvertTo<OperationResult<object>, object>();

                // Assert
                Assert.NotNull(result);
                Assert.Collection(result.Messages,
                    m => {
                        var exceptionMessage = Assert.IsType<TestExMessage>(m);
                        Assert.Same(exception, exceptionMessage.Exception);
                    }
                );
                Assert.False(result.HasValue());
            }

            [Fact]
            public void Should_convert_SuccessValueResult_to_SucessResult()
            {
                // Arrange
                var value = new { Name = "Some test value" };
                var success = OperationResult.Success(value);

                // Act
                var result = success.ConvertTo<OperationResult>();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<OperationResult>(result);
            }

            [Fact]
            public void Should_convert_FailureValueResult_to_FailureResult()
            {
                // Arrange
                var exception = new Exception("Some error");
                var failure = OperationResult.Failure<object>(new TestExMessage(exception));

                // Act
                var result = failure.ConvertTo<OperationResult>();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<OperationResult>(result);
                Assert.Collection(result.Messages,
                    m => {
                        var exceptionMessage = Assert.IsType<TestExMessage>(m);
                        Assert.Same(exception, exceptionMessage.Exception);
                    }
                );
            }

            [Fact]
            public void Should_convert_GenericResult_to_GenericResult()
            {
                // Arrange
                IOperationResult resultToConvert = OperationResult.Success(new ConvertTestClass());

                // Act
                var result = resultToConvert.ConvertTo<OperationResult<ConvertTestClass>>();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<OperationResult<ConvertTestClass>>(result);
            }

            [Fact]
            public void Should_convert_GenericResult_to_NonGenericResult()
            {
                // Arrange
                IOperationResult resultToConvert = OperationResult.Success(new ConvertTestClass());

                // Act
                var result = resultToConvert.ConvertTo<OperationResult>();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<OperationResult>(result);
            }

            [Fact]
            public void Should_convert_NonGenericResult_to_GenericResult()
            {
                // Arrange
                var resultToConvert = OperationResult.Success();

                // Act
                var result = resultToConvert.ConvertTo<IOperationResult<ConvertTestClass>>();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<OperationResult<ConvertTestClass>>(result);
            }
        }

        private class ConvertTestClass
        {

        }

        private class TestExMessage : Message
        {
            public Exception Exception { get; }

            public TestExMessage(Exception exception)
                : base(OperationMessageLevel.Error)
                => Exception = exception;
        }
    }
}
