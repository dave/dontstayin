namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.Xml.Schema;

    public sealed class MemberModifier
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static SimpleTypeValidator TypeDefinition = new AtomicSimpleTypeValidator(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), new RestrictionFacets(RestrictionFlags.Enumeration, new object[] { "Virtual", "Override", "New", "NewVirtual" }, 0, 0, null, null, 0, null, null, 0, null, 0, XmlSchemaWhiteSpace.Preserve));

        private MemberModifier()
        {
        }
    }
}

