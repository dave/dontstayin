namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class Function : XTypedElement, IXMetaData
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type> ElementTypeField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<XName, System.Type> localElementDictionary = new Dictionary<XName, System.Type>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Parameter> ParameterField;

        static Function()
        {
            BuildElementDictionary();
        }

        private static void BuildElementDictionary()
        {
            localElementDictionary.Add(XName.Get("Parameter", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Parameter));
            localElementDictionary.Add(XName.Get("ElementType", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Type));
            localElementDictionary.Add(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Return));
        }

        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<Function>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator Function(XElement xe)
        {
            return XTypedServices.ToXTypedElement<Function>(xe, LinqToXsdTypeManager.Instance);
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

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type> ElementType
        {
            get
            {
                if (this.ElementTypeField == null)
                {
                    this.ElementTypeField = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(this, LinqToXsdTypeManager.Instance, XName.Get("ElementType", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.ElementTypeField;
            }
            set
            {
                if (value == null)
                {
                    this.ElementTypeField = null;
                }
                else if (this.ElementTypeField == null)
                {
                    this.ElementTypeField = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("ElementType", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(this.ElementTypeField, value);
                }
            }
        }

        public bool? HasMultipleResults
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("HasMultipleResults", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("HasMultipleResults", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public string Id
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Id", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Id).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Id", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Id).Datatype);
            }
        }

        public bool? IsComposable
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsComposable", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsComposable", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
            }
        }

        public string Method
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("Method", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("Method", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
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
                return XName.Get("Function", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Parameter> Parameter
        {
            get
            {
                if (this.ParameterField == null)
                {
                    this.ParameterField = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Parameter>(this, LinqToXsdTypeManager.Instance, XName.Get("Parameter", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.ParameterField;
            }
            set
            {
                if (value == null)
                {
                    this.ParameterField = null;
                }
                else if (this.ParameterField == null)
                {
                    this.ParameterField = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Parameter>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Parameter", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Parameter>(this.ParameterField, value);
                }
            }
        }

        public schemas.microsoft.com.linqtosql.dbml.Item2007.Return Return
        {
            get
            {
                return (schemas.microsoft.com.linqtosql.dbml.Item2007.Return) base.GetElement(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
            }
            set
            {
                base.SetElement(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"), value);
            }
        }
    }
}

