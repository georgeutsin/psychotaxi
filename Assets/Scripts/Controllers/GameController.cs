#pragma warning disable 0436
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Analytics;
using Unity.RemoteConfig;

public class GameController : MonoBehaviour
{

    public GameObject GameOverScreen;
    public GameOverMenu gameOverMenu;
    public GameObject GameOverlayScreen;
    public GameStateScriptableObject gameState;
    public PlayerController player;
    public CameraController mainCamera;
    public DynamicEnvironmentController dynamicEnv;
    public StaticEnvironmentController staticEnv;
    public LevelDifficultyScriptableObject difficulty;
    public Shop shop;

    UnityAction gameOverEventListener;
    UnityAction newGameListener;
    UnityAction shopListener;

    bool tagbullActivitiesAvailable;
    bool isGameRunning;

    public struct userAttributes { }

    // Optionally declare variables for any custom app attributes:
    public struct appAttributes { }

    void Awake()
    {
         // Add a listener to apply settings when successfully retrieved: 
        ConfigManager.FetchCompleted += ApplyRemoteSettings;

        // Set the user’s unique ID:
        ConfigManager.SetCustomUserID(SystemInfo.deviceUniqueIdentifier);

        // Fetch configuration setting from the remote service: 
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    // Start is called before the first frame update
    void Start()
    {
        tagbullActivitiesAvailable = false;
        gameOverEventListener = new UnityAction(GameOverTriggered);
        EventManager.StartListening("GameOver", gameOverEventListener);

        newGameListener = new UnityAction(NewGameTriggered);
        EventManager.StartListening("NewGame", newGameListener);

        shopListener = new UnityAction(ShopTriggered);
        EventManager.StartListening("ShopPressed", shopListener);

        gameOverMenu.HideButtons();

        gameState.UpdateMultipliers();
        shop.SetModelAndMesh();
    }

    void ApplyRemoteSettings (ConfigResponse configResponse) {
        // Conditionally update settings, depending on the response's origin:
        switch (configResponse.requestOrigin) {
            case ConfigOrigin.Default:
                // Debug.Log ("No settings loaded this session; using default values.");
                break;
            case ConfigOrigin.Cached:
                // Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                // Debug.Log ("New settings loaded this session; update values accordingly.");
                gameState.showUnityAds = ConfigManager.appConfig.GetBool ("SHOW_UNITY_ADS");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            dynamicEnv.CustomUpdate(player.transform.position.x);
        }
    }

    void GameOverTriggered()
    {
        isGameRunning = false;
        GameOverlayScreen.SetActive(false);
        int score = (int) (player.transform.position.x * 100);
        int highScore = StateManager.SetHighScore(score);
        int coins = gameState.coinCount;
        int totalCoins = StateManager.AddCoins(coins);
        gameOverMenu.UpdateText(score, highScore, coins, totalCoins);

        StartCoroutine(WaitGameOver(GameOverScreen, gameOverMenu));
    }

    IEnumerator WaitGameOver(GameObject GameOverScreen, GameOverMenu gameOverMenu)
    {
        AnalyticsEvent.GameOver();
        Coroutine req = StartCoroutine(GetRequest("https://tagbull-prod.appspot.com/activities/available"));
        yield return new WaitForSeconds(0.5f);
        GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        yield return req;
        gameOverMenu.ShowButtons(tagbullActivitiesAvailable, gameState.showUnityAds);
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string res = webRequest.downloadHandler.text;
            if (res == "{\"data\":true}")
            {
                AnalyticsEvent.ScreenVisit("TagBullOffer");
                tagbullActivitiesAvailable = true;
            }
            else
            {
                tagbullActivitiesAvailable = false;
            }
        }
    }

    void NewGameTriggered()
    {
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        AnalyticsEvent.GameStart();
        gameState.timeLeft = gameState.maxTime;
        gameState.coinCount = 0;
        gameState.gasLevel = 1;
        gameState.cameraView = GameStateScriptableObject.CameraView.Game;
        difficulty.Reset();
        player.NewGame();
        dynamicEnv.NewGame();
        isGameRunning = true;
    }

    public void MenuScreenTriggered()
    {
        AnalyticsEvent.ScreenVisit("MainMenu");
        gameState.cameraView = GameStateScriptableObject.CameraView.MainMenu;
        player.NewGame();
        dynamicEnv.Reset();
        mainCamera.MoveToMenuPosn();
    }

    public void ShopTriggered()
    {
        AnalyticsEvent.ScreenVisit("Shop");
        gameState.cameraView = GameStateScriptableObject.CameraView.Shop;
    }

    public void ResetStats()
    {
        StateManager.ResetAllStats();
        shop.SetModelAndMesh();
    }
}
