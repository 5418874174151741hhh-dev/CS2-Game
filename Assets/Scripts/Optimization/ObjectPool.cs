using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 通用对象池 - 减少GC压力
/// </summary>
public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool;
    private T prefab;
    private int initialSize;
    private Transform poolParent;

    public ObjectPool(T prefab, int initialSize = 10, Transform poolParent = null)
    {
        this.prefab = prefab;
        this.initialSize = initialSize;
        this.poolParent = poolParent;
        this.pool = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            T instance = Object.Instantiate(prefab);
            if (poolParent != null)
                instance.transform.SetParent(poolParent);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }

        Debug.Log($"[ObjectPool] 对象池已创建，初始大小: {initialSize}");
    }

    /// <summary>
    /// 从池中获取对象
    /// </summary>
    public T Get(Vector3 position = default, Quaternion rotation = default)
    {
        T instance = pool.Count > 0 ? pool.Dequeue() : Object.Instantiate(prefab);
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);
        return instance;
    }

    /// <summary>
    /// 将对象返回到池中
    /// </summary>
    public void Return(T instance)
    {
        instance.gameObject.SetActive(false);
        pool.Enqueue(instance);
    }

    /// <summary>
    /// 获取池中剩余对象数
    /// </summary>
    public int GetAvailableCount() => pool.Count;
}
