using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.ObjectPool
{
    public class ObjectPoolMono<T> where T : MonoBehaviour
    {
        private T _prefab;
        private bool _autoExpand;
        private List<T> _pool;

        public List<T> GetPool => _pool;

        public ObjectPoolMono(T prefab, int count, bool autoExpand = false)
        {
            _prefab = prefab;
            _autoExpand = autoExpand;
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(_prefab);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element)
        {
            foreach (var mono in _pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (_autoExpand)
                return CreateObject(true);

            throw new Exception($"There are no free elements in the pool with type {typeof(T)}");
        }

        public void ReturnAllItemsToPool()
        {
            _pool.ForEach(behaviour => behaviour.gameObject.SetActive(false));
        }
    }
}