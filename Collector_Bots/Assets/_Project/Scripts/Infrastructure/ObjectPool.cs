using System;
using System.Collections.Generic;

public class ObjectPool<T>
{
    private Stack<T> _pool;
    private Action<T> _activateItem;
    private Action<T> _deactivateItem;
    private Action<T> _destroy;
    private Func<T> _createItem;

    public int AllCount { get; private set; }
    public int InactiveCount => _pool.Count;
    public int ActiveCount => AllCount - InactiveCount;

    public ObjectPool(
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
        T item;

        if (_pool.Count > 0)
        {
            item = _pool.Pop();
        }
        else
        {
            item = _createItem();
            AllCount++;
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
    
    public void PreWarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T item = _createItem();
            _deactivateItem(item);
            _pool.Push(item);
        }
        AllCount += count;
    }
}