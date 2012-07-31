namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    public class Database : XTypedElement, IXMetaData
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DatabaseType ContentField;

        public Database()
        {
            this.SetInnerType(new DatabaseType());
        }

        public Database(DatabaseType content)
        {
            this.SetInnerType(content);
        }

        public override XTypedElement Clone()
        {
            return new Database((DatabaseType) this.Content.Clone());
        }

        public static Database Load(string xmlFile)
        {
            return XTypedServices.Load<Database, DatabaseType>(xmlFile, LinqToXsdTypeManager.Instance);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator Database(XElement xe)
        {
            return XTypedServices.ToXTypedElement<Database, DatabaseType>(xe, LinqToXsdTypeManager.Instance);
        }

        public static Database Parse(string xml)
        {
            return XTypedServices.Parse<Database, DatabaseType>(xml, LinqToXsdTypeManager.Instance);
        }

        public void Save(TextWriter tw)
        {
            XTypedServices.Save(tw, this.Untyped);
        }

        public void Save(string xmlFile)
        {
            XTypedServices.Save(xmlFile, this.Untyped);
        }

        public void Save(XmlWriter xmlWriter)
        {
            XTypedServices.Save(xmlWriter, this.Untyped);
        }

        private void SetInnerType(DatabaseType ContentField)
        {
            this.ContentField = (DatabaseType) XTypedServices.GetCloneIfRooted(ContentField);
            XTypedServices.SetName(this, this.ContentField);
        }

        public string AccessModifier
        {
            get
            {
                return this.ContentField.AccessModifier;
            }
            set
            {
                this.ContentField.AccessModifier = value;
            }
        }

        public string BaseType
        {
            get
            {
                return this.ContentField.BaseType;
            }
            set
            {
                this.ContentField.BaseType = value;
            }
        }

        public string Class
        {
            get
            {
                return this.ContentField.Class;
            }
            set
            {
                this.ContentField.Class = value;
            }
        }

        public schemas.microsoft.com.linqtosql.dbml.Item2007.Connection Connection
        {
            get
            {
                return this.ContentField.Connection;
            }
            set
            {
                this.ContentField.Connection = value;
            }
        }

        public DatabaseType Content
        {
            get
            {
                return this.ContentField;
            }
        }

        public string ContextNamespace
        {
            get
            {
                return this.ContentField.ContextNamespace;
            }
            set
            {
                this.ContentField.ContextNamespace = value;
            }
        }

        public string EntityBase
        {
            get
            {
                return this.ContentField.EntityBase;
            }
            set
            {
                this.ContentField.EntityBase = value;
            }
        }

        public string EntityNamespace
        {
            get
            {
                return this.ContentField.EntityNamespace;
            }
            set
            {
                this.ContentField.EntityNamespace = value;
            }
        }

        public bool? ExternalMapping
        {
            get
            {
                return this.ContentField.ExternalMapping;
            }
            set
            {
                this.ContentField.ExternalMapping = value;
            }
        }

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Function> Function
        {
            get
            {
                return this.ContentField.Function;
            }
            set
            {
                this.ContentField.Function = value;
            }
        }

        XTypedElement IXMetaData.Content
        {
            get
            {
                return this.Content;
            }
        }

        Dictionary<XName, System.Type> IXMetaData.LocalElementsDictionary
        {
            get
            {
                return ((IXMetaData) this.Content).LocalElementsDictionary;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        XName IXMetaData.SchemaName
        {
            get
            {
                return XName.Get("Database", "http://schemas.microsoft.com/linqtosql/dbml/2007");
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILinqToXsdTypeManager IXMetaData.TypeManager
        {
            get
            {
                return LinqToXsdTypeManager.Instance;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        SchemaOrigin IXMetaData.TypeOrigin
        {
            get
            {
                return SchemaOrigin.Element;
            }
        }

        public string Modifier
        {
            get
            {
                return this.ContentField.Modifier;
            }
            set
            {
                this.ContentField.Modifier = value;
            }
        }

        public string Name
        {
            get
            {
                return this.ContentField.Name;
            }
            set
            {
                this.ContentField.Name = value;
            }
        }

        public string Provider
        {
            get
            {
                return this.ContentField.Provider;
            }
            set
            {
                this.ContentField.Provider = value;
            }
        }

        public string Serialization
        {
            get
            {
                return this.ContentField.Serialization;
            }
            set
            {
                this.ContentField.Serialization = value;
            }
        }

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Table> Table
        {
            get
            {
                return this.ContentField.Table;
            }
            set
            {
                this.ContentField.Table = value;
            }
        }

        public override XElement Untyped
        {
            get
            {
                return base.Untyped;
            }
            set
            {
                base.Untyped = value;
                this.ContentField.Untyped = value;
            }
        }
    }
}

