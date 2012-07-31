using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Xml.Linq;
using System;
namespace LinqToSqlClassTemplate
{
	public class Database
	{
		private XDocument dbml;

		public XNamespace ns { get; private set; }
		public List<Table> Tables;
		public String ContextNamespace { get; set; }
		public String EntityNamespace { get; set; }
		public String Serialization { get; set; }
		public String Name { get; set; }
		public String Class { get; set; }
		public String ConnectSettingsObject { get; set; }
		public String ConnectSettingsProperty { get; set; }

		public Database(string dbmlFileName)
		{
			ns = "http://schemas.microsoft.com/linqtosql/dbml/2007";
			dbml = XDocument.Load(dbmlFileName);
			ContextNamespace = (String)dbml.Root.Attribute("ContextNamespace") ?? "MyApplication";
			EntityNamespace = (String)dbml.Root.Attribute("EntityNamespace") ?? "MyApplication";
			foreach (XElement connection in dbml.Root.Elements(ns + "Connection"))
			{
				ConnectSettingsObject = (String)connection.Attribute("SettingsObjectName");
				ConnectSettingsProperty = (String)connection.Attribute("SettingsPropertyName");
			}
			Serialization = (String)dbml.Root.Attribute("Serialization");
			Name = (String)dbml.Root.Attribute("Name");
			Class = (String)dbml.Root.Attribute("Class");
			Tables = (from t in dbml.Root.Elements(ns + "Table")
					  select new Table(this, t)).ToList();
		}

		public Class FindClass(Predicate<Class> match)
		{
			foreach (Table t in Tables)
				foreach (Class c in t.Classes)
					if (match(c)) return c;
			throw new ArgumentException("Could not find public class " + match);
		}
	}

	public class CodeLanguage
	{
		public System.CodeDom.Compiler.CodeDomProvider CodeDomProvider { get; private set; }

		public CodeLanguage(CodeDomProvider codeDomProvider) { CodeDomProvider = codeDomProvider; }

		public String Format(Type type)
		{
			String typeRef = CodeDomProvider.GetTypeOutput(new System.CodeDom.CodeTypeReference(type));
			if (typeRef.StartsWith("System.Nullable<") && CodeDomProvider is Microsoft.CSharp.CSharpCodeProvider)
				typeRef = typeRef.Replace("System.Nullable<", "").Replace(">", "?");
			return (typeRef.LastIndexOf('.') != 6) ? typeRef.Replace("System.Data.Linq.", "") : typeRef.Replace("System.", "");
		}

		public String Format(System.CodeDom.MemberAttributes memberAttributes)
		{
			return GetAccess(memberAttributes) + GetModifier(memberAttributes);
		}

		public String GetAccess(System.CodeDom.MemberAttributes memberAttributes)
		{
			switch (memberAttributes & MemberAttributes.AccessMask)
			{
				case MemberAttributes.Private: return "private ";
				case MemberAttributes.Public: return "public ";
				case MemberAttributes.Family: return "protected ";
				case MemberAttributes.Assembly: return "internal ";
				case MemberAttributes.FamilyAndAssembly: return "protected internal ";
				default: return memberAttributes.ToString();
			}
		}

		public String GetModifier(MemberAttributes memberAttributes)
		{
			switch (memberAttributes & MemberAttributes.ScopeMask)
			{
				case MemberAttributes.Final: return "";
				case MemberAttributes.Abstract: return "abstract ";
				case MemberAttributes.Override: return "override ";
				default: return "virtual ";
			}
		}

	}

	public class Table
	{
		public Database Database { get; private set; }
		public List<Class> Classes { get; set; }
		public String Member { get; set; }
		public String Name { get; set; }
		public Class BaseClass { get; set; }

		public Table(Database database, XElement xe)
		{
			Database = database;
			Name = (String)xe.Attribute("Name");
			Member = (String)xe.Attribute("Member");
			Classes = (from c in xe.Descendants(Database.ns + "Type")
					   select new Class(this, c)).ToList();
			BaseClass = Classes[0];
		}
	}

	public class Class
	{
		private String name;

		public Table Table { get; private set; }
		public List<Column> Columns { get; set; }
		public List<Association> Associations { get; set; }
		public List<Class> Subclasses { get; set; }
		public Boolean IsBase { get { return (this == Table.BaseClass); } }
		public Column PrimaryKey { get { return (from c in Columns where c.IsPrimaryKey select c).First(); } }

		public String QualifiedName
		{
			get
			{
				return (String.IsNullOrEmpty(Table.Database.EntityNamespace) || (Table.Database.EntityNamespace == Table.Database.ContextNamespace))
					? Name : Table.Database.EntityNamespace + '.' + Name;
			}
		}

		public String Name
		{
			get { return (name == Table.Name) ? name + "1" : name; }
			set { name = value; }
		}

		public Class(Table table, XElement xe)
		{
			Table = table;
			name = (String)xe.Attribute("Name");
			Columns = (from c in xe.Elements(Table.Database.ns + "Column")
					   select new Column(this, c)).ToList();
			Associations = (from a in xe.Elements(Table.Database.ns + "Association")
							select new Association(this, a)).ToList();
			Subclasses = (from c in xe.Elements(Table.Database.ns + "Type")
						  select new Class(this.Table, c)).ToList();
		}
	}

	public class Association
	{
		private Class thisClass, otherClass;
		private String thisKeyName, otherKeyName, typeName, storage;
		private Column thisKey, otherKey;

		public String Name { get; set; }
		public String Member { get; set; }
		public Boolean IsForeignKey { get; set; }
		public String Storage { get { return storage ?? "_" + Member; } }
		public String DeleteRule { get; set; }
		public Boolean DeleteOnNull { get; set; }

		public Class Type
		{
			get
			{
				if (otherClass == null)
					otherClass = thisClass.Table.Database.FindClass(c => c.Name == typeName);
				return otherClass;
			}
			set { otherClass = value; }
		}

		public Association OtherSide
		{
			get
			{
				foreach (Association association in Type.Associations)
					if (association.Name == Name && association != this)
						return association;
				throw new InvalidOperationException(String.Format("Could not find association {0} within type {1}", Name, Type));
			}
		}

		public Column ThisKey
		{
			get
			{
				if (thisKey == null)
					thisKey = thisClass.Columns.Find(c => c.Name == thisKeyName);
				return thisKey;
			}
			set { thisKey = value; }
		}

		public Column OtherKey
		{
			get
			{
				if (otherKey == null)
					if (IsForeignKey)
						otherKey = Type.PrimaryKey;
					else
					{
						otherKey = Type.Columns.Find(c => c.Name == otherKeyName);
						if (otherKey == null)
							throw new ArgumentException(String.Format("Association {2} not find other key {0} on type {1}", otherKeyName, Type.Name, Name));
					}
				return otherKey;
			}
			set { otherKey = value; }
		}

		public Association(Class thisClass, XElement xe)
		{
			this.thisClass = thisClass;
			Name = (String)xe.Attribute("Name");
			Member = (String)xe.Attribute("Member");
			typeName = (String)xe.Attribute("Type");
			thisKeyName = (String)xe.Attribute("ThisKey");
			storage = (String)xe.Attribute("Storage");
			otherKeyName = (String)xe.Attribute("OtherKey");
			IsForeignKey = (xe.Attribute("IsForeignKey") != null);
			DeleteRule = (String)xe.Attribute("DeleteRule");
			DeleteOnNull = (Boolean?)xe.Attribute("DeleteOnNull") ?? false;
		}
	}

	public class Column
	{
		public string EnumProperty
		{
			get
			{
				// if this isn't a valid type, assume it's an overridden enum property
				if (new[] { "System.Data.Linq.Binary", "System.Xml.Linq.XElement" }.Contains(typeName))
					return null;
				if (Type.GetType(typeName) == null)
				{
					if (typeName.IndexOf(".") == -1)
					{
						return "Model.Entities." + Class.Name + "." + typeName;
					}
					if (typeName.IndexOf("Model.Entities.") == -1)
					{
						return "Model.Entities." + typeName;
					}
					return typeName;
				}
				return null;
			}
		}
        

		private String member, typeName, storage;
		private Type type;

		public System.Data.Linq.Mapping.AutoSync AutoSync { get; set; }
		public System.Data.Linq.Mapping.UpdateCheck UpdateCheck { get; set; }
		public Class Class { get; private set; }
		public String DbType { get; set; }
		public Boolean IsPrimaryKey { get; set; }
		public Boolean IsDbGenerated { get; set; }
		public Boolean IsDiscriminator { get; set; }
		public Boolean CanBeNull { get; set; }
		public String Name { get; set; }
		public MemberAttributes MemberAttributes { get; set; }

		public Type Type
		{
			get
			{
				if (type == null)
				{
					switch (typeName)
					{
						case "System.Xml.Linq.XElement": type = typeof(System.Xml.Linq.XElement); break;
						case "System.Data.Linq.Binary": type = typeof(System.Data.Linq.Binary); break;
						default: type = Type.GetType(typeName); break;
					}
					if (type == null)
						throw new TypeLoadException("Could not instantiate type '" + typeName + "'");
					if (type.IsValueType && CanBeNull)
						type = typeof(Nullable<>).MakeGenericType(type);
				}
				return type;
			}
			set { type = value; }
		}

		public String Storage
		{
			get { return String.IsNullOrEmpty(storage) ? "_" + Name : storage; }
			set { storage = value; }
		}

		public String Member
		{
			get { return member + ((member == Class.Name) ? "1" : ""); }
			set { member = value; }
		}

		public Column(Class class1, XElement xe)
		{
			Class = class1;
			Name = (String)xe.Attribute("Name") ?? "";
			Member = (String)xe.Attribute("Member") ?? Name;
			typeName = xe.Attribute("Type").Value;
			DbType = (String)xe.Attribute("DbType") ?? "";
			IsPrimaryKey = (Boolean?)xe.Attribute("IsPrimaryKey") ?? false;
			IsDbGenerated = (Boolean?)xe.Attribute("IsDbGenerated") ?? false;
			IsDiscriminator = (Boolean?)xe.Attribute("IsDiscriminator") ?? false;
			CanBeNull = (Boolean?)xe.Attribute("CanBeNull") ?? false;
			Storage = (String)xe.Attribute("Storage") ?? "";
			AutoSync = (xe.Attribute("AutoSync") != null)
				? (System.Data.Linq.Mapping.AutoSync)Enum.Parse(typeof(System.Data.Linq.Mapping.AutoSync), xe.Attribute("AutoSync").Value)
				: (IsDbGenerated) ? AutoSync.OnInsert : AutoSync.Default;
			UpdateCheck = (xe.Attribute("UpdateCheck") == null)
				? UpdateCheck.Always
				: (System.Data.Linq.Mapping.UpdateCheck)Enum.Parse(typeof(UpdateCheck), xe.Attribute("UpdateCheck").Value);
			MemberAttributes = DecodeAccess((String)xe.Attribute("AccessModifier")) | DecodeModifier((String)xe.Attribute("Modifier"));

		}

		private MemberAttributes DecodeAccess(string accessModifier)
		{
			switch (accessModifier)
			{
				case "Private": return MemberAttributes.Private;
				case "Internal": return MemberAttributes.Assembly;
				case "Protected": return MemberAttributes.Family;
				case "ProtectedInternal": return MemberAttributes.FamilyAndAssembly;
				default: return MemberAttributes.Public;
			}
		}

		private MemberAttributes DecodeModifier(string modifier)
		{
			switch (modifier)
			{
				case "Virtual": return 0;
				case "Override": return MemberAttributes.Override;
				default: return MemberAttributes.Final;
			}
		}
	}
}
