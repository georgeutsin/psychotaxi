using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGenerator : LevelGenerator
{
    public RandomLevelGenerator(
        GameObject[] obstacles,
        Transform parent,
        LevelDifficultyScriptableObject difficulty,
        RenderConfigScriptableObject renderConfig) 
        : base(obstacles, parent, difficulty, renderConfig)
    {

    }

    override public void RenderUntil(float levelOffset, float targetPosn_GC)
    {
        int level = difficulty.GetLevelFromDistance(curPosn_LC);

        while (curPosn_LC + levelOffset < targetPosn_GC)
        {
            SetObstacleLine(levelOffset);
            level = difficulty.GetLevelFromDistance(curPosn_LC);
            curPosn_LC += difficulty.GetObstacleSeparation(level);
        }
    }

    void SetObstacleLine(float levelOffset)
    {
        foreach (float lanePosn in renderConfig.lanePosns)
        {
            if (Random.value < difficulty.obstacleSpawnProbability)
            {
                GameObject o = pool.Next();
                o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
            }
        }
    }
}
