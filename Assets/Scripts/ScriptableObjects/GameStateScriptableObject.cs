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

    public float accelerationMultiplier;
    public float bodyMultiplier;
    public float efficiencyMultiplier;

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
        UpdateMultipliers();
    }

    public void UpdateMultipliers()
    {
        accelerationMultiplier = (float)(StateManager.GetUpgradeLevel("CurAcceleration") - 1) / 10.0f;
        bodyMultiplier = (float)(StateManager.GetUpgradeLevel("CurBody") - 1) / 10.0f * 5.0f;
        efficiencyMultiplier = (float)(StateManager.GetUpgradeLevel("CurEfficiency") - 1) / 10.0f;

        if (StateManager.GetUpgradeLevel("SelectedModel") == 2) // Racer
        {
            accelerationMultiplier *= 3;
        }

        if (StateManager.GetUpgradeLevel("SelectedModel") == 3) // Truck
        {
            bodyMultiplier *= 3;
        }

        accelerationMultiplier += 1.0f;
        bodyMultiplier += 1.0f;
        efficiencyMultiplier += 1.0f;
    }
}
