using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject editorOnly;
    public GameObject playerTracker;
    public RenderConfigScriptableObject config;
    public LevelDifficultyScriptableObject difficulty;
    public GameStateScriptableObject gameState;
    public GameObject[] Menus;
    public GameObject MainMenu;
    public float rotationSpeed = 5.0f;
    public float movementSpeed = 10.0f;

    public static Vector3 gamePosn = new Vector3(-0.0399f, 0.11205f, 0f);
    public static Vector3 gameRotn = new Vector3(46f, 90f, 0f);
    public static Vector3 mainMenuPosn = new Vector3(0.0183f, 0.0496f, -0.0443f);
    public static Vector3 mainMenuRotn = new Vector3(52.388f, -23.645f, 0f);
    public static Vector3 shopPosn = new Vector3(0.0336f, 0.0249f, -0.0443f);
    public static Vector3 shopRotn = new Vector3(2.57f, -35.881f, 0f);

    Camera cam;
    PlayerTracker player;

    // FOV calculations
    Vector2 target;
    Vector2 current;
    Vector2 result;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        player = playerTracker.GetComponent<PlayerTracker>();

        editorOnly.SetActive(false);


        foreach (GameObject menu in Menus)
        {
            menu.SetActive(false);
        }
        MainMenu.SetActive(true);
    }

    void LateUpdate()
    {
        //Debug.Log(Application.targetFrameRate);
        if (gameState.cameraView == GameStateScriptableObject.CameraView.Game)
        {
            RotateTo(gameRotn);
            if (Mathf.Abs(transform.rotation.eulerAngles.y - gameRotn.y) > 3f)
            {
                MoveTo(gamePosn + playerTracker.transform.position);
            }
            else
            {
                SetPositionTo(gamePosn + playerTracker.transform.position);
            }

            UpdateFOVToPlayerSpeed();
            return;
        }

        if (gameState.cameraView == GameStateScriptableObject.CameraView.MainMenu)
        {
            MoveTo(mainMenuPosn);
            RotateTo(mainMenuRotn);
            return;
        }

        if (gameState.cameraView == GameStateScriptableObject.CameraView.Shop)
        {
            MoveTo(shopPosn);
            RotateTo(shopRotn);
            return;
        }
    }

    void RotateTo(Vector3 viewRotn)
    {
        Quaternion targetQ = Quaternion.Euler(viewRotn);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, Time.deltaTime * rotationSpeed);
    }

    void MoveTo(Vector3 posn)
    {
        transform.position = Vector3.Slerp(transform.position, posn, Time.deltaTime * movementSpeed);
    }

    void SetPositionTo(Vector3 posn)
    {
        transform.position = posn;
    }

    public void MoveToMenuPosn()
    {
        MoveTo(mainMenuPosn);
    }

    public void UpdateFOVToPlayerSpeed()
    {
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
