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

    RoadController road;
    float roadRenderBuffer = 50f;

    void Start()
    {
        road = new RoadController(roadSegment, roadParent.transform);

        road.RenderUntil(renderDistance + roadRenderBuffer);
    }

    void Update()
    {
        float curPosn = playerTracker.transform.position.x;
        float targetPosn = curPosn + renderDistance;

        road.FreeUntil(curPosn - roadRenderBuffer);
        road.RenderUntil(targetPosn + roadRenderBuffer);
    }
}
