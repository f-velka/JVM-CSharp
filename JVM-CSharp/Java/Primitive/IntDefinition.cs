using JvmSharp.Runtime;

namespace JvmSharp.Java.Primitive
{
    internal class IntDefinition : IClassDefinition
    {
        public string FullName => "PrimitiveInt";

        public IReadOnlyList<IClassDefinition> SuperClassDefinitions { get; } = new List<IClassDefinition>();

        public IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions { get; } = new Dictionary<string, IClassDefinition>();

        public IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions { get; } = new Dictionary<string, IClassDefinition>();

        public IReadOnlyDictionary<string, IObject> StaticFields { get; } = new Dictionary<string, IObject>();

        public IReadOnlySet<string> Methods { get; } = new HashSet<string>();

        public IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg)
        {
            if (name == "<init>")
            {
                return null;
            }
            else if (name == "toString")
            {
                return context.ToJavaString(thisObj.GetPrimitiveValue<int>().ToString());
            }
            throw new NotImplementedException(name);
        }
    }
}
