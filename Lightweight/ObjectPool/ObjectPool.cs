using System.Collections.Concurrent;

namespace Lightweight.ObjectPool
{
    public class ObjectPool<T> where T : new()
    {
        private readonly ConcurrentBag<T> _items = new ConcurrentBag<T>();
        private int _counter = 0;
        private int _MAX = 15;

        public ObjectPool() { }

        public ObjectPool(int size)
        {
            this._MAX = size;
        }

        public T Get()
        {
            T item;
            if (_items.TryTake(out item))
            {
                this._counter--;
            }
            else
            {
                item = new T();
                _items.Add(item);
                this._counter++;
            }
            return item;
        }

        public void Release(T item)
        {
            if (this._counter < this._MAX)
            {
                this._items.Add(item);
                this._counter++;
            }
        }
    }
}
