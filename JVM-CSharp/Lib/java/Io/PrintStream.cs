using JvmSharp.Java;

namespace java.io
{
    internal class PrintStream : IClassDefinition
    {
        public string FullName => "java/io/PrintStream";

        public IReadOnlyList<IClassDefinition> SuperClassDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions { get; } = new Dictionary<string, IClassDefinition>();

        public IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IObject> StaticFields => throw new NotImplementedException();

        public IReadOnlySet<string> Methods { get; } = new HashSet<string>() { "<init>", "println" };

        public IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg)
        {
            if (name == "<init>")
            {
                return null;
            }
            if (name == "println")
            {
                PrintLn(context, arg!);
                return null;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static void PrintLn(IRuntimeContext context, IObject arg)
        {
            var javaStr = arg.Definition.InvokeMethod(arg, "toString", context, null);
            if (javaStr is IString str)
            {
                Console.WriteLine(str.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public IObject GetStaticField(string name)
        {
            throw new NotImplementedException();
        }
    }
}
