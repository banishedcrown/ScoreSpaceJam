using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayFabManager playFab { get; private set; } 
    
    Canvas Canvas;
    public Camera PlayerCamera;

    public double currentMass { get; private set; }
    double maximumMass;


    public double currentHealth { get; private set; } = 0f;
    public double maxHealth { get; private set; } = 100f;
    public double percentDamagePerSecond = 0.1f;

    public float timeStarted { get; private set; }
    public float currentTimeSurvided { get; private set; }

    public GameObject player;
    public bool inGame = false;
    public bool isDead = false;

    void OnEnable()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1) Destroy(this.gameObject);
        
        DontDestroyOnLoad(this.gameObject);
        playFab = GetComponent<PlayFabManager>();
        playFab.Login();
        
    }
    // Start is called before the first frame update
    void Start()
    {

        Canvas = GameObject.Find("UI").GetComponent<Canvas>();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("UpdateLeaderboard", 1f);
    }

    private void Update()
    {
        if (inGame && maxHealth > 2)
        {
            currentHealth -= maxHealth * percentDamagePerSecond * Time.deltaTime;

            if (currentHealth <= 0)
            {
                isDead = true;
                GameOver();
            }

            
        }
        if (inGame)
        {
            currentTimeSurvided = Time.time - timeStarted;
        }
    }


    public static GameManager GetManager()
    {
        return GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void UpdateLeaderboard()
    {
        playFab.UpdateAllStatistics();
    }


    public void NewGame()
    {
        SceneManager.LoadScene("Arena");
        PauseGame(false);
        Start();
        PlayerCamera.orthographicSize = 5f;
        currentMass = 1f;
        maximumMass = 1f;

        currentHealth = 1f;
        maxHealth = 1f;

        timeStarted = Time.time;
        inGame = true;
    }

    public void MainMenu()
    {
        inGame = false;
        SceneManager.LoadScene("MainMenu");
    }

    
    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
        inGame = !pause;
    }

    public void GameOver()
    {
        inGame = false;
        isDead = true;
        playFab.SendAllScores(maximumMass, Time.time - timeStarted);

        GameObject.Find("UI").SendMessage("SetGameOverMenuActive", true);
    }

    public void AddMass(float mass)
    {
        currentMass += mass;
        currentHealth += mass;
        maxHealth += mass;
        if (currentMass > maximumMass) maximumMass = currentMass;
    }
}
