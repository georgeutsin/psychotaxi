using System.Collections.Generic;
using UnityEngine;


public class CircularObjectPool
{
    List<GameObject> pool;
    GameObject obj;
    Transform parent;

    const int defaultSize = 20;
    int startIdx = 0;
    int endIdx = 0;

    public CircularObjectPool(GameObject obj, Transform parent)
        : this(obj, parent, defaultSize) { }

    public CircularObjectPool(GameObject obj, Transform parent, int poolSize)
    {
        this.obj = obj;
        this.parent = parent;

        this.pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject o = Object.Instantiate(obj, parent);
            o.SetActive(false);
            pool.Add(o);
        }
    }

    public GameObject Next()
    {
        GameObject o;
        if (endIdx < startIdx + pool.Count) {
            o = pool[endIdx % pool.Count];
        } else {
            o = Object.Instantiate(obj, parent);
            if (endIdx == pool.Count)
            {
                pool.Add(o);
            }
            else
            {
                pool.Insert(endIdx % pool.Count, o);
                startIdx += 1;
            }
        }

        endIdx += 1;
        o.SetActive(true);
        return o;
    }

    public GameObject First()
    {
        return pool[startIdx % pool.Count];
    }

    public void ReturnFirst()
    {
        pool[startIdx % pool.Count].SetActive(false);
        startIdx += 1;
    }
}


