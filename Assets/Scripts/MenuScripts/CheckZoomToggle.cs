using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckZoomToggle : MonoBehaviour
{

    void OnEnable()
    {
        Toggle toggle = gameObject.GetComponent<Toggle>();
        
        toggle.isOn = GameManager.GetManager().zoomIsToggle;

    }
}
