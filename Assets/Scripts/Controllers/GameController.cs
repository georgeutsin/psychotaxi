using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {

    }

    void GameOverTriggered()
    {
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
        yield return new WaitForSeconds(0.5f);
        GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gameOverMenu.ShowButtons();
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
    }

    public void MenuScreenTriggered()
    {
        gameState.cameraView = GameStateScriptableObject.CameraView.MainMenu;
        player.NewGame();
        dynamicEnv.Reset();
        mainCamera.MoveToMenuPosn();
    }

    public void ShopTriggered()
    {
        gameState.cameraView = GameStateScriptableObject.CameraView.Shop;
    }

    public void ResetStats()
    {
        StateManager.ResetAllStats();
        shop.SetModelAndMesh();
    }
}
