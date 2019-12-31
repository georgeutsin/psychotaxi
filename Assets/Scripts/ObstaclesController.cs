using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController
{
    float distanceIncrement = 20f; // todo: make this a scriptable object
    float spawnProbability = 0.6f; // todo: make this a scriptable object

    float curPosn = 40f;
    CircularObjectPool pool;
    float[] lanePosns = { 3f, 0f, -3f };

    public ObstaclesController(GameObject[] obstacles, Transform parent)
    {
        pool = new CircularMixedObjectPool(obstacles, parent, 20);
    }

    public void RenderUntil(float targetPosn)
    {
        while (curPosn < targetPosn)
        {
            SetObstacleLine();
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

    private void SetObstacleLine()
    {
        foreach (float lanePosn in lanePosns)
        {
            if (Random.value < spawnProbability)
            {
                GameObject o = pool.Next();
                ResetObject(o);
                o.transform.position = new Vector3(curPosn, 0f, lanePosn);
            }
        }
    }
}
