using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Facebook
{
    /// <summary>A factory class that uses <see cref="System.Reflection" /> to instiate instances of specified types.</summary>
    public static class ObjectFactory
    {
        private static Dictionary<String, ConstructorInfo> ctors = new Dictionary<String, ConstructorInfo>();

        /// <summary>Intializes an instance of <paramref name="type" /> using the specified <paramref name="parameters"/>.</summary>
        /// <param name="type">The <see cref="Type" /> of object to instantiate.</param>
        /// <param name="ctorSig">An array of <see cref="Type" /> objects that defines the signature of the target constructor.</param>
        /// <param name="parameters">An array of <see cref="Object" />s to pass into the constructor.</param>
        /// <returns>An instance of the specified <paramref name="type" />.</returns>
        public static Object Create(Type type, Type[] ctorSig, Object[] parameters)
        {
            var key = ObjectFactory.GenerateKey(type, ctorSig ?? new Type[0]);
            ConstructorInfo ctor = null;
            if (!ObjectFactory.ctors.TryGetValue(key, out ctor))
            {
                ctor = type.GetConstructor(ctorSig ?? new Type[0]);
                if (ctor == null) return Activator.CreateInstance(type, parameters);
                else ctors.Add(key, ctor);
            }

            return ctor.Invoke(parameters ?? new Type[0]);
        }

        /// <summary>Generates a key specific to the <paramref name="type" /> and <paramref name="ctorSig"/>.</summary>
        private static String GenerateKey(Type type, Type[] ctorSig)
        {
            var keyBuilder = new StringBuilder(type.FullName);
            foreach (var ctorSigType in ctorSig)
            {
                keyBuilder.AppendFormat("__{0}", ctorSigType.FullName);
            }
            return keyBuilder.ToString();
        }
    }
}
