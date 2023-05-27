namespace JvmSharp.Java
{
    internal static class ObjectStorage
    {
        private static readonly object lockObject = new();

        private static readonly Queue <uint> freeHandles =new();

        private static readonly Dictionary<uint, IObject> objects = new();

        private static uint nextHandle = 0;

        public static IObject Get(uint handle)
        {
            lock (lockObject)
            {
                if (objects.TryGetValue(handle, out var obj))
                {
                    return obj;
                }
            }

            throw new InvalidOperationException($"object with handle {handle} does not exist");
        }

        public static T GetAs<T>(uint handle) where T : IObject => (T)Get(handle);

        public static uint Add(IObject obj)
        {
            lock (lockObject)
            {
                uint handle;
                if (freeHandles.TryDequeue(out var h))
                {
                    handle = h;
                }
                else
                {
                    // max handle is uint.MaxValue - 1
                    if (nextHandle == uint.MaxValue)
                    {
                        throw new InvalidOperationException("no handle available");
                    }
                    handle = nextHandle++;
                }
                objects[handle] = obj;
                return handle;
            }
        }

        public static void Free(IObject obj)
        {
            lock (lockObject)
            {
                freeHandles.Enqueue(obj.Handle);
                objects.Remove(obj.Handle);
            }
        }
    }
}
