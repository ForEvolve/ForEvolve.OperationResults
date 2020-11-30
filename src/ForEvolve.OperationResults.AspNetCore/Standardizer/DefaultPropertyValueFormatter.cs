namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents the default property value formatter, used by <see cref="DefaultOperationResultStandardizer"/>.
    /// Implements the <see cref="IPropertyValueFormatter" />
    /// </summary>
    /// <seealso cref="IPropertyValueFormatter" />
    public class DefaultPropertyValueFormatter : IPropertyValueFormatter
    {
        /// <inheritdoc />
        public object Format(object @object)
        {
            return @object;
        }
    }
}
