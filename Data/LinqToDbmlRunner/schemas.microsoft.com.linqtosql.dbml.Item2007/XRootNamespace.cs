namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    public class XRootNamespace
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private System.Xml.Linq.XDocument doc;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedElement rootObject;

        private XRootNamespace()
        {
        }

        public XRootNamespace(schemas.microsoft.com.linqtosql.dbml.Item2007.Database root)
        {
            this.doc = new System.Xml.Linq.XDocument(new object[] { root.Untyped });
            this.rootObject = root;
        }

        public static XRootNamespace Load(TextReader textReader)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Load(textReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRootNamespace Load(string xmlFile)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Load(xmlFile);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRootNamespace Load(XmlReader xmlReader)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Load(xmlReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRootNamespace Load(TextReader textReader, LoadOptions options)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Load(textReader, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRootNamespace Load(string xmlFile, LoadOptions options)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Load(xmlFile, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRootNamespace Parse(string text)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Parse(text);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRootNamespace Parse(string text, LoadOptions options)
        {
            XRootNamespace root = new XRootNamespace();
            root.doc = System.Xml.Linq.XDocument.Parse(text, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public virtual void Save(TextWriter textWriter)
        {
            this.doc.Save(textWriter);
        }

        public virtual void Save(string fileName)
        {
            this.doc.Save(fileName);
        }

        public virtual void Save(XmlWriter writer)
        {
            this.doc.Save(writer);
        }

        public virtual void Save(TextWriter textWriter, SaveOptions options)
        {
            this.doc.Save(textWriter, options);
        }

        public virtual void Save(string fileName, SaveOptions options)
        {
            this.doc.Save(fileName, options);
        }

        public schemas.microsoft.com.linqtosql.dbml.Item2007.Database Database
        {
            get
            {
                return (this.rootObject as schemas.microsoft.com.linqtosql.dbml.Item2007.Database);
            }
        }

        public System.Xml.Linq.XDocument XDocument
        {
            get
            {
                return this.doc;
            }
        }
    }
}

