using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryController
{
    float sceneryLength = 0.995f;
    float endPosn = -2f;
    CircularObjectPool pool;

    public SceneryController(GameObject citySegment, Transform parent)
    {
        pool = new CircularObjectPool(citySegment, parent, 2);
    }

    public void RenderUntil(float targetPosn)
    {
        while (endPosn < targetPosn)
        {
            GameObject o = pool.Next();
            o.transform.position = new Vector3(endPosn, 0f, 0f);
            endPosn += sceneryLength;
        }
    }

    public void FreeUntil(float targetPosn)
    {
        while (pool.First().transform.position.x + 1f < targetPosn)
        {
            pool.ReturnFirst();
        }
    }

    public void Reset()
    {
        endPosn = -2f;
        pool.Reset();
    }
}
