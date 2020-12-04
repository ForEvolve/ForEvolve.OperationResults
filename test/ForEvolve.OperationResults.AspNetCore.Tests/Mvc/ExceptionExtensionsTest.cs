using System;
using Xunit;

namespace ForEvolve.OperationResults.AspNetCore.Mvc
{
    public class ExceptionExtensionsTest
    {
        public class ToOperationResult : ProblemDetailsExtensionsTest
        {
            [Fact]
            public void Should_throw_a_ArgumentNullException_when_exception_is_null()
            {
                Assert.Throws<ArgumentNullException>(
                    "exception",
                    () => ExceptionExtensions.ToOperationResult(default)
                );
            }
        }
    }
}
