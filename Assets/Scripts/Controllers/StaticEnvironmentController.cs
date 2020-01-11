using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnvironmentController : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject roadSegment;
    public GameObject roadParent;

    public RenderConfigScriptableObject config;

    RoadController roadCtl;

    void Start()
    {
        roadCtl = new RoadController(roadSegment, roadParent.transform);
    }

    void Update()
    {
        float curPosn = playerTracker.transform.position.x;
        float targetPosn = curPosn + config.renderDistance;

        roadCtl.FreeUntil(curPosn - config.buffer);
        roadCtl.RenderUntil(targetPosn + config.buffer);
    }
}
