using System;
using System.Collections.Generic;

namespace Facebook.Api
{
    /// <summary>Provides base functionality for hand-coded abstractions of API method wrappers, mostly those that abstract over
    /// parameters that are required to be JSON-encoded.</summary>
    public class FacebookArgs : Dictionary<String, Object>
    {
        /// <summary>Initializes an instance of <see cref="FacebookArgs" />.</summary>
        public FacebookArgs() { }

        /// <summary>Intializes an instance of <see cref="FacebookArgs" />, that contains elements copied from <paramref name="dictionary "/>.</summary>
        /// <param name="dictionary"></param>
        public FacebookArgs(IDictionary<String, Object> dictionary)
            : base(dictionary) { }

        /// <summary>Gets or sets the value associated with the specified key.</summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        /// <remarks>This implementation hides the base indexer implementation such that specifying a key that does not exist returns null rather than throwing a <see cref="KeyNotFoundException" />.</remarks>
        public new Object this[String key]
        {
            get
            {
                Object value;
                if (this.TryGetValue(key, out value)) return value;
                else return null;
            }
            set { base[key] = value; }
        }

        /// <summary>In derived classes, provides specialized serialization logic for specific properties contained in this object.</summary>
        /// <param name="key">The key of the property to serialize.</param>
        /// <param name="args">A reference to a dictionary of arguments that will be passed directly to a <see cref="FacebookRequest" /> object.</param>
        /// <remarks>
        /// <para>This method allows specialized serialization logic, such as JSON encoding, to be applied to specific properties.</para>
        /// <para>To implement, use a <see langword="switch" /> (<see langword="Select" /> in VB.NET) statement to filter based on <paramref name="key" /> and provide the property-specific logic.
        /// The resulting value should then be added to <paramref name="args" /> using the same <paramref name="key "/>.
        /// </para>
        /// <para>Properties that do not require additional processing (strings, integral values, etc) should be ignored.</para>
        /// </remarks>
        protected virtual void SerializeProperty(String key, ref IDictionary<String, Object> args) { }

        /// <summary>Returns a dictionary of key/value pairs suitable to be passed a <see cref="FacebookRequest" /> object.</summary>
        /// <returns>A dictionary of key/value pairs suitable to be passed a <see cref="FacebookRequest" /> object.</returns>
        /// <remarks>This method calls the <see cref="SerializeProperty" /> for each key in the object, which allows property-specific processing
        /// to be done on any property.</remarks>
        /// <seealso cref="SerializeProperty" />
        public IDictionary<String, Object> GetArgs()
        {
            IDictionary<String, Object> args = new Dictionary<String, Object>();

            foreach (String key in this.Keys)
            {
                this.SerializeProperty(key, ref args);

                if (!args.ContainsKey(key) && this[key] != null)
                {
                    args.Add(key, this[key]);
                }
            }

            return args;
        }
    }
}
