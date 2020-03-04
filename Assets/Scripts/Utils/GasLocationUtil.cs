using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLocationUtil
{
    public static void SetNextGasLocation(GameStateScriptableObject gameState, LevelDifficultyScriptableObject difficulty)
    {
        float timeLeft = difficulty.timeBetweenGas;

        while (timeLeft > 0f)
        {
            float timeSpentInLevel = difficulty.cumLevelDists[gameState.gasLevel] - gameState.nextGasLocation;
            gameState.nextGasLocation += Mathf.Min(timeLeft, timeSpentInLevel) * difficulty.GetMaxSpeed(gameState.gasLevel);
            if (timeSpentInLevel < timeLeft)
            {
                gameState.gasLevel += 1;
            }

            timeLeft -= timeSpentInLevel;
        }
    }
}
