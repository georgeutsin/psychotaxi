using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState", order = 1)]
public class GameStateScriptableObject : ScriptableObject
{
    public int coinCount;
    public float timeLeft = 30f;
    public float maxTime = 30f;
    public float nextGasLocation;
    public int gasLevel = 1;
    public bool isPaused = true;
    public CameraView cameraView;


    public enum CameraView
    {
        MainMenu,
        Shop,
        Game,
    }

    void OnEnable()
    {
        isPaused = true;
        coinCount = 0;
        gasLevel = 1;
        cameraView = CameraView.MainMenu;
    }
}
