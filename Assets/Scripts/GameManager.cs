using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayFabManager playFab;
    
    Canvas Canvas;
    public Camera PlayerCamera;

    double currentMass;
    double maximumMass;

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

        Canvas = GetComponentInChildren<Canvas>();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnGUI()
    {
        TMPro.TMP_Text score = Canvas.transform.Find("MassValue").GetComponent<TMPro.TMP_Text>();
        score.text = currentMass + " U";
    }



    public void NewGame()
    {
        SceneManager.LoadScene("Arena");
        Start();
        PlayerCamera.orthographicSize = 5f;
        currentMass = 1f;
        maximumMass = 1f;
        timeStarted = Time.time;
        inGame = true;
    }

    
    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
        inGame = false;
    }

    public void GameOver()
    {
        inGame = false;
        playFab.SendAllScores(maximumMass, Time.time - timeStarted);
        
    }

    public void AddMass(float mass)
    {
        currentMass += mass;
        if (currentMass > maximumMass) maximumMass = currentMass;
    }
}
