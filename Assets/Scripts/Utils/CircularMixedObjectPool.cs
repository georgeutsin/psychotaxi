using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class CircularMixedObjectPool : CircularObjectPool
{
    GameObject[] objs;

    public CircularMixedObjectPool(GameObject[] objs, Transform parent)
        : this(objs, parent, defaultSize) { }

    public CircularMixedObjectPool(GameObject[] objs, Transform parent, int poolSize)
        : base(null, parent, 0)
    {
        this.objs = objs;

        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject o = Object.Instantiate(GetObject(), parent);
            o.SetActive(false);
            pool.Add(o);
        }
    }

    protected override GameObject GetObject()
    {
        return objs[Random.Range(0, objs.Length)];
    }

}
