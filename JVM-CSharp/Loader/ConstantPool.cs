using JvmSharp.Loader.CpInfo;

namespace JvmSharp.Loader
{
    internal class ConstantPool
    {
        private readonly List<ICpInfo> cpInfos;

        public ConstantPool(IEnumerable<ICpInfo> cpInfos)
        {
            this.cpInfos = cpInfos.ToList();
        }

        public ICpInfo Get(int index)
        {
            if (index - 1 >= cpInfos.Count) throw new IndexOutOfRangeException(nameof(index));
            return cpInfos[index - 1];
        }

        public T GetAs<T>(int index) where T : class, ICpInfo
        {
            return Get(index) as T ?? throw new InvalidCastException($"CP info {cpInfos[index].GetType()} is not {typeof(T)}");
        }

        public string GetUtf8Text(int index) => GetAs<Utf8Info>(index).Text;
    }
}
