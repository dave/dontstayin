using System;
using System.Linq;
using Type = schemas.microsoft.com.linqtosql.dbml.Item2007.Type;
using XRoot = schemas.microsoft.com.linqtosql.dbml.Item2007.XRoot;
using schemas.microsoft.com.linqtosql.dbml.Item2007;
using System.Collections.Generic;

namespace LinqToDbmlRunner
{
    public static class Extensions
    {
        public static Table Table(this XRoot root, string tableName)
        {
            return root.Database.Table.First(t => t.Name == "dbo." + tableName);
        }
        public static Association Association(this Type type, string assocationName)
        {
            return type.Association.First(t => t.Name == assocationName);
        }
        public static Column Column(this Type type, string columnName)
        {
            return type.Column.First(t => t.Name == columnName);
        }
        public static Column Move(this Column column, Type source, Type dest)
        {
            source.Column.Remove(column);
            dest.Column.Add(column);
            return column;
        }
        public static Association Move(this Association association, Type source, Type dest)
        {
            source.Association.Remove(association);
            dest.Association.Add(association);
            return association;
        }

        public static T Apply<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }
        public static XRoot ExtrapolateSubclasses(this XRoot root)
        {
            foreach (var table in root.Database.Table)
            {
                var types = new List<Type>() { table.Type };
                var columnsInInheritors = (from c in table.Type.Column
                                           where c.Name.Contains("_")
                                           select c).ToArray();
                if (columnsInInheritors.Any())
                {
                    table.Type.Column.First(c => c.Name == table.Type.Name + "Type").IsDiscriminator = true;
                    table.Type.InheritanceCode = "0";
                    table.Type.IsInheritanceDefault = true;

                    foreach (var c in columnsInInheritors)
                    {
                        var parts = c.Name.Split('_');
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            string typeName = parts[i];
                            if (!types.Any(t => t.Name == typeName))
                            {
                                var newType = new Type()
                                {
                                    Name = typeName,
                                    InheritanceCode = types.Count.ToString()
                                };
                                types.Add(newType);
                                string parentTypeName = i == 0 ? table.Type.Name : parts[i - 1];
                                types.First(t => t.Name == parentTypeName).Type1.Add(newType);
                            }
                        }
                        table.Type.Column.Remove(c);
                        c.Member = parts[parts.Length - 1];
                        types.First(t => t.Name == parts[parts.Length - 2]).Column.Add(c);
                    }
                    var associationsOnInheritors = (from a in table.Type.Association
                                                    where (a.ThisKey ?? "").Contains("_")
                                                    select a).ToList();
                    foreach (var association in associationsOnInheritors)
                    {
                        var parts = association.ThisKey.Split('_');
                        association.ThisKey = parts[parts.Length - 1];
                        if (association.Member.Contains("_"))
                        {
                            association.Member = association.Member.Substring(association.Member.LastIndexOf("_"));
                        }
                        table.Type.Association.Remove(association);
                        types.First(t => t.Name == parts[parts.Length - 2]).Association.Add(association);
                    }
                }
            }
            return root;
        }

    }
}


//namespace LinqToDbmlRunner
//{
//    using schemas.microsoft.com.linqtosql.dbml.Item2007;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Runtime.CompilerServices;

//    public static class Extensions
//    {
//        public static T Apply<T>(this T t, Action<T> action)
//        {
//            action(t);
//            return t;
//        }

//        public static schemas.microsoft.com.linqtosql.dbml.Item2007.Association Association(this schemas.microsoft.com.linqtosql.dbml.Item2007.Type type, string assocationName, bool recurse)
//        {
//            schemas.microsoft.com.linqtosql.dbml.Item2007.Association assoc = type.Association.FirstOrDefault<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Association t) {
//                return t.Name == assocationName;
//            });
//            if ((assoc == null) && recurse)
//            {
//                foreach (schemas.microsoft.com.linqtosql.dbml.Item2007.Type child in type.Type1)
//                {
//                    assoc = child.Association(assocationName, recurse);
//                    if (assoc != null)
//                    {
//                        return assoc;
//                    }
//                }
//            }
//            return assoc;
//        }

//        public static schemas.microsoft.com.linqtosql.dbml.Item2007.Column Column(this schemas.microsoft.com.linqtosql.dbml.Item2007.Type type, string columnName)
//        {
//            return type.Column.First<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Column t) {
//                return (t.Name == columnName);
//            });
//        }

//        public static XRoot ExtrapolateSubclasses(this XRoot root)
//        {
//            List<schemas.microsoft.com.linqtosql.dbml.Item2007.Association> associationsNeedingToBePlacedIntoParentTypes = new List<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>();
//            using (IEnumerator<schemas.microsoft.com.linqtosql.dbml.Item2007.Table> unknownvar1 = root.Database.Table.GetEnumerator())
//            {
//                while (unknownvar1.MoveNext())
//                {
//                    schemas.microsoft.com.linqtosql.dbml.Item2007.Table table = unknownvar1.Current;
//                    List<schemas.microsoft.com.linqtosql.dbml.Item2007.Association> associationsOnForeignInheritedTypes = table.Type.Association.Where<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Association a) {
//                        return (a.OtherKey ?? "").Contains("_");
//                    }).ToList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>();
//                    foreach (schemas.microsoft.com.linqtosql.dbml.Item2007.Association association in associationsOnForeignInheritedTypes)
//                    {
//                        string[] parts = association.OtherKey.Split(new char[] { '_' });
//                        association.OtherKey = parts[parts.Length - 1];
//                        association.Type = parts[parts.Length - 2];
//                        association.Member = parts[parts.Length - 2] + "s";
//                    }
//                    List<schemas.microsoft.com.linqtosql.dbml.Item2007.Type> <>g__initLocal9 = new List<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>();
//                    <>g__initLocal9.Add(table.Type);
//                    List<schemas.microsoft.com.linqtosql.dbml.Item2007.Type> types = <>g__initLocal9;
//                    IEnumerable<int> inheritanceCodeQuery = table.Type.Type1.Where<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Type t) {
//                        return (t.InheritanceCode != null);
//                    }).Select<schemas.microsoft.com.linqtosql.dbml.Item2007.Type, int>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Type t) {
//                        return int.Parse(t.InheritanceCode);
//                    });
//                    int maxInheritanceCode = inheritanceCodeQuery.Any<int>() ? inheritanceCodeQuery.Max() : 0;
//                    schemas.microsoft.com.linqtosql.dbml.Item2007.Column[] columnsInInheritors = table.Type.Column.Where<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Column c) {
//                        return c.Name.Contains("_");
//                    }).ToArray<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>();
//                    if (columnsInInheritors.Any<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>())
//                    {
//                        <>c__DisplayClass1b unknownvar2<>8__locals1c;
//                        table.Type.Column.First<schemas.microsoft.com.linqtosql.dbml.Item2007.Column>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Column c) {
//                            return (c.Name == (table.Type.Name + "Type"));
//                        }).IsDiscriminator = true;
//                        table.Type.InheritanceCode = "0";
//                        table.Type.IsInheritanceDefault = true;
//                        foreach (schemas.microsoft.com.linqtosql.dbml.Item2007.Column c in columnsInInheritors)
//                        {
//                            <>c__DisplayClass1b unknownvar2<>8__locals1c = unknownvar2<>8__locals1c;
//                            c.CanBeNull = false;
//                            string[] parts = c.Name.Split(new char[] { '_' });
//                            for (int i = 0; i < (parts.Length - 1); i++)
//                            {
//                                <>c__DisplayClass1d unknownvar2<>8__locals1e;
//                                <>c__DisplayClass1d unknownvar2<>8__locals1e = unknownvar2<>8__locals1e;
//                                <>c__DisplayClass1b unknownvar2<>8__locals1c = unknownvar2<>8__locals1c;
//                                string typeName = parts[i];
//                                if (!types.Any<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Type t) {
//                                    return (t.Name == typeName);
//                                }))
//                                {
//                                    <>c__DisplayClass1f unknownvar2<>8__locals20;
//                                    <>c__DisplayClass1f unknownvar2<>8__locals20 = unknownvar2<>8__locals20;
//                                    <>c__DisplayClass1d unknownvar2<>8__locals1e = unknownvar2<>8__locals1e;
//                                    <>c__DisplayClass1b unknownvar2<>8__locals1c = unknownvar2<>8__locals1c;
//                                    schemas.microsoft.com.linqtosql.dbml.Item2007.Type <>g__initLocala = new schemas.microsoft.com.linqtosql.dbml.Item2007.Type();
//                                    <>g__initLocala.Name = typeName;
//                                    <>g__initLocala.InheritanceCode = (maxInheritanceCode + types.Count).ToString();
//                                    schemas.microsoft.com.linqtosql.dbml.Item2007.Type newType = <>g__initLocala;
//                                    types.Add(newType);
//                                    string parentTypeName = (i == 0) ? table.Type.Name : parts[i - 1];
//                                    types.First<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Type t) {
//                                        return (t.Name == parentTypeName);
//                                    }).Type1.Add(newType);
//                                }
//                            }
//                            table.Type.Column.Remove(c);
//                            c.Member = parts[parts.Length - 1];
//                            types.First<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Type t) {
//                                return (t.Name == parts[parts.Length - 2]);
//                            }).Column.Add(c);
//                        }
//                        List<schemas.microsoft.com.linqtosql.dbml.Item2007.Association> associationsOnInheritors = table.Type.Association.Where<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Association a) {
//                            return (a.ThisKey ?? "").Contains("_");
//                        }).ToList<schemas.microsoft.com.linqtosql.dbml.Item2007.Association>();
//                        foreach (schemas.microsoft.com.linqtosql.dbml.Item2007.Association association in associationsOnInheritors)
//                        {
//                            <>c__DisplayClass1b unknownvar2<>8__locals1c = unknownvar2<>8__locals1c;
//                            string[] parts = association.ThisKey.Split(new char[] { '_' });
//                            association.ThisKey = parts[parts.Length - 1];
//                            if (association.Member.Contains("_"))
//                            {
//                                association.Member = association.Member.Substring(association.Member.LastIndexOf("_") + 1);
//                            }
//                            table.Type.Association.Remove(association);
//                            types.First<schemas.microsoft.com.linqtosql.dbml.Item2007.Type>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Type t) {
//                                return (t.Name == parts[parts.Length - 2]);
//                            }).Association.Add(association);
//                        }
//                    }
//                }
//            }
//            return root;
//        }

//        public static schemas.microsoft.com.linqtosql.dbml.Item2007.Association Move(this schemas.microsoft.com.linqtosql.dbml.Item2007.Association association, schemas.microsoft.com.linqtosql.dbml.Item2007.Type source, schemas.microsoft.com.linqtosql.dbml.Item2007.Type dest)
//        {
//            source.Association.Remove(association);
//            dest.Association.Add(association);
//            return association;
//        }

//        public static schemas.microsoft.com.linqtosql.dbml.Item2007.Column Move(this schemas.microsoft.com.linqtosql.dbml.Item2007.Column column, schemas.microsoft.com.linqtosql.dbml.Item2007.Type source, schemas.microsoft.com.linqtosql.dbml.Item2007.Type dest)
//        {
//            source.Column.Remove(column);
//            dest.Column.Add(column);
//            return column;
//        }

//        public static schemas.microsoft.com.linqtosql.dbml.Item2007.Table Table(this XRoot root, string tableName)
//        {
//            return root.Database.Table.First<schemas.microsoft.com.linqtosql.dbml.Item2007.Table>(delegate (schemas.microsoft.com.linqtosql.dbml.Item2007.Table t) {
//                return (t.Name == ("dbo." + tableName));
//            });
//        }
//    }
//}

