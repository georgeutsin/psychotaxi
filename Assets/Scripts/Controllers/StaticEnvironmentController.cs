using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnvironmentController : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject roadSegment;
    public GameObject roadParent;
    public GameObject citySegment;
    public GameObject beachSegment;
    public GameObject surSegment;
    public GameObject forestSegment;
    public GameObject sceneryParent;
    public RenderConfigScriptableObject config;
    StaticElementController roadCtl;
    SceneryController sceneCtl;

    void Start()
    {
        roadCtl = new StaticElementController(roadSegment, roadParent.transform);
        GameObject[] scenes = {
            citySegment,
            beachSegment, beachSegment,  beachSegment,
            surSegment, surSegment, surSegment, surSegment,
            forestSegment, forestSegment, forestSegment, forestSegment,
            surSegment, surSegment, surSegment,
            citySegment, citySegment, citySegment,
            };
        sceneCtl = new SceneryController(scenes, sceneryParent.transform, 0.995f);
    }

    void Update()
    {
        float start = playerTracker.transform.position.x - config.buffer;
        float end = start + config.renderDistance + config.buffer;

        roadCtl.RenderFrom(start, end);
        sceneCtl.RenderFrom(start, end);
    }
}
