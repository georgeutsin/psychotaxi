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
    public DynamicEnvironmentController dynamicEnv;
    public StaticEnvironmentController staticEnv;

    private UnityAction gameOverEventListener;
    private UnityAction newGameListener;



    // Start is called before the first frame update
    void Start()
    {
        gameOverEventListener = new UnityAction(GameOverTriggered);
        EventManager.StartListening("GameOver", gameOverEventListener);

        newGameListener = new UnityAction(NewGameTriggered);
        EventManager.StartListening("NewGame", newGameListener);
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
        player.NewGame();
        dynamicEnv.NewGame();
        staticEnv.NewGame();
    }
}
