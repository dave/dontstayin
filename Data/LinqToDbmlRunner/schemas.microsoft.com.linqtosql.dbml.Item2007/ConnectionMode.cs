namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.Xml.Schema;

    public sealed class ConnectionMode
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static SimpleTypeValidator TypeDefinition = new AtomicSimpleTypeValidator(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), new RestrictionFacets(RestrictionFlags.Enumeration, new object[] { "ConnectionString", "AppSettings", "WebSettings" }, 0, 0, null, null, 0, null, null, 0, null, 0, XmlSchemaWhiteSpace.Preserve));

        private ConnectionMode()
        {
        }
    }
}

