using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayFabManager playFab;

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
    }

    
    public static void NewGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public static void GameOver()
    {

    }
}
