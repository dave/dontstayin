namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class Type : XTypedElement, IXMetaData
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association> AssociationField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Column> ColumnField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<XName, System.Type> localElementDictionary = new Dictionary<XName, System.Type>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type> Type1Field;

        static Type()
        {
            BuildElementDictionary();
        }

        private static void BuildElementDictionary()
        {
            localElementDictionary.Add(XName.Get("Column", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Column));
            localElementDictionary.Add(XName.Get("Association", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Association));
            localElementDictionary.Add(XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Type));
        }

        public override XTypedElement Clone()
        {
            return XTypedServices.CloneXTypedElement<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(this);
        }

        ContentModelEntity IXMetaData.GetContentModel()
        {
            return ContentModelEntity.Default;
        }

        public static explicit operator schemas.microsoft.com.linqtosql.dbml.Item2007.Type(XElement xe)
        {
            return XTypedServices.ToXTypedElement<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(xe, LinqToXsdTypeManager.Instance);
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

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association> Association
        {
            get
            {
                if (this.AssociationField == null)
                {
                    this.AssociationField = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>(this, LinqToXsdTypeManager.Instance, XName.Get("Association", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.AssociationField;
            }
            set
            {
                if (value == null)
                {
                    this.AssociationField = null;
                }
                else if (this.AssociationField == null)
                {
                    this.AssociationField = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Association", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>(this.AssociationField, value);
                }
            }
        }

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Column> Column
        {
            get
            {
                if (this.ColumnField == null)
                {
                    this.ColumnField = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>(this, LinqToXsdTypeManager.Instance, XName.Get("Column", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.ColumnField;
            }
            set
            {
                if (value == null)
                {
                    this.ColumnField = null;
                }
                else if (this.ColumnField == null)
                {
                    this.ColumnField = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Column", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>(this.ColumnField, value);
                }
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

        public string IdRef
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("IdRef", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Idref).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("IdRef", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Idref).Datatype);
            }
        }

        public string InheritanceCode
        {
            get
            {
                return XTypedServices.ParseValue<string>(base.Attribute(XName.Get("InheritanceCode", "")), XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set
            {
                base.SetAttribute(XName.Get("InheritanceCode", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }

        public bool? IsInheritanceDefault
        {
            get
            {
                XAttribute x = base.Attribute(XName.Get("IsInheritanceDefault", ""));
                if (x == null)
                {
                    return null;
                }
                return new bool?(XTypedServices.ParseValue<bool>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype));
            }
            set
            {
                base.SetAttribute(XName.Get("IsInheritanceDefault", ""), value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean).Datatype);
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
                return XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007");
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

        public IList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type> Type1
        {
            get
            {
                if (this.Type1Field == null)
                {
                    this.Type1Field = new XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(this, LinqToXsdTypeManager.Instance, XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                return this.Type1Field;
            }
            set
            {
                if (value == null)
                {
                    this.Type1Field = null;
                }
                else if (this.Type1Field == null)
                {
                    this.Type1Field = XTypedList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>.Initialize(this, LinqToXsdTypeManager.Instance, value, XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"));
                }
                else
                {
                    XTypedServices.SetList<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(this.Type1Field, value);
                }
            }
        }
    }
}

