using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public RenderConfigScriptableObject renderConfig;
    public PlayerConfigScriptableObject playerConfig;
    public LevelDifficultyScriptableObject difficulty;
    public GameStateScriptableObject gameState;

    UnityAction pauseListener;
    UnityAction resumeListener;
    Vector3 savedVelocity;
    int posIdx = 1;
    Rigidbody rb;


    public void NewGame()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        posIdx = 1;
        rb.mass = GetMass();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = GetMass();

        NewGame();
        pauseListener = new UnityAction(PauseEvent);
        EventManager.StartListening("GamePaused", pauseListener);

        resumeListener = new UnityAction(ResumeEvent);
        EventManager.StartListening("GameResumed", resumeListener);
    }

    void Update()
    {
        if (gameState.isPaused)
        {
            return;
        }

        if (gameState.timeLeft <= 0)
        {
            EventManager.TriggerEvent("GameOver");
            gameState.isPaused = true;
            rb.AddForce(new Vector3(-10f * playerConfig.maxAcceleration, 0, 0));
            return;
        }

        if (Input.GetKeyDown("up"))
        {
            Jump();
        }

        if (Input.GetKeyDown("left"))
        {
            Left();
        }

        if (Input.GetKeyDown("right"))
        {
            Right();
        }
    }

    void FixedUpdate()
    {
        if (gameState.isPaused)
        {
            return;
        }

        if (gameState.timeLeft <= 0)
        {
            EventManager.TriggerEvent("GameOver");
            gameState.isPaused = true;
            return;
        }

        int level = difficulty.GetLevelFromDistance(rb.transform.position.x);

        if (rb.velocity.x < difficulty.GetMaxSpeed(level))
        {
            float acceleration = playerConfig.maxAcceleration * GetMass() * gameState.accelerationMultiplier;
            rb.AddForce(new Vector3(acceleration, 0, 0));
        }

        float step = playerConfig.lateralSpeed * Time.deltaTime;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, renderConfig.lanePosns[posIdx]);
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        gameState.timeLeft -= Time.deltaTime / gameState.efficiencyMultiplier;
    }

    public void Left()
    {
        if (posIdx > 0) posIdx -= 1;
    }

    public void Right()
    {
        if (posIdx < renderConfig.lanePosns.Length - 1) posIdx += 1;
    }

    public void Jump()
    {
        if (rb.transform.position.y < playerConfig.vehicleHeight) // single jump
        {
            rb.velocity = new Vector3(rb.velocity.x, playerConfig.jumpSpeed, rb.velocity.z);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            gameState.coinCount += 1;
            other.gameObject.SetActive(false);
            return;
        }

        if (other.tag == "Gas")
        {
            gameState.timeLeft = gameState.timeLeft + difficulty.gasTimeAdded > gameState.maxTime ? gameState.maxTime : gameState.timeLeft + difficulty.gasTimeAdded;
            other.gameObject.SetActive(false);
            return;
        }
    }

    void PauseEvent()
    {
        savedVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    void ResumeEvent()
    {
        rb.velocity = savedVelocity;
    }

    float GetMass()
    {
        return playerConfig.mass * gameState.bodyMultiplier;
    }
}
