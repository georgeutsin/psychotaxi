﻿using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState", order = 1)]
public class GameStateScriptableObject : ScriptableObject
{
    public int coinCount;
    public float timeLeft = 30f;
    public float maxTime = 30f;
}
