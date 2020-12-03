using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents a generic operation result message.
    /// Implements the <see cref="IMessage" />
    /// </summary>
    /// <seealso cref="IMessage" />
    public class Message : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="type">The message type.</param>
        public Message(OperationMessageLevel severity)
            : this(severity, new Dictionary<string, object>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="details">The details.</param>
        /// <param name="type">The message type.</param>
        /// <exception cref="ArgumentNullException">details</exception>
        public Message(OperationMessageLevel severity, IDictionary<string, object> details, Type type = null)
        {
            Severity = severity;
            Details = details ?? throw new ArgumentNullException(nameof(details));
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The message severity.</param>
        /// <param name="details">The message details that will be loaded in the <see cref="IDictionary{string, object}"/>.</param>
        /// <param name="ignoreNull">if set to <c>true</c> null properties will be ignored (not added in the <see cref="IDictionary{string, object}"/>).</param>
        /// <exception cref="ArgumentNullException">details</exception>
        public Message(OperationMessageLevel severity, object details, bool ignoreNull = true)
            : this(severity, details)
        {
            LoadDetails(details, ignoreNull);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="severity">The message severity.</param>
        /// <param name="details">The message details that will be loaded in the <see cref="IDictionary{string, object}"/>.</param>
        /// <exception cref="ArgumentNullException">details</exception>
        protected Message(OperationMessageLevel severity, object details)
            : this(severity)
        {
            if (details == null) { throw new ArgumentNullException(nameof(details)); }
            Type = details.GetType();
            IsAnonymous = Type.Name.Contains("AnonymousType");
            OriginalObject = details;
        }

        protected virtual void LoadDetails(object details, bool ignoreNull)
        {
            var properties = TypeDescriptor.GetProperties(details);
            foreach (PropertyDescriptor property in properties)
            {
                var value = property.GetValue(details);
                if (!ignoreNull || value != null)
                {
                    Details.Add(property.Name, value);
                }
            }
        }

        /// <inheritdoc />
        public virtual bool Is<TType>()
        {
            return typeof(TType) == Type;
        }

        /// <inheritdoc />
        public virtual bool Is(Type type)
        {
            return type == Type;
        }

        /// <inheritdoc />
        public virtual TType As<TType>()
        {
            if (!Is<TType>())
            {
                throw new TypeMismatchException(this, typeof(TType));
            }
            return (TType)As(typeof(TType));
        }

        /// <inheritdoc />
        public virtual object As(Type type)
        {
            if (!Is(type))
            {
                throw new TypeMismatchException(this, type);
            }
            if (CanReturnTheOriginalObject(type))
            {
                return OriginalObject;
            }
            var result = Activator­.CreateInstance(type);
            var properties = TypeDescriptor.GetProperties(result);
            foreach (PropertyDescriptor property in properties)
            {
                if (Details.ContainsKey(property.Name))
                {
                    property.SetValue(result, Details[property.Name]);
                }
            }
            return result;
        }

        private bool CanReturnTheOriginalObject(Type type)
        {
            return OriginalObject != null && type.IsAssignableFrom(OriginalObject.GetType());
        }

        /// <inheritdoc />
        public virtual OperationMessageLevel Severity { get; }

        /// <inheritdoc />
        public virtual IDictionary<string, object> Details { get; }

        /// <inheritdoc />
        [JsonIgnore]
        public virtual Type Type { get; }

        /// <summary>
        /// Gets if the <see cref="Type"/> was an anonymous type.
        /// </summary>
        [JsonIgnore]
        public virtual bool IsAnonymous { get; }

        /// <summary>
        /// Gets the original object that was used to load the Details, if any.
        /// </summary>
        [JsonIgnore]
        public virtual object OriginalObject { get; }
    }
}
