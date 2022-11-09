using System;

namespace ScaffoldDotnet.MongoProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DocumentName : Attribute
    {
        public DocumentName(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
