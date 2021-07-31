using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlatformAndVersionDisplay : MonoBehaviour
{
    //Version and Platform take the cooresponding details to display within themselves.
    public GameObject VersionField;
    public GameObject PlatformField;

    //Set to True if you want enabling the object this is attached to, to push the platform and version to the Gameobjects.
    public bool ShowVersionandPlatformWhenEnable;

    //Debug Tools
    public bool OverrideActivated;
    public string VersionOverride;
    public string PlatformOverride;

    //public GameObject Version { get => version; set => version = value; }

    void OnEnable()
    {
        if (ShowVersionandPlatformWhenEnable == true)
        {
            ShowPlatform();
            ShowVersion();
        }

        //Otherwise do nothing.

    }


    public void ShowPlatform()
    {
        //Debug.Log(Application.platform);

        //Check the runtime platform, output the platform into the platform gameobject
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            PlatformField.GetComponent<TMP_Text>().text = "Windows";
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            PlatformField.GetComponent<TMP_Text>().text = "Web";
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            PlatformField.GetComponent<TMP_Text>().text = "Mac";
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            PlatformField.GetComponent<TMP_Text>().text = "Linux";
        }
        else
        {
            //If all checks fail just push the application platform unformatting and set to the windows version of the main menu.
            PlatformField.GetComponent<TMP_Text>().text = Application.platform.ToString();

        }

        //Override Stuff
        if (OverrideActivated == true)
        {
            PlatformField.GetComponent<TMP_Text>().text = PlatformOverride;
        }
    }

    public void ShowVersion()
    {
        //Debug.Log(Application.version);

        //Set the application version text to the current version.
        VersionField.GetComponent<TMP_Text>().text = "V" + Application.version;

        //Override Stuff
        if (OverrideActivated == true)
        {
            VersionField.GetComponent<TMP_Text>().text = VersionOverride;
        }
    } 

}

