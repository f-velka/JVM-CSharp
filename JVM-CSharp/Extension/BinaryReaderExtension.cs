namespace JvmSharp.Extension
{
    internal static class BinaryReaderExtension
    {
        public static ushort ReadUInt16BE(this BinaryReader reader)
        {
            var data = reader.ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToUInt16(data, 0);
        }

        public static uint ReadUInt32BE(this BinaryReader reader)
        {
            var data = reader.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }
    }
}
