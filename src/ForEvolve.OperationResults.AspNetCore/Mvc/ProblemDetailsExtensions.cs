using ForEvolve.OperationResults;
using ForEvolve.OperationResults.AspNetCore.Mvc;
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
            return ToOperationResult(problemDetails, OperationMessageLevel.Error);
        }

        public static IOperationResult ToOperationResult(this ProblemDetails problemDetails, OperationMessageLevel severity)
        {
            var result = new OperationResult();
            result.Messages.Add(new ProblemDetailsMessage(problemDetails, severity));
            return result;
        }

        public static IOperationResult<TValue> ToOperationResult<TValue>(ProblemDetails problemDetails)
        {
            return ToOperationResult<TValue>(problemDetails, OperationMessageLevel.Error);
        }

        public static IOperationResult<TValue> ToOperationResult<TValue>(ProblemDetails problemDetails, OperationMessageLevel severity)
        {
            var result = new OperationResult<TValue>();
            result.Messages.Add(new ProblemDetailsMessage(problemDetails, severity));
            return result;
        }
    }
}
