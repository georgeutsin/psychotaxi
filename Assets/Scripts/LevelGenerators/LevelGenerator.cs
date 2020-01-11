using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LevelGenerator
{
    protected LevelDifficultyScriptableObject difficulty;
    protected RenderConfigScriptableObject renderConfig;

    protected float curPosn_LC;
    protected MixedObjectPool pool;

    public LevelGenerator(
        GameObject[] obstacles,
        Transform parent,
        LevelDifficultyScriptableObject difficulty,
        RenderConfigScriptableObject renderConfig)
    {
        pool = new MixedObjectPool(obstacles, parent, 20);
        this.difficulty = difficulty;
        this.renderConfig = renderConfig;
    }

    public void Init(float curPosn_LC = 0.2f)
    {
        this.curPosn_LC = curPosn_LC;
    }

    abstract public void RenderUntil(float levelOffset, float targetPosn_GC);

}
