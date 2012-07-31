namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class TableFunction : XTypedElement, IXMetaData
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<TableFunctionParameter> ArgumentField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ContentModelEntity contentModel;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<XName, System.Type> localElementDictionary = new Dictionary<XName, System.Type>();

        static TableFunction()
        {
            BuildElementDictionary();
            contentModel = new SequenceContentModelEntity(new ContentModelEntity[] { new NamedContentModelEntity(XName.Get("Argument", "http://schemas.microsoft.com/linqtosql/dbml/2007")), new NamedContentModelEntity(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007")) });
        }

        private static void BuildElementDictionary()
        {
            localElementDictionary.Add(XName.Get("Argument", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunctionParameter));
            localElementDictionary.Add(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunctionReturn));
        }

        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<TableFunction>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return contentModel;
        }

        public static explicit operator TableFunction(XElement xe)
        {
            return XTypedServices.ToXTypedElement<TableFunction>(xe, LinqToXsdTypeManager.Instance);
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

        public IList<TableFunctionParameter> Argument
        {
            get
            {
                if (this.ArgumentField == null)
                {
                    this.ArgumentField = new XTypedList<TableFunctionParameter>(this, LinqToXsdTypeManager.Instance, XName.Get("Argument", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.ArgumentField;
            }
            set
            {
                if (value == null)
                {
                    this.ArgumentField = null;
                }
                else if (this.ArgumentField == null)
                {
                    this.ArgumentField = XTypedList<TableFunctionParameter>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Argument", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<TableFunctionParameter>(this.ArgumentField, value);
                }
            }
        }

        public string FunctionId
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("FunctionId", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Idref).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("FunctionId", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Idref).Datatype);
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
                return XName.Get("TableFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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

        public TableFunctionReturn Return
        {
            get
            {
                return (TableFunctionReturn) base.GetElement(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
            }
        }
    }
}

