using ForEvolve.OperationResults;
using ForEvolve.OperationResults.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ProblemDetailsExtensions
    {
        public static IOperationResult ToOperationResult(this ProblemDetails problemDetails)
        {
            if (problemDetails == null) { throw new ArgumentNullException(nameof(problemDetails)); }
            return ToOperationResult(problemDetails, OperationMessageLevel.Error);
        }

        public static IOperationResult ToOperationResult(this ProblemDetails problemDetails, OperationMessageLevel severity)
        {
            if (problemDetails == null) { throw new ArgumentNullException(nameof(problemDetails)); }
            var result = new OperationResult();
            result.Messages.Add(new ProblemDetailsMessage(problemDetails, severity));
            return result;
        }

        public static IOperationResult<TValue> ToOperationResult<TValue>(ProblemDetails problemDetails)
        {
            if (problemDetails == null) { throw new ArgumentNullException(nameof(problemDetails)); }
            return ToOperationResult<TValue>(problemDetails, OperationMessageLevel.Error);
        }

        public static IOperationResult<TValue> ToOperationResult<TValue>(ProblemDetails problemDetails, OperationMessageLevel severity)
        {
            if (problemDetails == null) { throw new ArgumentNullException(nameof(problemDetails)); }
            var result = new OperationResult<TValue>();
            result.Messages.Add(new ProblemDetailsMessage(problemDetails, severity));
            return result;
        }
    }
}
