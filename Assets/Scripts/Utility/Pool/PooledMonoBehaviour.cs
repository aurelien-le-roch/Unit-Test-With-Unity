using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField] private int _initialPoolSize = 50;

    public event Action<PooledMonoBehaviour> OnReturnToPool;
    public int InitialPoolSize => _initialPoolSize;


    public T Get<T>(bool enable = true) where T : PooledMonoBehaviour
    {
        var pool = Pool.GetPool(this);
        var pooledObject = pool.Get<T>();

        if (enable)
        {
            pooledObject.gameObject.SetActive(true);
        }

        return pooledObject;
    }

    public T Get<T>(Vector3 position,Quaternion rotation) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>();

        var pooledObjectTransform = pooledObject.transform;
        
        pooledObjectTransform.position = position;
        pooledObjectTransform.rotation = rotation;

        return pooledObject;
    }
    public T Get<T>(Vector3 position,Vector3 localScale,Quaternion rotation) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>();
        var pooledObjectTransform = pooledObject.transform;
        
        pooledObjectTransform.position = position;
        pooledObjectTransform.localScale = localScale;
        pooledObjectTransform.rotation = rotation;
        
        return pooledObject;
    }
    private void OnDisable()
    {
        OnReturnToPool?.Invoke(this);
    }
    
    public void ReturnToPool(float delay)
    {
        StartCoroutine(ReturnToPoolAfterSeconds(delay));
    }
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ReturnToPoolAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}