using JvmSharp.Loader.AttributeInfo;
using JvmSharp.Extension;
using JvmSharp.Loader.CpInfo;
using System.Text;
using JvmSharp.RuntimeExceptions;
using JvmSharp.Runtime;
using JvmSharp.Java;

namespace JvmSharp.Loader
{
    internal static class ClassLoader
    {
        private static readonly byte[] CafeBabe = new byte[] { 0xca, 0xfe, 0xba, 0xbe };

        public static ClassFile Load(string path, RuntimeContext context)
        {
            var classFile = ReadClassFile(path);
            context.AddClass(Path.GetFileNameWithoutExtension(path), classFile);
            return classFile;
        }

        public static ClassFile ReadClassFile(string path)
        {
            using var input = File.Open(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(input, Encoding.UTF8, false);

            var magic = reader.ReadBytes(4);
            if (!magic.SequenceEqual(CafeBabe))
            {
                throw new InvalidFormatException("Cannot find the magic word");
            }
            var minorVersion = reader.ReadUInt16BE();
            var majorVersion = reader.ReadUInt16BE();

            var constantPoolCount = reader.ReadUInt16BE();
            var cpInfos = new List<ICpInfo>();
            for (int i = 0; i < constantPoolCount - 1; i++)
            {
                var cpInfo = ReadCpInfo(reader);
                cpInfos.Add(cpInfo);
            }
            var cp = new ConstantPool(cpInfos);

            var accessFlags = reader.ReadUInt16BE();
            var thisClass = reader.ReadUInt16BE();
            var superClass = reader.ReadUInt16BE();

            var interfacesCount = reader.ReadUInt16BE();
            var interfaces = new List<ushort>();
            for (int i = 0; i < interfacesCount; i++)
            {
                interfaces.Add(reader.ReadUInt16BE());
            }

            var fieldCount = reader.ReadUInt16BE();
            var fields = new List<FieldInfo>();
            for (int i = 0; i < fieldCount; i++)
            {
                var fieldInfo = ReadFieldInfo(reader, cp);
                fields.Add(fieldInfo);
            }

            var methodsCount = reader.ReadUInt16BE();
            var methods = new List<MethodInfo>();
            for (int i = 0; i < methodsCount; i++)
            {
                var methodInfo = ReadMethodInfo(reader, cp);
                methods.Add(methodInfo);
            }

            var attributeCount = reader.ReadUInt16BE();
            var attributes = new List<IAttributeInfo>();
            for (int i = 0; i < attributeCount; i++)
            {
                attributes.Add(ReadAttributeInfo(reader, cp));
            }

            if (reader.PeekChar() >= 0)
            {
                throw new InvalidFormatException("input is not at EOF");
            }
            return new(
                magic,
                minorVersion,
                majorVersion,
                cp,
                accessFlags,
                thisClass,
                superClass,
                interfaces,
                fields,
                methods,
                attributes
            );
        }

        private static ICpInfo ReadCpInfo(BinaryReader reader)
        {
            var tag = reader.ReadByte();
            switch ((ConstantKind)tag)
            {
                case ConstantKind.Utf8:
                    var length = reader.ReadUInt16BE();
                    var bytes = reader.ReadBytes(length);
                    // TODO: modified UTF-8
                    var text = Encoding.UTF8.GetString(bytes);
                    return new Utf8Info(bytes, text);
                case ConstantKind.Integer:
                    break;
                case ConstantKind.Float:
                    break;
                case ConstantKind.Long:
                    break;
                case ConstantKind.Double:
                    break;
                case ConstantKind.Class:
                    return new ClassInfo(reader.ReadUInt16BE());
                case ConstantKind.String:
                    return new StringInfo(reader.ReadUInt16BE());
                case ConstantKind.Fieldref:
                    return new FieldRefInfo(reader.ReadUInt16BE(), reader.ReadUInt16BE());
                case ConstantKind.Methodref:
                    return new MethodrefInfo(reader.ReadUInt16BE(), reader.ReadUInt16BE());
                case ConstantKind.InterfaceMethodref:
                    return new InterfaceMethodrefInfo(reader.ReadUInt16BE(), reader.ReadUInt16BE());
                case ConstantKind.NameAndType:
                    return new NameAndTypeInfo(reader.ReadUInt16BE(), reader.ReadUInt16BE());
                case ConstantKind.MethodHandle:
                    break;
                case ConstantKind.MethodType:
                    break;
                case ConstantKind.Dynamic:
                    break;
                case ConstantKind.InvokeDynamic:
                    break;
                default:
                    throw new InvalidFormatException($"invalid CP info tag {tag}");
            }

            throw new NotImplementedException($"CP info {tag} is not implemented");
        }

        private static FieldInfo ReadFieldInfo(BinaryReader reader, ConstantPool cp)
        {
            var accessFlags = reader.ReadUInt16BE();
            var nameIndex = reader.ReadUInt16BE();
            var descriptorIndex = reader.ReadUInt16BE();
            var attributeCount = reader.ReadUInt16BE();
            var attributes = new List<IAttributeInfo>();
            for (int i = 0; i < attributeCount; i++)
            {
                attributes.Add(ReadAttributeInfo(reader, cp));
            }
            return new(accessFlags, nameIndex, descriptorIndex, attributes);
        }

        private static MethodInfo ReadMethodInfo(BinaryReader reader, ConstantPool cp)
        {
            var accessFlags = reader.ReadUInt16BE();
            var nameIndex = reader.ReadUInt16BE();
            var descriptorIndex = reader.ReadUInt16BE();
            var attributeCount = reader.ReadUInt16BE();
            var attributes = new List<IAttributeInfo>();
            for (int i = 0; i < attributeCount; i++)
            {
                var attribute = ReadAttributeInfo(reader, cp);
                attributes.Add(attribute);
            }
            return new(accessFlags, nameIndex, descriptorIndex, attributes);
        }

        private static IAttributeInfo ReadAttributeInfo(BinaryReader reader, ConstantPool cp)
        {
            var attributeNameIndex = reader.ReadUInt16BE();
            var attributeNameInfo = cp.GetAs<Utf8Info>(attributeNameIndex);
            var attributeLength = reader.ReadUInt32BE();

            if (!Enum.TryParse(attributeNameInfo.Text, out AttributeKind kind))
            {
                throw new InvalidFormatException($"invalid attribute name: {attributeNameInfo.Text}");
            };
            switch (kind)
            {
                case AttributeKind.ConstantValue:
                    break;
                case AttributeKind.Code:
                    var maxStack = reader.ReadUInt16BE();
                    var maxLocals = reader.ReadUInt16BE();
                    uint codeLength = reader.ReadUInt32BE();
                    // limit code length to the maximum value of int
                    var code = reader.ReadBytes(Convert.ToInt32(codeLength));
                    var exceptionTableLength = reader.ReadUInt16BE();
                    var exceptionTable = new List<ExceptionTableEntry>();
                    for (int i = 0; i < exceptionTableLength; i++)
                    {
                        var startPc = reader.ReadUInt16BE();
                        var endPc = reader.ReadUInt16BE();
                        var handerPc = reader.ReadUInt16BE();
                        var catchType = reader.ReadUInt16BE();
                        exceptionTable.Add(new(startPc, endPc, handerPc, catchType));
                    }
                    var attributesCount = reader.ReadUInt16BE();
                    var attributes = new List<IAttributeInfo>();
                    for (int i = 0; i < attributesCount; i++)
                    {
                        var attribute = ReadAttributeInfo(reader, cp);
                        attributes.Add(attribute);
                    }
                    return new CodeAttribute(maxStack, maxLocals, code, exceptionTable, attributes);
                case AttributeKind.StackMapTable:
                    break;
                case AttributeKind.Exceptions:
                    break;
                case AttributeKind.InnerClasses:
                    break;
                case AttributeKind.EnclosingMethod:
                    break;
                case AttributeKind.Synthetic:
                    break;
                case AttributeKind.Signature:
                    break;
                case AttributeKind.SourceFile:
                    var sourceFileIndex = reader.ReadUInt16();
                    return new SourceFileAttribute(sourceFileIndex);
                case AttributeKind.SourceDebugExtension:
                    break;
                case AttributeKind.LineNumberTable:
                    var lineNumberTableLength = reader.ReadUInt16BE();
                    var lineNumberTable = new List<LineNumberEntry>();
                    for (int i = 0; i < lineNumberTableLength; i++)
                    {
                        var startPc = reader.ReadUInt16BE();
                        var lineNumber = reader.ReadUInt16BE();
                        lineNumberTable.Add(new(startPc, lineNumber));
                    }
                    return new LineNumberTableAttribute(lineNumberTable);
                case AttributeKind.LocalVariableTable:
                    break;
                case AttributeKind.LocalVariableTypeTable:
                    break;
                case AttributeKind.Deprecated:
                    break;
                case AttributeKind.RuntimeVisibleAnnotations:
                    break;
                case AttributeKind.RuntimeInvisibleAnnotations:
                    break;
                case AttributeKind.RuntimeVisibleParameterAnnotations:
                    break;
                case AttributeKind.RuntimeInvisibleParameterAnnotations:
                    break;
                case AttributeKind.RuntimeVisibleTypeAnnotations:
                    break;
                case AttributeKind.RuntimeInvisibleTypeAnnotations:
                    break;
                case AttributeKind.AnnotationDefault:
                    break;
                case AttributeKind.BootstrapMethods:
                    break;
                case AttributeKind.MethodParameters:
                    break;
                default:
                    throw new InvalidFormatException($"invalid attribute kind {kind}");
            }

            throw new NotImplementedException($"Attribute info {kind} is not implemented");
        }

    }
}
