using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.General.ObjectPool
{
    public class PoolContainerByType<T> where T : Component
    {
        private readonly PoolObjectInformation<T>[] _objectsInfo;
        private readonly Transform _poolContainer;
        protected readonly Dictionary<Type, ObjectPool<T>> _pools;

        protected PoolContainerByType(PoolObjectInformation<T>[] poolObjectInfos, Transform poolContainer)
        {
            _poolContainer = poolContainer;
            _pools = new Dictionary<Type, ObjectPool<T>>();
            _objectsInfo = poolObjectInfos;
            InitializePools();
        }
        
        private void InitializePools()
        {
            foreach (var info in _objectsInfo)
            {
                var poolData = new PoolData<T>
                {
                    size = info.PoolSize,
                    container = _poolContainer,
                    prefab = info.Prefab
                };
                
                _pools.Add(info.Prefab.GetType(), new ObjectPool<T>(poolData));
            }
        }

        protected T GetObjectFromPoolByType(Type type)
        {
            return _pools[type].GetElement();
        }

        public void ReturnObjectToPool(T gameObject)
        {
            _pools[gameObject.GetType()].ReturnElementToPool(gameObject);
        }
    }
}