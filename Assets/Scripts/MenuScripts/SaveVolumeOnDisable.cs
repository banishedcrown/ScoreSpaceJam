using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SaveVolumeOnDisable : MonoBehaviour
{
    private AudioMixer Mixer;

    void OnDisable()
    {
        /*
        Disabled so unity stop throwing a fit with the gamemanager not existing.

        //As it says on the tin, we are saving our volume to the game manager, if the object this is tied to is disabled.
        Mixer = GameManager.GetManager().GetAudioMixer();

        //Backup default values if for some reason we cannot get the floats of the volumes
        float CurrentMaster = 1f;
        float CurrentMusic = 1f;
        float CurrentSFX = 1f;

        //Getting the floats from the volumes so we can push them to save
        Mixer.GetFloat("MasterVol", out CurrentMaster);
        Mixer.GetFloat("MusicVol", out CurrentMusic);
        Mixer.GetFloat("SFXVol", out CurrentSFX);

        //Setting up a variable to make the code for saving a bit easier to read
        SettingsData settings = GameManager.GetManager().GetSettings();

        //Saving our values we got from pulling from the audiomanager
        settings.AudioMaster = CurrentMaster;
        settings.AudioMusic = CurrentMusic;
        settings.AudioSFX = CurrentSFX;

        //I don't remember this.
        SaveSystem.SaveSettings(GameManager.GetManager().GetSettings());
        */

    }
}
