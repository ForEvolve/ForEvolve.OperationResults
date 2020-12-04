using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForEvolve.OperationResults.AspNetCore
{
    /// <summary>
    /// Represents an operation result message build around [RFC3986] <see cref="OperationResults.ProblemDetails"/>.
    /// Inherits from <see cref="Message" />
    /// </summary>
    /// <seealso cref="Message" />
    public class ProblemDetailsMessage : Message
    {
        /// <summary>
        /// Gets the problem details.
        /// </summary>
        /// <value>The problem details.</value>
        [JsonIgnore]
        public ProblemDetails ProblemDetails { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDetailsMessage"/> class.
        /// </summary>
        /// <param name="problemDetails">The problem details.</param>
        /// <param name="severity">The severity.</param>
        /// <exception cref="ArgumentNullException">problemDetails</exception>
        public ProblemDetailsMessage(ProblemDetails problemDetails, OperationMessageLevel severity)
            : base(severity, problemDetails)
        {
            ProblemDetails = problemDetails ?? throw new ArgumentNullException(nameof(problemDetails));
            LoadProblemDetails(problemDetails);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDetailsMessage"/> class.
        /// Sub-classes must manually call the <see cref="LoadProblemDetails"/> method.
        /// </summary>
        /// <param name="severity">The severity.</param>
        protected ProblemDetailsMessage(OperationMessageLevel severity)
            : base(severity)
        {
        }

        /// <summary>
        /// Loads the specified problem details into the <see cref="Details"/> dictionary.
        /// </summary>
        /// <param name="problemDetails">The problem details to load.</param>
        protected void LoadProblemDetails(ProblemDetails problemDetails)
        {
            if (problemDetails.Type != null)
            {
                Details.Add(nameof(problemDetails.Type).ToLowerInvariant(), problemDetails.Type);
            }
            if (problemDetails.Title != null)
            {
                Details.Add(nameof(problemDetails.Title).ToLowerInvariant(), problemDetails.Title);
            }
            if (problemDetails.Status != null)
            {
                Details.Add(nameof(problemDetails.Status).ToLowerInvariant(), problemDetails.Status);
            }
            if (problemDetails.Detail != null)
            {
                Details.Add(nameof(problemDetails.Detail).ToLowerInvariant(), problemDetails.Detail);
            }
            if (problemDetails.Instance != null)
            {
                Details.Add(nameof(problemDetails.Instance).ToLowerInvariant(), problemDetails.Instance);
            }
            foreach (var item in problemDetails.Extensions)
            {
                Details.Add(item);
            }
        }
    }
}
