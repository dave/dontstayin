namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class Table : XTypedElement, IXMetaData
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<XName, System.Type> localElementDictionary = new Dictionary<XName, System.Type>();

        static Table()
        {
            BuildElementDictionary();
        }

        private static void BuildElementDictionary()
        {
            localElementDictionary.Add(XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Type));
            localElementDictionary.Add(XName.Get("InsertFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunction));
            localElementDictionary.Add(XName.Get("UpdateFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunction));
            localElementDictionary.Add(XName.Get("DeleteFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunction));
        }

        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<Table>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator Table(XElement xe)
        {
            return XTypedServices.ToXTypedElement<Table>(xe, LinqToXsdTypeManager.Instance);
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

        public TableFunction DeleteFunction
        {
            get
            {
                return (TableFunction) base.GetElement(XName.Get("DeleteFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("DeleteFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
            }
        }

        public TableFunction InsertFunction
        {
            get
            {
                return (TableFunction) base.GetElement(XName.Get("InsertFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("InsertFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
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
                return XName.Get("Table", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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

        public schemas.microsoft.com.linqtosql.dbml.Item2007.Type Type
        {
            get
            {
                return (schemas.microsoft.com.linqtosql.dbml.Item2007.Type) base.GetElement(XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
            }
        }

        public TableFunction UpdateFunction
        {
            get
            {
                return (TableFunction) base.GetElement(XName.Get("UpdateFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("UpdateFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
            }
        }
    }
}

