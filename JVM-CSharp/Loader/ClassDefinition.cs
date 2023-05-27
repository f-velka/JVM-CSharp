using JvmSharp.Code;
using JvmSharp.Java;
using JvmSharp.Runtime;
using JvmSharp.RuntimeExceptions;

namespace JvmSharp.Loader
{
    internal class ClassDefinition : IClassDefinition
    {
        public string FullName { get; }

        public IReadOnlyList<IClassDefinition> SuperClassDefinitions { get; }

        public IReadOnlyDictionary<string, IClassDefinition> FieldDefinitions { get; }

        public IReadOnlyDictionary<string, IClassDefinition> StaticFieldDefinitions => throw new NotImplementedException();

        public IReadOnlyDictionary<string, IObject> StaticFields => throw new NotImplementedException();

        public IReadOnlySet<string> Methods { get; }

        private readonly ClassFile classFile;

        public ClassDefinition(string fullName, ClassFile classFile, RuntimeContext context)
        {
            FullName = fullName;
            SuperClassDefinitions = new List<IClassDefinition>() { new java.lang.Object() };
            this.classFile = classFile;

            var cp = classFile.Cp;
            FieldDefinitions = classFile.Fields
                .ToDictionary(
                x => cp.GetUtf8Text(x.NameIndex),
                // FIXME: reference types
                x => context.GetPrimitiveTypeDefinition(cp.GetUtf8Text(x.DescriptorIndex).ToJavaType()));
            Methods = classFile.Methods.Select(x => cp.GetUtf8Text(x.NameIndex)).ToHashSet();
        }

        public IObject? InvokeMethod(IObject thisObj, string name, IRuntimeContext context, IObject? arg)
        {
            var cp = classFile.Cp;
            var method = classFile.Methods.FirstOrDefault(x => cp.GetUtf8Text(x.NameIndex) == name)
                ?? throw new NoSuchMethodError(name);
            return CodeExecuter.ExecuteMethod(method, thisObj.Handle, cp, context);
        }
    }
}
