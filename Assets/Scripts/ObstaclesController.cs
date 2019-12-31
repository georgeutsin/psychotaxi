using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController
{
    public float distanceIncrement = 20f; // todo: make this a scriptable object

    float curPosn = 40f;
    CircularObjectPool pool;

    public ObstaclesController(GameObject[] obstacles, Transform parent)
    {
        pool = new CircularMixedObjectPool(obstacles, parent, 20);
    }

    public void RenderUntil(float targetPosn)
    {
        while (curPosn < targetPosn)
        {
            GameObject o = pool.Next();
            ResetObject(o);
            o.transform.position = new Vector3(curPosn, 0f, 0f);
            curPosn += distanceIncrement;
        }
    }

    public void FreeUntil(float targetPosn)
    {
        while (pool.First().transform.position.x < targetPosn)
        {
            pool.ReturnFirst();
        }
    }

    private void ResetObject(GameObject o)
    {
        o.transform.rotation = Quaternion.identity;
        o.transform.position = Vector3.zero;
    }
}
