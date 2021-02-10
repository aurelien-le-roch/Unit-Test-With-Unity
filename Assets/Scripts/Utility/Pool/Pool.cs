﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonoBehaviour,Pool> _pools = new Dictionary<PooledMonoBehaviour, Pool>();
    
    private Queue<PooledMonoBehaviour> _objects = new Queue<PooledMonoBehaviour>();
    private PooledMonoBehaviour _prefab;

    private void OnDisable()
    {
        _pools.Clear();
        _objects.Clear();
    }

    public static Pool GetPool(PooledMonoBehaviour prefab)
    {
        if (_pools.ContainsKey(prefab))
            return _pools[prefab];

        var poolGameObject = new GameObject("Pool - " + prefab.name);
        var pool = poolGameObject.AddComponent<Pool>();

        pool._prefab = prefab;
        
        _pools.Add(prefab,pool);
        return pool;
    }
    
    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (_objects.Count == 0)
        {
            GrowPool();
        }

        var pooledObject = _objects.Dequeue();
        return pooledObject as T;
    }

    private void GrowPool()
    {
        for (int i = 0; i < _prefab.InitialPoolSize; i++)
        {
            var pooledObject = Instantiate(_prefab, transform, true) as PooledMonoBehaviour;
            pooledObject.gameObject.name += " " + i;

            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.gameObject.SetActive(false);
        }
    }

    private void AddObjectToAvailableQueue(PooledMonoBehaviour pooledMonoBehaviour)
    {
        pooledMonoBehaviour.transform.SetParent(transform);
        _objects.Enqueue(pooledMonoBehaviour);
    }
}