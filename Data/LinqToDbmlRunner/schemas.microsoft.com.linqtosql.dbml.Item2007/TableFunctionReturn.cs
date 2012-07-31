namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class TableFunctionReturn : XTypedElement, IXMetaData
    {
        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<TableFunctionReturn>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator TableFunctionReturn(XElement xe)
        {
            return XTypedServices.ToXTypedElement<TableFunctionReturn>(xe, LinqToXsdTypeManager.Instance);
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
                return XName.Get("TableFunctionReturn", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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
    }
}

