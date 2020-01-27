using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LevelDifficulty", menuName = "Game/LevelDifficulty", order = 1)]
public class LevelDifficultyScriptableObject : ScriptableObject
{
    public float gameLength = 5000f;
    public int numberOfLevels = 100;
    public float minLevelTime = 15f; // in seconds
    public float maxLevelTime = 1200f; // in seconds
    public AnimationCurve levelLengthCurve;

    public float baseSpeed = 0.1f;
    public float maxSpeedMultiplier = 10f;
    public AnimationCurve levelSpeedMuliplierCurve;

    public float maxObstacleWeight = 5f;
    public AnimationCurve obstacleWeightMultiplierCurve;

    public float obstacleSeparation = 0.1f;

    public float obstacleSpawnProbability = 0.6f; // todo make this a curve
    public float coinSpawnProbability = 0.3f; // todo make this a curve
    public float gasSpawnProbability = 0.05f; // todo tweak to make this fun
    public float gasTimeAdded = 15f;
    public float timeBetweenGas = 12f;

    public float[] levelDists;
    public float[] cumLevelDists;
    public int curLevel;
    public float gameProgressionMultiplier = 3.0f;

    void OnEnable()
    {
        levelDists[0] = 0;
        cumLevelDists[0] = 0;

        for (int i = 1; i < levelDists.Length; i++)
        {
            float d = GetMaxSpeed(i) * CurveUtil.NormalizedSample(levelLengthCurve, i, 1f, numberOfLevels, minLevelTime, maxLevelTime);
            levelDists[i] = d / gameProgressionMultiplier;
            cumLevelDists[i] = cumLevelDists[i - 1] + d;
        }
        curLevel = 0;
    }

    public int GetLevelFromDistance(float dist)
    {
        if (curLevel >= numberOfLevels)
        {
            return curLevel;
        }

        while(dist > cumLevelDists[curLevel])
        {
            curLevel += 1;
        }

        return curLevel;
    }

    public float GetObstacleSeparation(int level)
    {
        return obstacleSeparation;
    }

    public float GetMaxSpeed(int level)
    {
        return baseSpeed * CurveUtil.MultiplierSample(
            levelSpeedMuliplierCurve,
            (float)level / numberOfLevels,
            maxSpeedMultiplier);
    }
}