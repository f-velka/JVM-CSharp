using java.io;
using JvmSharp.Java;
using JvmSharp.Runtime;

namespace java.lang
{
    internal class System : IClassDefinition
    {
        public string FullName => "java/lang/System";

        public IReadOnlyList<IClassDefinition> SuperClassDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions { get; } = new Dictionary<string, IClassDefinition>();

        public IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions { get; } = new Dictionary<string, IClassDefinition>()
        {
            { "out", new PrintStream() },
        };

        public IReadOnlyDictionary<string, IObject> StaticFields { get; }

        public IReadOnlySet<string> Methods => throw new NotImplementedException();

        public System()
        {
            StaticFields = new Dictionary<string, IObject>()
            {
                { "out", ObjectFactory.Create(StaticFieldDefinitions["out"]) },
            };
        }

        public IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg)
        {
            throw new NotImplementedException();
        }
    }
}
