namespace Asteroids2
{
    internal sealed class Pool<T> where T : class, IPoolable
    {
        public int Count
        {
            get => count;
        }

        private T[] storage;
        private int count;

        public Pool(int initialCapacity = 50)
        {
            storage = new T[initialCapacity];
            count = 0;
        }

        public void Add(T item)
        {
            if(count == storage.Length)
            {
                T[] newStorage = new T[storage.Length*2];
                storage.CopyTo(newStorage, 0);
                storage = newStorage;
            }
            storage[count] = item;
            count++;
        }

        public bool TryGet(out T item)
        {
            bool itemFound = false;

            item = null;
            for(int i = 0; i < count; i++)
            {
                if(storage[i].isFree)
                {
                    storage[i].isFree = false;
                    item = storage[i];
                    itemFound = true;
                    break;
                }
            }

            return itemFound;
        }
    }
}
