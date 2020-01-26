using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig", order = 1)]
public class PlayerConfigScriptableObject : ScriptableObject
{
    public LevelDifficultyScriptableObject difficulty;

    public float jumpSpeed = 40f;
    public float vehicleHeight = 2.0f;
    public float lateralSpeed = 40f;
    public float maxAcceleration = 15f;
    public float mass = 1f;

}
