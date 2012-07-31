namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class Column : XTypedElement, IXMetaData
    {
        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<Column>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator Column(XElement xe)
        {
            return XTypedServices.ToXTypedElement<Column>(xe, LinqToXsdTypeManager.Instance);
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

        public string AutoSync
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("AutoSync", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("AutoSync", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public bool? CanBeNull
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("CanBeNull", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("CanBeNull", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public string DbType
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("DbType", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("DbType", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string Expression
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Expression", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Expression", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public bool? IsDbGenerated
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsDbGenerated", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsDbGenerated", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public bool? IsDelayLoaded
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsDelayLoaded", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsDelayLoaded", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public bool? IsDiscriminator
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsDiscriminator", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsDiscriminator", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public bool? IsPrimaryKey
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsPrimaryKey", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsPrimaryKey", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public bool? IsReadOnly
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsReadOnly", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsReadOnly", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public bool? IsVersion
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsVersion", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsVersion", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public string Member
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Member", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Member", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        XName IXMetaData.SchemaName
        {
            get
            {
                return XName.Get("Column", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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

        public string Storage
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Storage", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Storage", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string Type
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Type", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Type", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public string UpdateCheck
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("UpdateCheck", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("UpdateCheck", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
    }
}

