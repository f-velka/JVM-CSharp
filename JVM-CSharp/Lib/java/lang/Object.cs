using JvmSharp.Java;
using JvmSharp.RuntimeExceptions;

namespace java.lang
{
    internal class Object : IClassDefinition
    {
        public string FullName => "java/lang/Object";

        public IReadOnlyList<IClassDefinition> SuperClassDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IObject> StaticFields => throw new NotImplementedException();

        public IReadOnlySet<string> Methods { get; } = new HashSet<string>() { "<init>"};

        public IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg)
        {
            if (name == "<init>")
            {
                return null;
            }
            else
            {
                throw new NoSuchMethodError(name);
            }
        }
    }
}
