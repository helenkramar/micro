namespace consumer.Base
{
    public class NameValueParameter
    {
        /// <summary>Initializes a new instance of NameValueParameter</summary>
        public NameValueParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of NameValueParameter with name and value
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public NameValueParameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>Gets or sets Name of parameter</summary>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets Value of parameter</summary>
        public virtual string Value { get; set; }
    }
}
