using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    protected LevelDifficultyScriptableObject difficulty;
    protected RenderConfigScriptableObject renderConfig;

    protected float curPosn_LC;
    protected MixedObjectPool obstaclePool;
    protected ObjectPool coinPool;
    protected ObjectPool gasPool;

    public LevelGenerator(
        GameObject[] obstacles,
        Transform obstaclesParent,
        GameObject coin,
        GameObject gas,
        Transform itemsParent,
        LevelDifficultyScriptableObject difficulty,
        RenderConfigScriptableObject renderConfig)
    {
        obstaclePool = new MixedObjectPool(obstacles, obstaclesParent, 20);
        coinPool = new ObjectPool(coin, itemsParent, 20);
        gasPool = new ObjectPool(gas, itemsParent, 5);
        this.difficulty = difficulty;
        this.renderConfig = renderConfig;
        curPosn_LC = 0f;
    }

    public LevelGenerator(LevelGenerator source)
    {
        obstaclePool = source.obstaclePool;
        coinPool = source.coinPool;
        gasPool = source.gasPool;
        difficulty = source.difficulty;
        renderConfig = source.renderConfig;
    }

    public void Init(float curPosn_LC = 0.2f)
    {
        this.curPosn_LC = curPosn_LC;
    }

    virtual public void RenderUntil(float levelOffset, float targetPosn_GC)
    {

    }

}
