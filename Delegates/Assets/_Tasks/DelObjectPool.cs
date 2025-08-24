using System;
using System.Collections.Generic;

//Для второго задания
namespace _Game.Scripts.Delegates
{
    public class DelObjectPool<T>
    {
        private Stack<T> _pool;
        private Action<T> _activateItem;
        private Action<T> _deactivateItem;
        private Action<T> _destroy;
        private Func<T> _createItem;

        public int CountAll { get; private set; }
        public int CountInactive => _pool.Count;
        public int CountActive => CountAll - CountInactive;

        public DelObjectPool(
            Action<T> activateItem,
            Action<T> deactivateItem,
            Action<T> destroy,
            Func<T> createItem)
        {
            _activateItem = activateItem;
            _deactivateItem = deactivateItem;
            _destroy = destroy;
            _createItem = createItem;
            _pool = new Stack<T>();
        }

        public T GetItem()
        {
            T item = default;

            if (_pool.Count > 0)
            {
                item = _pool.Pop();
            }

            if (_pool.Count == 0)
            {
                item = _createItem();
                CountAll++;
            }

            _activateItem(item);
            return item;
        }

        public void ReleaseItem(T item)
        {
            _deactivateItem(item);
            _pool.Push(item);
        }

        public void Clear()
        {
            foreach (T item in _pool)
            {
                _destroy(item);
            }
        }
    }
}