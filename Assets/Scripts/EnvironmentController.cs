﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject roadSegment;
    public GameObject roadParent;
    public GameObject[] obstacles;
    public GameObject obstacleParent;

    public LevelDifficultyScriptableObject difficulty;
    public RenderConfigScriptableObject config;

    RoadController roadCtl;
    ObstaclesController obstacleCtl;

    void Start()
    {
        roadCtl = new RoadController(roadSegment, roadParent.transform);
        obstacleCtl = new ObstaclesController(obstacles, obstacleParent.transform, difficulty, config);

        roadCtl.RenderUntil(config.renderDistance + config.buffer);
        obstacleCtl.RenderUntil(config.renderDistance + config.buffer);

        //AnimationCurve inverse = CurveUtil.InvertCurve(difficulty.levelLengthCurve);
        //for (int i = 1; i < 100; i++) {
        //    Debug.Log(i + " - " + CurveUtil.NormalizedSample(inverse, i / 100f, 1f, 100f, 0f, difficulty.gameLength));
        //}
    }

    void Update()
    {
        float curPosn = playerTracker.transform.position.x;
        float targetPosn = curPosn + config.renderDistance;

        roadCtl.FreeUntil(curPosn - config.buffer);
        roadCtl.RenderUntil(targetPosn + config.buffer);

        obstacleCtl.RenderUntil(targetPosn + config.buffer / 2);
        //Debug.Log(difficulty.GetLevelFromDistance(curPosn));
    }
}
