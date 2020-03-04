using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// _GC = global/rendered/world coordinates
// _LC = local/logical/level coordinates
public class DynamicEnvironmentController : MonoBehaviour
{
    public GameObject playerTracker;
    public GameObject[] obstacles;
    public GameObject obstacleParent;
    public GameObject coin;
    public GameObject gas;
    public GameObject itemsParent;

    public LevelDifficultyScriptableObject difficulty;
    public RenderConfigScriptableObject config;
    public GameStateScriptableObject gameState;

    List<LevelGenerator> levelGenerators = new List<LevelGenerator>();
    LevelGenerator curGenerator;

    float levelOffset;
    float obsSpeed = DynamicRoadObject.obstacleSpeed;
    float targetPosn_LC;
    float targetPosn_GC;

    public float levelSegmentLength = 1f; // TODO tweak this so its fun
    float levelSegmentBoundary;
    float initialLevelDelayLength = 0.2f;

    public void NewGame()
    {
        Reset();
        curGenerator.RenderUntil(0, config.renderDistance + config.buffer);
        levelSegmentBoundary += levelSegmentLength;
    }

    public void Reset()
    {
        foreach (var generator in levelGenerators)
        {
            generator.Reset();
        }
        levelOffset = 0f;
        levelSegmentBoundary = 0f;
        gameState.nextGasLocation = 0.2f;
        // GasLocationUtil.SetNextGasLocation(gameState, difficulty);

        gameState.gasLevel = 1;

        PickLevelGenerator(initialLevelDelayLength);
    }

    void Start()
    {
        LevelGenerator source = new LevelGenerator(
            obstacles,
            obstacleParent.transform,
            coin,
            gas,
            itemsParent.transform,
            difficulty,
            config,
            gameState);

        levelGenerators.Add(new RandomLevelGenerator(source));
        levelGenerators.Add(new NoJumpLevelGenerator(source));
        Reset();
    }

    public void CustomUpdate(float curPosn_GC)
    {
        levelOffset += Time.deltaTime * obsSpeed;
        targetPosn_GC = curPosn_GC + config.renderDistance + config.buffer;
        targetPosn_LC = targetPosn_GC - levelOffset;

        if (targetPosn_LC > levelSegmentBoundary)
        {
            PickLevelGenerator(curGenerator.curPosn_LC);
            levelSegmentBoundary += levelSegmentLength;
        }

        curGenerator.RenderUntil(levelOffset, targetPosn_GC);
    }

    void PickLevelGenerator(float targetPosition_LC)
    {
        int generatorIdx = Random.Range(0, levelGenerators.Count);
        curGenerator = levelGenerators[generatorIdx];
        curGenerator.Init(targetPosition_LC);
    }
}
