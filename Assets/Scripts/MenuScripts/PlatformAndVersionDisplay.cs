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
    public GameObject VersionandPlaformField;

    //Set to True if you want enabling the object this is attached to, to push the platform and version to the Gameobjects.
    public bool ShowVersionWhenEnable;
    public bool ShowPlatformWhenEnable;
    public bool ShowVersionandPlatformOnOneWhenEnable;

    //Debug Tools
    public bool OverrideActivated;
    public string VersionOverride;
    public string PlatformOverride;

    //Data
    string VersionObtained = "";
    string PlatformObtained = "";

    //public GameObject Version { get => version; set => version = value; }

    void OnEnable()
    {
        FindVersion();
        FindPlatform();

        if (ShowVersionWhenEnable == true)
        {
            //Show both version on one object.
            VersionField.GetComponent<TMP_Text>().text = VersionObtained;
        }

        if (ShowPlatformWhenEnable == true)
        {
            //Show both platform on one object.
            PlatformField.GetComponent<TMP_Text>().text = PlatformObtained;
        }

        if (ShowVersionandPlatformOnOneWhenEnable == true)
        {
            //Show both version and platform on one object.
            VersionandPlaformField.GetComponent<TMP_Text>().text = PlatformObtained + " " + VersionObtained;
        }

        //Otherwise do nothing.

    }


    public void FindPlatform()
    {
        //Debug.Log(Application.platform);

        //Check the runtime platform, output the platform into the platform gameobject
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            PlatformObtained = "Windows";
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            PlatformObtained = "Web";
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            PlatformObtained = "Mac";
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            PlatformObtained = "Linux";
        }
        else
        {
            //If all checks fail just push the application platform unformatting and set to the windows version of the main menu.
            PlatformObtained = Application.platform.ToString();

        }

        //Override Stuff
        if (OverrideActivated == true)
        {
            PlatformObtained = PlatformOverride;
        }
    }

    public void FindVersion()
    {
        //Debug.Log(Application.version);

        //Set the application version text to the current version.
        VersionObtained = "V" + Application.version;

        //Override Stuff
        if (OverrideActivated == true)
        {
            VersionObtained = VersionOverride;
        }
    }

}

