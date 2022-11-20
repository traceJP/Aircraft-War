using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public interface IObjectPool<T> where T : Object
    {
        void Push(T obj);

        T Get();

        bool IsEmpty();

        void Destroy();
    }

    public abstract class ObjectPool<T> : IObjectPool<T> where T : Object
    {
        protected Queue<T> Pool;

        protected ObjectPool()
        {
            Pool = new Queue<T>();
        }

        public virtual void Push(T obj)
        {
            Pool.Enqueue(obj);
        }

        public virtual T Get()
        {
            return default;
        }

        public virtual bool IsEmpty()
        {
            return !IsNotEmpty();
        }

        protected bool IsNotEmpty()
        {
            return Pool.Any();
        }

        public virtual void Destroy()
        {
            Pool = null;
        }

    }
}