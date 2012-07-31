namespace LinqToDbmlRunner
{
    using schemas.microsoft.com.linqtosql.dbml.Item2007;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    throw new Exception("Usage: xsdrunner.console scriptAssembly inputXmlFile outputXmlFile");
                }
                List<System.Type> xsdScripts = new List<System.Type>(Assembly.LoadFrom(args[0]).GetTypes().Where<System.Type>(delegate (System.Type type) {
                    return typeof(LinqToDbmlScript).IsAssignableFrom(type);
                }));
                xsdScripts.Sort(delegate (System.Type t1, System.Type t2) {
                    return t1.Name.CompareTo(t2.Name);
                });
                XRoot root = XRoot.Load(args[1]);
                foreach (System.Type xsdScript in xsdScripts)
                {
                    ((LinqToDbmlScript) Activator.CreateInstance(xsdScript)).Apply(root);
                }
                root.Save(args[2]);
            }
            catch (Exception ex)
            {
                WriteMessage(Assembly.GetExecutingAssembly().FullName, "", Category.error, "1", ex.ToString());
            }
        }

        private static void WriteMessage(string origin, string subcategory, Category category, string code, string text)
        {
            Console.WriteLine("{0} : {1} {2} {3} : {4}", new object[] { origin, subcategory, category.ToString(), code, text });
        }

        private enum Category
        {
            error,
            warning
        }
    }
}

