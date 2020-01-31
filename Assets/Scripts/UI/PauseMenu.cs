using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameStateScriptableObject gameState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        gameState.isPaused = true;
        EventManager.TriggerEvent("GamePaused");
    }

    public void ResumeGame()
    {
        gameState.isPaused = false;
        EventManager.TriggerEvent("GameResumed");
    }
}
