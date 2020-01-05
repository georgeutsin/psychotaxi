using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUtil
{
    //static List<float> levelDistances;
    //static List<float> cumulativeLevelDistances;
    //static LevelDifficultyScriptableObject difficulty;
    //static int curLevel;

    //[RuntimeInitializeOnLoadMethod]
    //static void OnRuntimeMethodLoad()
    //{
    //    difficulty = Resources.Load<LevelDifficultyScriptableObject>("ScriptableObjects/LevelDifficulty");
    //    Debug.Log(difficulty.baseSpeed);
    //}

    //static void BuildLevelDists()
    //{
    //    levelDistances[0] = 0;
    //    cumulativeLevelDistances[0] = 0;
    //    Debug.Log("LOL");

    //    for (int i = 1; i <= difficulty.numberOfLevels; i++)
    //    {

    //        float d = difficulty.GetMaxSpeed(i) * Mathf.FloorToInt(CurveUtil.NormalizedSample(difficulty.levelLengthCurve, i, difficulty.minLevelTime, difficulty.maxLevelTime, 1f, difficulty.numberOfLevels));
    //        Debug.Log(d);
    //        levelDistances[i] = d;
    //        cumulativeLevelDistances[i] = cumulativeLevelDistances[i - 1] + d;
    //    }
    //    curLevel = 0;
    //}
}
