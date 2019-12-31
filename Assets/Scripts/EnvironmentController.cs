using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject roadSegment;
    public GameObject roadParent;
    public GameObject[] obstacles;
    public GameObject obstacleParent;

    public float renderDistance;

    float buffer = 50f;

    RoadController roadCtl;
    ObstaclesController obstacleCtl;

    void Start()
    {
        roadCtl = new RoadController(roadSegment, roadParent.transform);
        obstacleCtl = new ObstaclesController(obstacles, obstacleParent.transform);

        roadCtl.RenderUntil(renderDistance + buffer);
        obstacleCtl.RenderUntil(renderDistance + buffer);
    }

    void Update()
    {
        float curPosn = playerTracker.transform.position.x;
        float targetPosn = curPosn + renderDistance;

        roadCtl.FreeUntil(curPosn - buffer);
        roadCtl.RenderUntil(targetPosn + buffer);

        obstacleCtl.RenderUntil(targetPosn + buffer / 2);
    }
}
