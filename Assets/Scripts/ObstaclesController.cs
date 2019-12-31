using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController
{
    float distanceIncrement = 20f; // todo: make this a scriptable object
    float spawnProbability = 0.6f; // todo: make this a scriptable object
    float[] lanePosns = { 3f, 0f, -3f }; // todo: make this a scriptable object

    float curPosn = 40f;
    MixedObjectPool pool;


    public ObstaclesController(GameObject[] obstacles, Transform parent)
    {
        pool = new MixedObjectPool(obstacles, parent, 20);
    }

    public void RenderUntil(float targetPosn)
    {
        while (curPosn < targetPosn)
        {
            SetObstacleLine();
            curPosn += distanceIncrement;
        }
    }

    private void SetObstacleLine()
    {
        foreach (float lanePosn in lanePosns)
        {
            if (Random.value < spawnProbability)
            {
                GameObject o = pool.Next();
                o.transform.position = new Vector3(curPosn, 0f, lanePosn);
            }
        }
    }
}
