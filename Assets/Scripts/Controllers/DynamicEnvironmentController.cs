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
    float curPosn_GC;
    float targetPosn_LC;
    float targetPosn_GC;

    public float levelSegmentLength = 1f; // TODO tweak this so its fun
    float levelSegmentBoundary;
    float initialLevelDelayLength = 0.2f;

    void Start()
    {
        levelOffset = 0f;
        curPosn_GC = 0f;
        gameState.nextGasLocation = 0f;
        gameState.gasLevel = 1;
        GasLocationUtil.SetNextGasLocation(gameState, difficulty);

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

        PickLevelGenerator(initialLevelDelayLength);
        curGenerator.RenderUntil(0, config.renderDistance + config.buffer);
        levelSegmentBoundary += levelSegmentLength;
    }

    void Update()
    {
        levelOffset += Time.deltaTime * obsSpeed;
        curPosn_GC = playerTracker.transform.position.x;
        targetPosn_GC = curPosn_GC + config.renderDistance + config.buffer;
        targetPosn_LC = targetPosn_GC - levelOffset;

        if (targetPosn_LC > levelSegmentBoundary)
        {
            PickLevelGenerator(targetPosn_LC);
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
