using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDelayScript : MonoBehaviour
{
    //Realllly simple script here, attach this to the parent of the buttons that are running the button script to allow you to add additional delay onto all the buttons at once.
    //The Public float delay is refrenced by the button script on start 

    public float delay = 0f;
}
