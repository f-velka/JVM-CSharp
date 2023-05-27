using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JvmSharp.Java
{
    internal interface IRuntimeContext
    {
        void AddClass(string className, ClassFile classFile);

        IObject GetStaticField(string className, string fieldName);

        IObject ToJavaString(string value);

        IClassDefinition GetPrimitiveTypeDefinition(JavaType type);

        IClassDefinition GetClassDefinition(string name);

        IObject CreateInstance(string className);
    }
}
