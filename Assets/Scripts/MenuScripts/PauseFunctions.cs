using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunctions : MonoBehaviour
{
    //This script deals with pausing and un-pausing allowing you to link up this script to when the object it is attached to is enabled and disabled. 

    float PriorTimeScale;

    //Set this to true to enable pausing, when the item this is attached to is enabled.
    public bool PauseWhenEnable;

    //Set this to true to enable un-pausing, when the item this is attached to is disabled.
    public bool UnpauseWhenDisable;

    void OnEnable()
    {
        //Pause the game if when you enable if the bool is set to true.
        if (PauseWhenEnable == true)
        {          
            Pause();
        }
        
        //Otherwise do nothing.

    }

    void OnDisable()
    {
        //Un-pause the game if when you disable if the bool is set to true.
        if (PauseWhenEnable == true)
        {
            Resume();
        }

        //Otherwise do nothing.

    }

    public void Pause()
    {
        //This script saves the prior timescale, then sets the timescale to 0.

        PriorTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);

    }

    public void Resume()
    {
        //This script checks the prior timescale, then if the prior was 0 it sets it to 1.

        if (PriorTimeScale == 1)
        {
            Time.timeScale = 1;
        }

        //Otherwise do nothing

    }
}
