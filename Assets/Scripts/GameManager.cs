using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayFabManager playFab;
    Canvas Canvas;
    Camera PlayerCamera;

    double currentMass;
    double maximumMass;

    float timeStarted;

    bool inGame = false;

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
    }

    

    public void NewGame()
    {
        SceneManager.LoadScene("Arena");
        Start();
        PlayerCamera.orthographicSize = 5f;
        currentMass = 1f;
        maximumMass = 1f;
        timeStarted = Time.time;
    }

    
    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }

    public void GameOver()
    {

    }
}
