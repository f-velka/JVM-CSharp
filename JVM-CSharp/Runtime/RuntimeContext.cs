using JvmSharp.Java;
using JvmSharp.Java.Primitive;
using JvmSharp.Loader;
using JvmSharp.RuntimeExceptions;

namespace JvmSharp.Runtime
{
    internal class RuntimeContext : IRuntimeContext
    {
        private readonly Dictionary<string, IObject> staticObjects = new();

        private readonly Dictionary<string, IClassDefinition> classDefinitions = new();

        public RuntimeContext()
        {
            RegisterLib("java/lang/System");
        }

        private void RegisterLib(string className)
        {
            var type = Type.GetType(className.Replace("/", "."))
                ?? throw new ClassNotFoundException(className);
            var def = Activator.CreateInstance(type) as IClassDefinition
                ?? throw new InvalidOperationException("failed to create instance");
            staticObjects[className] = ObjectFactory.Create(def);
        }

        public void AddClass(string className, ClassFile classFile)
        {
            // FIXME: this...?
            var def = new ClassDefinition(className, classFile, this);
            classDefinitions[className] = def;
        }

        public IObject GetStaticField(string className, string fieldName)
        {
            if (staticObjects.TryGetValue(className, out var obj))
            {
                return obj.Definition.StaticFields[fieldName];
            }

            throw new NotImplementedException($"{className}#{fieldName}");
        }

        public IObject ToJavaString(string value) => ObjectFactory.CreateString(value);

        private static readonly IClassDefinition IntDef = new IntDefinition();
        public IClassDefinition GetPrimitiveTypeDefinition(JavaType type)
        {
            if (type == JavaType.Int)
            {
                return IntDef;
            }
            throw new NotImplementedException(type.ToString());
        }

        public IClassDefinition GetClassDefinition(string name)
        {
            if (classDefinitions.TryGetValue(name, out var def))
            {
                return def;
            }
            throw new NoClassDefFoundError(name);
        }

        public IObject CreateInstance(string className) => ObjectFactory.Create(GetClassDefinition(className));
    }
}
