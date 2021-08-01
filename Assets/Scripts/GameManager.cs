using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayFabManager playFab;
    
    Canvas Canvas;
    public Camera PlayerCamera;

    public double currentMass { get; private set; }
    double maximumMass;

    
    double currentHealth = 0f;
    double maxHealth = 100f;
    double percentDamagePerSecond = 0.1f;

    float timeStarted;

    public GameObject player;
    public bool inGame = false;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1) Destroy(this.gameObject);
        
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        playFab = GetComponent<PlayFabManager>();

        Canvas = GameObject.Find("UI").GetComponent<Canvas>();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        currentHealth -= maxHealth * percentDamagePerSecond * Time.deltaTime;

        if (currentHealth <= 0) GameOver();
    }

    private void OnGUI()
    {
        TMPro.TMP_Text score = GameObject.Find("MassValue").GetComponent<TMPro.TMP_Text>();
        score.text = currentMass.ToString("F3") + " U";
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

    
    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
        inGame = pause;
    }

    public void GameOver()
    {
        inGame = false;
        PauseGame(true);
        playFab.SendAllScores(maximumMass, Time.time - timeStarted);
        
    }

    public void AddMass(float mass)
    {
        currentMass += mass;
        currentHealth += mass;
        maxHealth += mass;
        if (currentMass > maximumMass) maximumMass = currentMass;
    }
}
