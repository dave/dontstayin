namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class Parameter : XTypedElement, IXMetaData
    {
        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<Parameter>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator Parameter(XElement xe)
        {
            return XTypedServices.ToXTypedElement<Parameter>(xe, LinqToXsdTypeManager.Instance);
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

        public string Direction
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Direction", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Direction", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        XName IXMetaData.SchemaName
        {
            get
            {
                return XName.Get("Parameter", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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

        public string Parameter1
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Parameter", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Parameter", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
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
    }
}

