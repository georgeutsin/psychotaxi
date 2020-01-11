using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpLevelGenerator : LevelGenerator
{
    int gapIdx;
    public NoJumpLevelGenerator(
        GameObject[] obstacles,
        Transform parent,
        LevelDifficultyScriptableObject difficulty,
        RenderConfigScriptableObject renderConfig)
        : base(obstacles, parent, difficulty, renderConfig)
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

    void SetObstacleLine(float levelOffset)
    {
        for (int i = 0; i < renderConfig.lanePosns.Length; i++)
        {
            if (i == gapIdx)
            {
                continue;
            }

            float lanePosn = renderConfig.lanePosns[i];
            GameObject o = pool.Next();
            o.transform.position = new Vector3(curPosn_LC + levelOffset, 0f, lanePosn);
        }

        if (gapIdx == 0)
        {
            gapIdx += 1;
            return;
        }

        if (gapIdx == renderConfig.lanePosns.Length-1)
        {
            gapIdx -= 1;
            return;
        }

        gapIdx += Random.Range(0, 2) * 2 - 1;
    }
}
