using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RenderConfig", menuName = "Game/RenderConfig", order = 1)]
public class RenderConfigScriptableObject : ScriptableObject
{
    public float renderDistance = 250f;
    public float buffer = 50f;
    public float[] lanePosns = { 3f, 0f, -3f };
    public float minFOV = 68;
    public float maxFOV = 75;
    public float fovSpeed = 5f;
    public AnimationCurve FOVCurve;
}
