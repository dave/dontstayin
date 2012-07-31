/* *********************************************************************
 * The contents of this file are subject to the Mozilla Public License 
 * Version 1.1 (the "License"); you may not use this file except in 
 * compliance with the License. You may obtain a copy of the License at 
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS" 
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
 * See the License for the specific language governing rights and 
 * limitations under the License.
 *
 * The Original Code is named .NET Mobile API, first released under 
 * this licence on 11th March 2009.
 * 
 * The Initial Developer of the Original Code is owned by 
 * 51 Degrees Mobile Experts Limited. Portions created by 51 Degrees
 * Mobile Experts Limited are Copyright (C) 2009 - 2011. All Rights Reserved.
 * 
 * Contributor(s):
 *     James Rosewell <james@51degrees.mobi>
 * 
 * ********************************************************************* */

#region Usings

using System;
using System.Configuration;

#endregion

namespace FiftyOne.Foundation.Mobile.Detection.Configuration
{
    /// <summary>
    /// A collection of <see cref="FilesCollection"/>. This class cannot be inherited.
    /// </summary>
    internal sealed class FilesCollection : ConfigurationElementCollection
    {
        #region Methods

        /// <summary>
        /// Creates a new instance of <see cref="FileConfigElement"/>.
        /// </summary>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileConfigElement();
        }

        /// <summary>
        /// Gets the element key value.
        /// </summary>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((FileConfigElement)element).Name;
        }

        /// <summary>
        /// Add element to the base collection.
        /// </summary>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Gets the index of the specific element inside the collection.
        /// </summary>
        /// <param name="file">The file element being sought.</param>
        /// <returns>The index of the element.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="file"/> equals null.</exception>
        internal int IndexOf(FileConfigElement file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            return BaseIndexOf(file);
        }

        /// <summary>
        /// Add element into the collection.
        /// </summary>
        /// <param name="file">The file to be added to the collection.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="file"/> equals null.</exception>
        internal void Add(FileConfigElement file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            BaseAdd(file);
        }

        /// <summary>
        /// Removes a <see cref="System.Configuration.ConfigurationElement"/> from the collection.
        /// </summary>
        /// <param name="file">The xml file to be removed from the collection.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="file"/> equals null.</exception>
        internal void Remove(FileConfigElement file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            if (BaseIndexOf(file) >= 0)
                BaseRemove(file.Name);
        }

        /// <summary>
        /// Removes a <see cref="System.Configuration.ConfigurationElement"/> at the specified index location.
        /// </summary>
        /// <param name="index">The index of the patch to remove from the collection.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is less than zero or bigger than the number of indexes on the collections.</exception>
        internal void RemoveAt(int index)
        {
            if ((index < 0) || (index > base.Count - 1))
                throw new ArgumentOutOfRangeException("index");

            BaseRemoveAt(index);
        }

        /// <summary>
        /// Removes a <see cref="System.Configuration.ConfigurationElement"/> from the collection.
        /// </summary>
        /// <param name="name">The name of the patch to remove from the collection.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="name"/> equals null.</exception>
        internal void Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            BaseRemove(name);
        }

        /// <summary>
        /// Removes all configuration elements from the collection.
        /// </summary>
        internal void Clear()
        {
            BaseClear();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="FileConfigElement"/>.
        /// </summary>
        internal FileConfigElement this[int index]
        {
            get { return (FileConfigElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FileConfigElement"/>.
        /// </summary>        
        internal new FileConfigElement this[string name]
        {
            get { return (FileConfigElement)BaseGet(name); }
        }

        #endregion
    }
}
