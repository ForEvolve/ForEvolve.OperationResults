using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults.AspNetCore.Mvc
{
    public class ProblemDetailsExtensionsTest
    {
        public class ToOperationResult : ProblemDetailsExtensionsTest
        {
            [Fact]
            public void Should_throw_a_ArgumentNullException_when_problemDetails_is_null()
            {
                Assert.Throws<ArgumentNullException>(
                    "problemDetails",
                    () => ProblemDetailsExtensions.ToOperationResult(default)
                );
                Assert.Throws<ArgumentNullException>(
                    "problemDetails",
                    () => ProblemDetailsExtensions.ToOperationResult(default, OperationMessageLevel.Error)
                );
            }
        }
    }
}
