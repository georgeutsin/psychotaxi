using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController
{
    LevelDifficultyScriptableObject difficulty;
    RenderConfigScriptableObject renderConfig;

    float curPosn = 0.2f;
    MixedObjectPool pool;

    public ObstaclesController(
        GameObject[] obstacles,
        Transform parent,
        LevelDifficultyScriptableObject difficulty,
        RenderConfigScriptableObject renderConfig)
    {
        pool = new MixedObjectPool(obstacles, parent, 20);
        this.difficulty = difficulty;
        this.renderConfig = renderConfig;
    }

    public void RenderUntil(float targetPosn)
    {
        int level = difficulty.GetLevelFromDistance(curPosn);

        while (curPosn < targetPosn)
        {
            SetObstacleLine();
            level = difficulty.GetLevelFromDistance(curPosn);
            curPosn += difficulty.GetObstacleSeparation(level);
        }
    }

    void SetObstacleLine()
    {
        foreach (float lanePosn in renderConfig.lanePosns)
        {
            if (Random.value < difficulty.obstacleSpawnProbability)
            {
                GameObject o = pool.Next();
                o.transform.position = new Vector3(curPosn, 0f, lanePosn);
            }
        }
    }
}
