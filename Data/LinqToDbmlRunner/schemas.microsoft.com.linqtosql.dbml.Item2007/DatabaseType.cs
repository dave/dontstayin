namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class DatabaseType : XTypedElement, IXMetaData
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ContentModelEntity contentModel;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Function> FunctionField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<XName, System.Type> localElementDictionary = new Dictionary<XName, System.Type>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Table> TableField;

        static DatabaseType()
        {
            BuildElementDictionary();
            contentModel = new SequenceContentModelEntity(new ContentModelEntity[] { new NamedContentModelEntity(XName.Get("Connection", "http://schemas.microsoft.com/linqtosql/dbml/2007")), new NamedContentModelEntity(XName.Get("Table", "http://schemas.microsoft.com/linqtosql/dbml/2007")), new NamedContentModelEntity(XName.Get("Function", "http://schemas.microsoft.com/linqtosql/dbml/2007")) });
        }

        private static void BuildElementDictionary()
        {
            localElementDictionary.Add(XName.Get("Connection", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Connection));
            localElementDictionary.Add(XName.Get("Table", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Table));
            localElementDictionary.Add(XName.Get("Function", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Function));
        }

        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<DatabaseType>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return contentModel;
        }

        public static explicit operator DatabaseType(XElement xe)
        {
            return XTypedServices.ToXTypedElement<DatabaseType>(xe, LinqToXsdTypeManager.Instance);
        }

        public string AccessModifier
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("AccessModifier", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("AccessModifier", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string BaseType
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("BaseType", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("BaseType", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string Class
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Class", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Class", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public schemas.microsoft.com.linqtosql.dbml.Item2007.Connection Connection
        {
            get
            {
                return (schemas.microsoft.com.linqtosql.dbml.Item2007.Connection) base.GetElement(XName.Get("Connection", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("Connection", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
            }
        }

        public string ContextNamespace
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("ContextNamespace", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("ContextNamespace", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string EntityBase
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("EntityBase", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("EntityBase", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string EntityNamespace
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("EntityNamespace", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("EntityNamespace", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public bool? ExternalMapping
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("ExternalMapping", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("ExternalMapping", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Function> Function
        {
            get
            {
                if (this.FunctionField == null)
                {
                    this.FunctionField = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Function>(this, LinqToXsdTypeManager.Instance, XName.Get("Function", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.FunctionField;
            }
            set
            {
                if (value == null)
                {
                    this.FunctionField = null;
                }
                else if (this.FunctionField == null)
                {
                    this.FunctionField = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Function>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Function", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Function>(this.FunctionField, value);
                }
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Dictionary<XName, System.Type> IXMetaData.LocalElementsDictionary
        {
            get
            {
                return localElementDictionary;
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
                return SchemaOrigin.Fragment;
            }
        }

        public string Modifier
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Modifier", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Modifier", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string Name
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Name", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Name", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string Provider
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Provider", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Provider", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string Serialization
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Serialization", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Serialization", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Table> Table
        {
            get
            {
                if (this.TableField == null)
                {
                    this.TableField = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Table>(this, LinqToXsdTypeManager.Instance, XName.Get("Table", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.TableField;
            }
            set
            {
                if (value == null)
                {
                    this.TableField = null;
                }
                else if (this.TableField == null)
                {
                    this.TableField = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Table>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Table", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Table>(this.TableField, value);
                }
            }
        }
    }
}

