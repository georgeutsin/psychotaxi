using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    protected List<GameObject> pool;
    protected GameObject obj;
    protected Transform parent;

    protected const int defaultSize = 20;

    public ObjectPool(GameObject obj, Transform parent)
        : this(obj, parent, defaultSize) { }

    public ObjectPool(GameObject obj, Transform parent, int poolSize)
    {
        this.obj = obj;
        this.parent = parent;

        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject o = Object.Instantiate(obj, parent);
            o.SetActive(false);
            pool.Add(o);
        }
    }

    public GameObject Next()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        
        GameObject o = Object.Instantiate(GetObject(), parent); ;
        pool.Add(o);
        return o;
    }

	public void Return(GameObject o)
    {
		o.SetActive(false);
	}

    public void ResetPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].SetActive(false);
        }
    }

    protected virtual GameObject GetObject()
    {
        return obj;
    }
}

