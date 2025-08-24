using System.Collections.Generic;

//Для третьего задания
namespace _Game.Scripts.Delegates
{
    public delegate T CreateFunc<T>();
    public delegate void ActionOnActivate<T>(T item);
    public delegate void ActionOnDeactivate<T>(T obj);
    public delegate void ActionOnDestroy<T>(T obj);

    public class ObjectPoolExample<T>
    {
        private Stack<T> _pool;
        private ActionOnActivate<T> _activateItem;
        private ActionOnDeactivate<T> _deactivateItem;
        private ActionOnDestroy<T> _destroy;
        private CreateFunc<T> _createItem;

        public int CountAll { get; private set; }
        public int CountInactive => _pool.Count;
        public int CountActive => CountAll - CountInactive;

        public ObjectPoolExample(
            ActionOnActivate<T> activateItem,
            ActionOnDeactivate<T> deactivateItem,
            ActionOnDestroy<T> destroy,
            CreateFunc<T> createItem)
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