using JvmSharp.Loader;
using JvmSharp.Loader.AttributeInfo;
using JvmSharp.Code.Instructions;
using JvmSharp.Runtime;
using JvmSharp.Java;
using JvmSharp.Java.Primitive;
using System.Diagnostics;

namespace JvmSharp.Code
{
    internal static class CodeExecuter
    {
        public static IObject? ExecuteMethod(MethodInfo method, uint? thisHandle, ConstantPool cp, IRuntimeContext context)
        {
            var codeAtrribute = method.FindAttribute<CodeAttribute>();
            var reader = new CodeReader(codeAtrribute.Code);
            var frame = new Frame(
                thisHandle,
                stackalloc uint[codeAtrribute.MaxLocals],
                stackalloc uint[codeAtrribute.MaxStack]
            );
            return ExecuteCode(reader, cp, ref frame, context);
        }

        private static IObject? ExecuteCode(CodeReader codeReader, ConstantPool cp, ref Frame frame, IRuntimeContext context)
        {
            while (!codeReader.IsEnd)
            {
                var ope = (OpeCode)codeReader.NextByte();
                Debug.WriteLine($"{ope}");
                switch (ope)
                {
                    case OpeCode.aastore:
                        break;
                    case OpeCode.aconst_null:
                        break;
                    case OpeCode.aload:
                        break;
                    case OpeCode.aload_0:
                        Instruction.Aload_0(ref frame);
                        break;
                    case OpeCode.aload_1:
                        Instruction.Aload_1(ref frame);
                        break;
                    case OpeCode.aload_2:
                        break;
                    case OpeCode.aload_3:
                        break;
                    case OpeCode.anewarray:
                        break;
                    case OpeCode.areturn:
                        break;
                    case OpeCode.arraylength:
                        break;
                    case OpeCode.astore:
                        break;
                    case OpeCode.astore_0:
                        break;
                    case OpeCode.astore_1:
                        Instruction.Astore_1(ref frame);
                        break;
                    case OpeCode.astore_2:
                        break;
                    case OpeCode.astore_3:
                        break;
                    case OpeCode.athrow:
                        break;
                    case OpeCode.baload:
                        break;
                    case OpeCode.bastore:
                        break;
                    case OpeCode.bipush:
                        Instruction.Bipush(codeReader, ref frame);
                        break;
                    case OpeCode.caload:
                        break;
                    case OpeCode.castore:
                        break;
                    case OpeCode.checkcast:
                        break;
                    case OpeCode.d2f:
                        break;
                    case OpeCode.d2i:
                        break;
                    case OpeCode.d2l:
                        break;
                    case OpeCode.dadd:
                        break;
                    case OpeCode.daload:
                        break;
                    case OpeCode.dastore:
                        break;
                    case OpeCode.dcmpg:
                        break;
                    case OpeCode.dcmpl:
                        break;
                    case OpeCode.dconst_0:
                        break;
                    case OpeCode.dconst_1:
                        break;
                    case OpeCode.ddiv:
                        break;
                    case OpeCode.dload:
                        break;
                    case OpeCode.dload_0:
                        break;
                    case OpeCode.dload_1:
                        break;
                    case OpeCode.dload_2:
                        break;
                    case OpeCode.dload_3:
                        break;
                    case OpeCode.dmul:
                        break;
                    case OpeCode.dneg:
                        break;
                    case OpeCode.drem:
                        break;
                    case OpeCode.dreturn:
                        break;
                    case OpeCode.dstore:
                        break;
                    case OpeCode.dstore_0:
                        break;
                    case OpeCode.dstore_1:
                        break;
                    case OpeCode.dstore_2:
                        break;
                    case OpeCode.dstore_3:
                        break;
                    case OpeCode.dsub:
                        break;
                    case OpeCode.dup:
                        Instruction.Dup(ref frame);
                        break;
                    case OpeCode.dup_x1:
                        break;
                    case OpeCode.dup_x2:
                        break;
                    case OpeCode.dup2:
                        break;
                    case OpeCode.dup2_x1:
                        break;
                    case OpeCode.dup2_x2:
                        break;
                    case OpeCode.f2d:
                        break;
                    case OpeCode.f2i:
                        break;
                    case OpeCode.f2l:
                        break;
                    case OpeCode.fadd:
                        break;
                    case OpeCode.faload:
                        break;
                    case OpeCode.fastore:
                        break;
                    case OpeCode.fcmpg:
                        break;
                    case OpeCode.fcmpl:
                        break;
                    case OpeCode.fconst_0:
                        break;
                    case OpeCode.fconst_1:
                        break;
                    case OpeCode.fconst_2:
                        break;
                    case OpeCode.fdiv:
                        break;
                    case OpeCode.fload:
                        break;
                    case OpeCode.fload_0:
                        break;
                    case OpeCode.fload_1:
                        break;
                    case OpeCode.fload_2:
                        break;
                    case OpeCode.fload_3:
                        break;
                    case OpeCode.fmul:
                        break;
                    case OpeCode.fneg:
                        break;
                    case OpeCode.frem:
                        break;
                    case OpeCode.freturn:
                        break;
                    case OpeCode.fstore:
                        break;
                    case OpeCode.fstore_0:
                        break;
                    case OpeCode.fstore_1:
                        break;
                    case OpeCode.fstore_2:
                        break;
                    case OpeCode.fstore_3:
                        break;
                    case OpeCode.fsub:
                        break;
                    case OpeCode.getfield:
                        Instruction.Getfield(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.getstatic:
                        Instruction.Getstatic(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.@goto:
                        break;
                    case OpeCode.goto_w:
                        break;
                    case OpeCode.i2b:
                        break;
                    case OpeCode.i2c:
                        break;
                    case OpeCode.i2d:
                        break;
                    case OpeCode.i2f:
                        break;
                    case OpeCode.i2l:
                        break;
                    case OpeCode.i2s:
                        break;
                    case OpeCode.iadd:
                        Instruction.Iadd(ref frame);
                        break;
                    case OpeCode.iaload:
                        break;
                    case OpeCode.iand:
                        break;
                    case OpeCode.iastore:
                        break;
                    case OpeCode.iconst_m1:
                        break;
                    case OpeCode.iconst_0:
                        break;
                    case OpeCode.iconst_1:
                        break;
                    case OpeCode.iconst_2:
                        break;
                    case OpeCode.iconst_3:
                        break;
                    case OpeCode.iconst_4:
                        break;
                    case OpeCode.iconst_5:
                        break;
                    case OpeCode.idiv:
                        break;
                    case OpeCode.if_acmpeq:
                        break;
                    case OpeCode.if_acmpne:
                        break;
                    case OpeCode.if_icmpeq:
                        break;
                    case OpeCode.if_icmpne:
                        break;
                    case OpeCode.if_icmplt:
                        break;
                    case OpeCode.if_icmpge:
                        break;
                    case OpeCode.if_icmpgt:
                        break;
                    case OpeCode.if_icmple:
                        break;
                    case OpeCode.ifeq:
                        break;
                    case OpeCode.ifne:
                        break;
                    case OpeCode.iflt:
                        break;
                    case OpeCode.ifge:
                        break;
                    case OpeCode.ifgt:
                        break;
                    case OpeCode.ifle:
                        break;
                    case OpeCode.ifnonnull:
                        break;
                    case OpeCode.ifnull:
                        break;
                    case OpeCode.iinc:
                        break;
                    case OpeCode.iload:
                        break;
                    case OpeCode.iload_0:
                        break;
                    case OpeCode.iload_1:
                        break;
                    case OpeCode.iload_2:
                        break;
                    case OpeCode.iload_3:
                        break;
                    case OpeCode.imul:
                        break;
                    case OpeCode.instanceof:
                        break;
                    case OpeCode.invokedynamic:
                        break;
                    case OpeCode.invokeinterface:
                        break;
                    case OpeCode.invokespecial:
                        Instruction.Invokespecial(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.invokestatic:
                        break;
                    case OpeCode.invokevirtual:
                        Instruction.Invokevirtual(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.ior:
                        break;
                    case OpeCode.irem:
                        break;
                    case OpeCode.ireturn:
                        return Instruction.Ireturn(ref frame);
                    case OpeCode.ishl:
                        break;
                    case OpeCode.ishr:
                        break;
                    case OpeCode.istore:
                        break;
                    case OpeCode.istore_0:
                        break;
                    case OpeCode.istore_1:
                        break;
                    case OpeCode.istore_2:
                        break;
                    case OpeCode.istore_3:
                        break;
                    case OpeCode.isub:
                        break;
                    case OpeCode.iushr:
                        break;
                    case OpeCode.ixor:
                        break;
                    case OpeCode.jsr:
                        break;
                    case OpeCode.jsr_w:
                        break;
                    case OpeCode.l2d:
                        break;
                    case OpeCode.l2f:
                        break;
                    case OpeCode.l2i:
                        break;
                    case OpeCode.ladd:
                        break;
                    case OpeCode.laload:
                        break;
                    case OpeCode.land:
                        break;
                    case OpeCode.lastore:
                        break;
                    case OpeCode.lcmp:
                        break;
                    case OpeCode.lconst_0:
                        break;
                    case OpeCode.lconst_1:
                        break;
                    case OpeCode.ldc:
                        Instruction.Ldc(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.ldc_w:
                        break;
                    case OpeCode.ldc2_w:
                        break;
                    case OpeCode.ldiv:
                        break;
                    case OpeCode.lload:
                        break;
                    case OpeCode.lload_0:
                        break;
                    case OpeCode.lload_1:
                        break;
                    case OpeCode.lload_2:
                        break;
                    case OpeCode.lload_3:
                        break;
                    case OpeCode.lmul:
                        break;
                    case OpeCode.lneg:
                        break;
                    case OpeCode.lookupswitch:
                        break;
                    case OpeCode.lor:
                        break;
                    case OpeCode.lrem:
                        break;
                    case OpeCode.lreturn:
                        break;
                    case OpeCode.lshl:
                        break;
                    case OpeCode.lshr:
                        break;
                    case OpeCode.lstore:
                        break;
                    case OpeCode.lstore_0:
                        break;
                    case OpeCode.lstore_1:
                        break;
                    case OpeCode.lstore_2:
                        break;
                    case OpeCode.lstore_3:
                        break;
                    case OpeCode.lsub:
                        break;
                    case OpeCode.lushr:
                        break;
                    case OpeCode.lxor:
                        break;
                    case OpeCode.monitorenter:
                        break;
                    case OpeCode.monitorexit:
                        break;
                    case OpeCode.multianewarray:
                        break;
                    case OpeCode.@new:
                        Instruction.New(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.newarray:
                        break;
                    case OpeCode.nop:
                        break;
                    case OpeCode.pop:
                        break;
                    case OpeCode.pop2:
                        break;
                    case OpeCode.putfield:
                        Instruction.Putfield(codeReader, cp, ref frame, context);
                        break;
                    case OpeCode.putstatic:
                        break;
                    case OpeCode.ret:
                        break;
                    case OpeCode.@return:
                        return null;
                    case OpeCode.saload:
                        break;
                    case OpeCode.sastore:
                        break;
                    case OpeCode.sipush:
                        break;
                    case OpeCode.swap:
                        break;
                    case OpeCode.tableswitch:
                        break;
                    case OpeCode.wide:
                        break;
                    default:
                        break;
                }
            }

            return null;
        }
    }
}
