using JvmSharp.Java;

namespace java.lang
{
    internal class String : IClassDefinition
    {
        public string FullName => "java/lang/String";

        public IReadOnlyList<IClassDefinition> SuperClassDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions { get; } = new Dictionary<string, IClassDefinition>();

        public IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IObject> StaticFields => throw new NotImplementedException();

        public IReadOnlySet<string> Methods { get; } = new HashSet<string>() { "toString" };

        public IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg)
        {
            if (name == "toString")
            {
                return thisObj;
            }

            throw new NotImplementedException();
        }
    }
}
