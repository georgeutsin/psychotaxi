using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpLevelGenerator : LevelGenerator
{
    int gapIdx;
    GameObject o;

    public NoJumpLevelGenerator(LevelGenerator source) : base(source)
    {
        gapIdx = Random.Range(0, renderConfig.lanePosns.Length);
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

    protected void SetObstacleLine(float levelOffset)
    {
        for (int i = 0; i < renderConfig.lanePosns.Length; i++)
        {
            float lanePosn = renderConfig.lanePosns[i];

            if (i == gapIdx)
            {
                if (curPosn_LC > gameState.nextGasLocation)
                {
                    o = gasPool.Next();
                    UpdateGasLocation();
                    o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
                    continue;
                }

                o = coinPool.Next();
                o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
                continue;
            }

            o = obstaclePool.Next();
            DynamicRoadObject.ResetObject(o);
            o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
        }

        if (gapIdx == 0)
        {
            gapIdx += 1;
            //gapIdx += Random.Range(0, 2);
            return;
        }

        if (gapIdx == renderConfig.lanePosns.Length-1)
        {
            gapIdx -= 1;
            //gapIdx -= Random.Range(0, 2);
            return;
        }

        gapIdx += Random.Range(0, 2) * 2 - 1;
        //gapIdx += Random.Range(0, 3) - 1;
    }
}
