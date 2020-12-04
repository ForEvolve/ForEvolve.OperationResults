using ForEvolve.OperationResults;
using ForEvolve.OperationResults.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ExceptionExtensions
    {
        public static IOperationResult ToOperationResult(this Exception exception)
        {
            var result = new OperationResult();
            result.Messages.Add(new ExceptionMessage(exception));
            return result;
        }

        public static IOperationResult<TValue> ToOperationResult<TValue>(this Exception exception)
        {
            var result = new OperationResult<TValue>();
            result.Messages.Add(new ExceptionMessage(exception));
            return result;
        }
    }
}
