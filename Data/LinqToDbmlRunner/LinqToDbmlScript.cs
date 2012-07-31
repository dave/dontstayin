namespace LinqToDbmlRunner
{
    using schemas.microsoft.com.linqtosql.dbml.Item2007;
    using System;

    public abstract class LinqToDbmlScript
    {
        protected LinqToDbmlScript()
        {
        }

        public abstract void Apply(XRoot root);
    }
}

