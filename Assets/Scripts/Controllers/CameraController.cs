using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject editorOnly;
    public GameObject playerTracker;
    public RenderConfigScriptableObject config;
    public LevelDifficultyScriptableObject difficulty;
    public GameObject[] Menus;
    public GameObject MainMenu;

    public GameObject gamePosn;

    Vector3 gameViewPosition;
    Camera cam;
    PlayerTracker player;

    // FOV calculations
    Vector2 target;
    Vector2 current;
    Vector2 result;

    void Start()
    {
        gameViewPosition = transform.position;
        cam = GetComponent<Camera>();
        player = playerTracker.GetComponent<PlayerTracker>();

        editorOnly.SetActive(false);
        Application.targetFrameRate = 60;

        foreach (GameObject menu in Menus)
        {
            menu.SetActive(false);
        }
        MainMenu.SetActive(true);
    }

    void LateUpdate()
    {
        transform.position = gameViewPosition + playerTracker.transform.position;

        float step = config.fovSpeed * Time.deltaTime;
        float targetFOV = CurveUtil.NormalizedSample(
            config.FOVCurve,
            player.GetVelocity(),
            0.025f,
            difficulty.maxSpeedMultiplier * difficulty.baseSpeed,
            config.minFOV,
            config.maxFOV);
        target = new Vector2(targetFOV, 0);
        current = new Vector2(cam.fieldOfView, 0);
        result = Vector3.MoveTowards(current, target, step);

        cam.fieldOfView = result.x;
    }
}
