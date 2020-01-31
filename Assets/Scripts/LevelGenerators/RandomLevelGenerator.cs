using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGenerator : LevelGenerator
{
    GameObject o;
    public RandomLevelGenerator(LevelGenerator source) : base(source)
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
            if (curPosn_LC > gameState.nextGasLocation)
            {
                o = gasPool.Next();
                UpdateGasLocation();
                DynamicRoadObject.ResetObject(o);
                o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
                continue;
            }

            if (Random.value < difficulty.obstacleSpawnProbability)
            {
                o = obstaclePool.Next();
                DynamicRoadObject.ResetObject(o);
                o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
                continue;
            }

            if (Random.value < difficulty.coinSpawnProbability)
            {
                o = coinPool.Next();
                DynamicRoadObject.ResetObject(o);
                o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
                continue;
            }
        }
    }
}
