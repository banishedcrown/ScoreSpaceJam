using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject inGameUI;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnGUI()
    {
        if (gm.inGame)
        {
            TMPro.TMP_Text score = GameObject.Find("MassValue").GetComponent<TMPro.TMP_Text>();
            score.text = gm.currentMass.ToString("F3") + " U";

            Slider healthSlider = GameObject.Find("TimeBar").GetComponent<Slider>();
            healthSlider.maxValue = (float) gm.maxHealth;
            healthSlider.value = (float) gm.currentHealth;
        }
    }

    public void SetGameOverMenuActive(bool value)
    {
        gameOverMenu.SetActive(value);
        inGameUI.SetActive(!value);
    }

    public void RestartGame()
    {
        gm.NewGame();
    }
}
