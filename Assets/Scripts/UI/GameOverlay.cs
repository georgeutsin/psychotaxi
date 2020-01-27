 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : MonoBehaviour
{
    public GameStateScriptableObject gs;
    public Slider gasSlider;
    public Text score;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gasSlider.value = gs.timeLeft / gs.maxTime;
        score.text = string.Format("Score: {0}", player.transform.position.x);
    }
}
