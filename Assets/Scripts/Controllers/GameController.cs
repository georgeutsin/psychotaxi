using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    public GameObject GameOverScreen;
    public GameObject GameOverlayScreen;
    public GameStateScriptableObject gameState;
    public PlayerController player;
    public CameraController mainCamera;
    public DynamicEnvironmentController dynamicEnv;
    public StaticEnvironmentController staticEnv;
    public LevelDifficultyScriptableObject difficulty;

    UnityAction gameOverEventListener;
    UnityAction newGameListener;
    UnityAction shopListener;

    // Start is called before the first frame update
    void Start()
    {
        gameOverEventListener = new UnityAction(GameOverTriggered);
        EventManager.StartListening("GameOver", gameOverEventListener);

        newGameListener = new UnityAction(NewGameTriggered);
        EventManager.StartListening("NewGame", newGameListener);

        shopListener = new UnityAction(ShopTriggered);
        EventManager.StartListening("ShopPressed", shopListener);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameOverTriggered()
    {
        GameOverScreen.SetActive(true);
        GameOverlayScreen.SetActive(false);
    }

    void NewGameTriggered()
    {
        gameState.timeLeft = gameState.maxTime;
        gameState.coinCount = 0;
        gameState.gasLevel = 1;
        gameState.cameraView = GameStateScriptableObject.CameraView.Game;
        difficulty.Reset();
        player.NewGame();
        dynamicEnv.NewGame();
        staticEnv.NewGame();
    }

    public void MenuScreenTriggered()
    {
        gameState.cameraView = GameStateScriptableObject.CameraView.MainMenu;
        player.NewGame();
        staticEnv.NewGame();
        dynamicEnv.Reset();
        mainCamera.MoveToMenuPosn();
    }

    public void ShopTriggered()
    {
        gameState.cameraView = GameStateScriptableObject.CameraView.Shop;
    }
}
