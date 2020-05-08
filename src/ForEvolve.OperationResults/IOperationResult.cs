﻿namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents an operation result containing optional messages, generated by the operation.
    /// </summary>
    public interface IOperationResult
    {
        /// <summary>
        /// Gets a value indicating whether the operation has succeeded.
        /// </summary>
        /// <value><c>true</c> if the operation has succeeded; otherwise, <c>false</c>.</value>
        bool Succeeded { get; }

        /// <summary>
        /// Gets the messages associated with the operation result.
        /// </summary>
        /// <value>The operation result messages.</value>
        MessageCollection Messages { get; }

        /// <summary>
        /// Determines whether the operation generated any messages.
        /// </summary>
        /// <returns><c>true</c> if the operation generated messages; otherwise, <c>false</c>.</returns>
        bool HasMessages();
    }

    /// <summary>
    /// Represents an operation result containing optional messages, generated by the operation, and an optional resulting object.
    /// Implements the <see cref="ForEvolve.OperationResults.IOperationResult" />
    /// </summary>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <seealso cref="ForEvolve.OperationResults.IOperationResult" />
    public interface IOperationResult<TValue> : IOperationResult
    {
        /// <summary>
        /// Gets the value attached by the operation.
        /// </summary>
        /// <value>The operation result's value.</value>
        TValue Value { get; }

        /// <summary>
        /// Determines whether the operation attached a value.
        /// </summary>
        /// <returns><c>true</c> if the operation attached a value; otherwise, <c>false</c>.</returns>
        bool HasValue();
    }
}
