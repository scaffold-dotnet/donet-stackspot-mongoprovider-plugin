using System;

namespace MongoProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DocumentName : System.Attribute
    {
        public DocumentName(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
