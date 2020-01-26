using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnvironmentController : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject roadSegment;
    public GameObject roadParent;
    public GameObject citySegment;
    public GameObject sceneryParent;


    public RenderConfigScriptableObject config;

    RoadController roadCtl;
    SceneryController sceneCtl;

    void Start()
    {
        roadCtl = new RoadController(roadSegment, roadParent.transform);
        sceneCtl = new SceneryController(citySegment, sceneryParent.transform);
    }

    void Update()
    {
        float curPosn = playerTracker.transform.position.x;
        float targetPosn = curPosn + config.renderDistance;

        roadCtl.FreeUntil(curPosn - config.buffer);
        roadCtl.RenderUntil(targetPosn + config.buffer);

        sceneCtl.FreeUntil(curPosn - config.buffer);
        sceneCtl.RenderUntil(targetPosn + config.buffer);
    }
}
