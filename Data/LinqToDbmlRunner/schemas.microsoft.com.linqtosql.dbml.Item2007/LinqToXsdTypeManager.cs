namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class LinqToXsdTypeManager : ILinqToXsdTypeManager
    {
        private static Dictionary<XName, System.Type> elementDictionary = new Dictionary<XName, System.Type>();
        private static XmlSchemaSet schemaSet;
        private static Dictionary<XName, System.Type> typeDictionary = new Dictionary<XName, System.Type>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static LinqToXsdTypeManager typeManagerSingleton = new LinqToXsdTypeManager();
        private static Dictionary<System.Type, System.Type> wrapperDictionary = new Dictionary<System.Type, System.Type>();

        static LinqToXsdTypeManager()
        {
            BuildTypeDictionary();
            BuildElementDictionary();
            BuildWrapperDictionary();
        }

        protected internal static void AddSchemas(XmlSchemaSet schemas)
        {
            schemas.Add(schemaSet);
        }

        private static void BuildElementDictionary()
        {
            elementDictionary.Add(XName.Get("Database", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Database));
        }

        private static void BuildTypeDictionary()
        {
            typeDictionary.Add(XName.Get("Database", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(DatabaseType));
            typeDictionary.Add(XName.Get("Table", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Table));
            typeDictionary.Add(XName.Get("Type", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(schemas.microsoft.com.linqtosql.dbml.Item2007.Type));
            typeDictionary.Add(XName.Get("Column", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Column));
            typeDictionary.Add(XName.Get("Association", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Association));
            typeDictionary.Add(XName.Get("Function", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Function));
            typeDictionary.Add(XName.Get("TableFunction", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunction));
            typeDictionary.Add(XName.Get("Parameter", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Parameter));
            typeDictionary.Add(XName.Get("Return", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Return));
            typeDictionary.Add(XName.Get("TableFunctionParameter", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunctionParameter));
            typeDictionary.Add(XName.Get("TableFunctionReturn", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(TableFunctionReturn));
            typeDictionary.Add(XName.Get("Connection", "http://schemas.microsoft.com/linqtosql/dbml/2007"), typeof(Connection));
        }

        private static void BuildWrapperDictionary()
        {
            wrapperDictionary.Add(typeof(Database), typeof(DatabaseType));
        }

        public static System.Type GetRootType()
        {
            return elementDictionary[XName.Get("Database", "http://schemas.microsoft.com/linqtosql/dbml/2007")];
        }

        public static LinqToXsdTypeManager Instance
        {
            get
            {
                return typeManagerSingleton;
            }
        }

        Dictionary<XName, System.Type> ILinqToXsdTypeManager.GlobalElementDictionary
        {
            get
            {
                return elementDictionary;
            }
        }

        Dictionary<XName, System.Type> ILinqToXsdTypeManager.GlobalTypeDictionary
        {
            get
            {
                return typeDictionary;
            }
        }

        Dictionary<System.Type, System.Type> ILinqToXsdTypeManager.RootContentTypeMapping
        {
            get
            {
                return wrapperDictionary;
            }
        }

        XmlSchemaSet ILinqToXsdTypeManager.Schemas
        {
            get
            {
                if (schemaSet == null)
                {
                    XmlSchemaSet tempSet = new XmlSchemaSet();
                    Interlocked.CompareExchange<XmlSchemaSet>(ref schemaSet, tempSet, null);
                }
                return schemaSet;
            }
            set
            {
                schemaSet = value;
            }
        }
    }
}

