using System.Runtime.InteropServices;

namespace JvmSharp.Code
{
    internal ref struct Frame
    {
        private const uint True = 1;
        private const uint False = 0;

        /// <summary>
        /// DO NOT TOUCH THIS DIRECTLY! USE <see cref="FrameExtension.GetLocalsRef(ref Frame)"/>.
        /// </summary>
        public VariableArray _locals;

        /// <summary>
        /// DO NOT TOUCH THIS DIRECTLY! USE <see cref="FrameExtension.GetStackRef(ref Frame)"/>.
        /// </summary>
        public VariableStack _stack;

        public Frame(uint? thisHandle, Span<uint> localsSpan, Span<uint> stackSpan)
        {
            _locals = new VariableArray(localsSpan);
            _stack = new VariableStack(stackSpan);
            if (thisHandle.HasValue)
            {
                _locals.Set(0, thisHandle.Value);
            }
        }

        public ref struct VariableArray
        {
            Span<uint> locals;

            public VariableArray(Span<uint> locals)
            {
                this.locals = locals;
            }

            public bool GetBoolean(int index) => locals[index] == True;
            public byte GetByte(int index) => (byte)locals[index];
            public char GetChar(int index) => (char)locals[index];
            public short GetShort(int index) => (short)locals[index];
            public int GetInt(int index) => (int)locals[index];
            public uint GetUint(int index) => locals[index];
            public float GetFloat(int index) => new UintFloat(locals[index]).floatValue;
            public long GetLong(int index) => new UintLongDouble(locals[index], locals[index + 1]).longValue;
            public double GetDouble(int index) => new UintLongDouble(locals[index], locals[index + 1]).doubleValue;

            public void Set(int index, bool value) => locals[index] = value ? True : False;
            public void Set(int index, byte value) => locals[index] = value;
            public void Set(int index, char value) => locals[index] = value;
            public void Set(int index, short value) => locals[index] = (uint)value;
            public void Set(int index, int value) => locals[index] = (uint)value;
            public void Set(int index, uint value) => locals[index] = value;
            public void Set(int index, float value) => locals[index] = new UintFloat(value).uintValue;
            public void Set(int index, long value)
            {
                var uintValues = new UintLongDouble(value);
                locals[index] = uintValues.uintValue1;
                locals[index + 1] = uintValues.uintValue2;
            }
            public void Set(int index, double value)
            {
                var uintValues = new UintLongDouble(value);
                locals[index] = uintValues.uintValue1;
                locals[index + 1] = uintValues.uintValue2;
            }
        }

        public ref struct VariableStack
        {
            readonly Span<uint> stack;
            int index;

            public VariableStack(Span<uint> stack)
            {
                this.stack = stack;
                this.index = 0;
            }

            public bool PopBoolean() => PopUint() == True;
            public byte PopByte() => (byte)PopUint();
            public char PopChar() => (char)PopUint();
            public short PopShort() => (short)PopUint();
            public int PopInt() => (int)PopUint();
            public uint PopUint() => stack[--index];
            public float PopFloat() => new UintFloat(PopUint()).floatValue;
            public long PopLong() => new UintLongDouble(PopUint(), PopUint()).longValue;
            public double PopDouble() => new UintLongDouble(PopUint(), PopUint()).doubleValue;

            public void Push(bool value) => Push(value ? True : False);
            public void Push(byte value) => Push((uint)value);
            public void Push(char value) => Push((uint)value);
            public void Push(short value) => Push((uint)value);
            public void Push(int value) => Push((uint)value);
            public void Push(uint value) => stack[index++] = value;
            public void Push(float value) => Push(new UintFloat(value).uintValue);
            public void Push(long value)
            {
                var uintValues = new UintLongDouble(value);
                Push(uintValues.uintValue2);
                Push(uintValues.uintValue1);
            }
            public void Push(double value)
            {
                var uintValues = new UintLongDouble(value);
                Push(uintValues.uintValue2);
                Push(uintValues.uintValue1);
            }

            public void Dup()
            {
                var top = stack[index - 1];
                Push(top);
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct UintFloat
        {
            [FieldOffset(0)]
            public uint uintValue;
            [FieldOffset(0)]
            public float floatValue;

            public UintFloat(uint value) => uintValue = value;
            public UintFloat(float value) => floatValue = value;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct UlongDouble
        {
            [FieldOffset(0)]
            public ulong ulongValue;
            [FieldOffset(0)]
            public double doubleValue;

            public UlongDouble(ulong value) => ulongValue = value;
            public UlongDouble(double value) => doubleValue = value;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct UintLongDouble
        {
            [FieldOffset(0)]
            public uint uintValue1;
            [FieldOffset(1)]
            public uint uintValue2;
            [FieldOffset(0)]
            public long longValue;
            [FieldOffset(0)]
            public double doubleValue;

            public UintLongDouble(uint value1, uint value2) => (uintValue1, uintValue2) = (value1, value2);
            public UintLongDouble(long value) => longValue = value;
            public UintLongDouble(double value) => doubleValue = value;
        }
    }

    internal static class FrameExtension
    {
        public static ref Frame.VariableArray GetLocalsRef(this ref Frame frame) => ref frame._locals;
        public static ref Frame.VariableStack GetStackRef(this ref Frame frame) => ref frame._stack;
    }
}
