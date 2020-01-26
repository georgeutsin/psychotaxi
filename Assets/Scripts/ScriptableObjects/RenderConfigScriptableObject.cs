using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RenderConfig", menuName = "Game/RenderConfig", order = 1)]
public class RenderConfigScriptableObject : ScriptableObject
{
    public float renderDistance = 1.25f;
    public float buffer = 0.25f;
    public float[] lanePosns = { 0.015f, 0f, -0.015f };
    public float minFOV = 68;
    public float maxFOV = 89;
    public float fovSpeed = 20f;
    public AnimationCurve FOVCurve;
}
