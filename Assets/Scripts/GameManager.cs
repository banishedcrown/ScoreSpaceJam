using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayFabManager playFab { get; private set; } 
    
    Canvas Canvas;
    public Camera PlayerCamera;

    public AudioSource alarmSource;
    public float healthAlarmTriggerPercentage = 0.3f;

    public double currentMass { get; private set; }
    double maximumMass;


    public double currentHealth { get; private set; } = 0f;
    public double maxHealth { get; private set; } = 100f;
    public double percentDamagePerSecond = 0.05f;


    public float timeStarted { get; private set; }
    public float currentTimeSurvided { get; private set; }

    public GameObject player;
    public PlayerController playerController;
    public bool inGame = false;
    public bool isDead = false;

    public bool zoomIsToggle { get; private set; }


    void OnEnable()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1) Destroy(this.gameObject);
        
        DontDestroyOnLoad(this.gameObject);
        playFab = GetComponent<PlayFabManager>();
        playFab.Login();

        alarmSource = GetComponent<AudioSource>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

        Canvas = GameObject.Find("UI").GetComponent<Canvas>();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) playerController = player.GetComponent<PlayerController>();

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
            else if( currentHealth < maxHealth * healthAlarmTriggerPercentage)
            {
                if (!alarmSource.isPlaying)
                {
                    alarmSource.loop = true;
                    alarmSource.Play();
                }

            }
            else
            {
                alarmSource.Stop();
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

    public void Restart()
    {
        NewGame();
    }
    public void NewGame()
    { 
        SceneManager.LoadScene("Arena");
        PauseGame(false);
        Start();
        PlayerCamera.orthographicSize = 5f;
        currentMass = 0f;
        maximumMass = 0f;

        currentHealth = 0f;
        maxHealth = 0f;

        timeStarted = Time.time;
        inGame = true;
        isDead = false;
    }

    public void MainMenu()
    {
        inGame = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void AllowZoomToggle(bool on)
    {
        this.zoomIsToggle = on;
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
        if (alarmSource.isPlaying) alarmSource.Stop();

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
