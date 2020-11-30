using ForEvolve.OperationResults.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.OperationResults
{
    public static class MessageCollectionExtensions
    {
        /// <summary>
        /// Filters exception messages and returns their <see cref="ExceptionMessage.Exception"/> that are of the specified <typeparamref name="TException"/> type.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to search for.</typeparam>
        /// <param name="messages"></param>
        /// <returns>The filtered messages <see cref="ExceptionMessage.Exception"/>.</returns>
        public static IEnumerable<TException> GetExceptionsOfType<TException>(this MessageCollection messages)
            where TException : Exception
        {
            return messages
                .GetAll<ExceptionMessage>()
                .HavingDetailsOfTypeAs<TException>();
        }
    }
}
