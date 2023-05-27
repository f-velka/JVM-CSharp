using JvmSharp.Java;
using JvmSharp.Java.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JvmSharp.Runtime
{
    internal static class ObjectFactory
    {
        // FIXME:
        private static readonly IClassDefinition Int = new IntDefinition();
        public static IObject Create(IClassDefinition def)
        {
            if (def.FullName == Int.FullName)
            {
                return new PrimitiveObject<int>(def);
            }
            else
            {
                return new RuntimeObject(def);
            }
        }

        public static IObject CreateString(string value) => new StringObject(value);
    }

    file class RuntimeObject : IObject
    {
        public uint Handle { get; }

        public IClassDefinition Definition { get; }

        public IDictionary<string, IObject> Fields { get; }

        public RuntimeObject(IClassDefinition definition)
        {
            Handle = ObjectStorage.Add(this);

            Definition = definition;
            Fields = definition.FieldDefinitions
                .ToDictionary(x => x.Key, x => ObjectFactory.Create(x.Value));
        }

        ~RuntimeObject()
        {
            ObjectStorage.Free(this);
        }

        public override string ToString()
        {
            var fields = string.Join(",", Fields.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            return $"{Definition.FullName}, Handle={Handle}, Fields={{ {fields} }}";
        }
    }

    file class PrimitiveObject<T> : RuntimeObject where T : struct
    {
        public T Value { get; set; } = default;

        public PrimitiveObject(IClassDefinition definition) : base(definition)
        {
        }
    }

    internal static class PrimitiveObjectExtension
    {
        public static T GetPrimitiveValue<T>(this IObject obj) where T : struct
        {
            return (obj as PrimitiveObject<T>)?.Value
                ?? throw new InvalidOperationException("object is not primitive");
        }

        public static void SetPrimitiveValue<T>(this IObject obj, T value) where T : struct
        {
            if (obj is PrimitiveObject<T> primitive)
            {
                primitive.Value = value;
            }
            else
            {
                throw new InvalidOperationException("object is not primitive");
            }
        }
    }

    file class StringObject : RuntimeObject, IString
    {
        private static readonly IClassDefinition Def = new java.lang.String();

        public string Value { get; }

        public StringObject(string value) : base(Def)
        {
            Value = value;
        }
    }
}
