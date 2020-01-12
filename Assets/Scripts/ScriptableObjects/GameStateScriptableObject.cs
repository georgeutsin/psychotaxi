using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState", order = 1)]
public class GameStateSriptableObject : ScriptableObject
{
    public int coinCount;
    public float timeLeft = 30f;
    public float maxTime = 30f;

}
