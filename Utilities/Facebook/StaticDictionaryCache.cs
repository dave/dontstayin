using System;
using System.Collections.Generic;

namespace Facebook
{
    /// <summary>An implementation of <see cref="ICacheWrapper" /> that stores items in a static dictionary.</summary>
    /// <remarks>This <see cref="ICacheWrapper" /> implementation is intended to be used for Desktop applications.</remarks>
    public class StaticDictionaryCache : ICacheWrapper
    {        
        private static Dictionary<String, Object> INTERNAL_DICTIONARY = new Dictionary<String, Object>();

        #region [ ICacheWrapper Members ]

        void ICacheWrapper.Add(String key, Object value)
        {
            INTERNAL_DICTIONARY.Add(key, value);
        }

        Object ICacheWrapper.Remove(String key)
        {
            Object value;
            INTERNAL_DICTIONARY.TryGetValue(key, out value);
            INTERNAL_DICTIONARY.Remove(key);
            return value;
        }

        Boolean ICacheWrapper.ContainsKey(String key)
        {
            return INTERNAL_DICTIONARY.ContainsKey(key);
        }

        Int32 ICacheWrapper.Count
        {
            get { return INTERNAL_DICTIONARY.Count; }
        }

        Object ICacheWrapper.this[String key]
        {
            get { return INTERNAL_DICTIONARY[key]; }
            set { INTERNAL_DICTIONARY[key] = value; }
        }

        #endregion
    }
}
