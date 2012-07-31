namespace schemas.microsoft.com.linqtosql.dbml.Item2007
{
    using Microsoft.Xml.Schema.Linq;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    public class XRoot
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private System.Xml.Linq.XDocument doc;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedElement rootObject;

        private XRoot()
        {
        }

        public XRoot(schemas.microsoft.com.linqtosql.dbml.Item2007.Database root)
        {
            this.doc = new System.Xml.Linq.XDocument(new object[] { root.Untyped });
            this.rootObject = root;
        }

        public static XRoot Load(TextReader textReader)
        {
            XRoot root = new XRoot();
            root.doc = System.Xml.Linq.XDocument.Load(textReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRoot Load(string xmlFile)
        {
            XRoot root = new XRoot();
            root.doc = System.Xml.Linq.XDocument.Load(xmlFile);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRoot Load(XmlReader xmlReader)
        {
            XRoot root = new XRoot();
            root.doc = System.Xml.Linq.XDocument.Load(xmlReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRoot Load(TextReader textReader, LoadOptions options)
        {
            XRoot root = new XRoot();
            root.doc = System.Xml.Linq.XDocument.Load(textReader, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRoot Load(string xmlFile, LoadOptions options)
        {
            XRoot root = new XRoot();
            root.doc = System.Xml.Linq.XDocument.Load(xmlFile, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRoot Parse(string text)
        {
            XRoot root = new XRoot();
            root.doc = System.Xml.Linq.XDocument.Parse(text);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if (typedRoot == null)
            {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }

        public static XRoot Parse(string text, LoadOptions options)
        {
            XRoot root = new XRoot();
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

