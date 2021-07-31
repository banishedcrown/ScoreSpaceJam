using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    //Link up your sliders to these.
    public Slider Master;
    public Slider Music;
    public Slider Effects;

    //Set to public if you want to try and use this without gamemanager
    public AudioMixer Mixer;

    void OnEnable()
    {
        //Making the gamemanager set the audiomixers to their current value when the menu that does audio is opened.

        //Uncomment out this and change the audiomixer above to private to use a gamemanager
        //Mixer = GameManager.GetManager().GetAudioMixer();

        //Backup if nothing is there
        float CurrentMaster = 1f;
        float CurrentMusic = 1f;
        float CurrentSFX = 1f;

        //Pulling the value for each mixer to set the sliders to those values from the start.
        Mixer.GetFloat("MasterVol", out CurrentMaster);
        Master.value = Mathf.Pow(10, (CurrentMaster / 20f));

        Mixer.GetFloat("MusicVol", out CurrentMusic);
        Music.value = Mathf.Pow(10, (CurrentMusic / 20f));

        Mixer.GetFloat("SFXVol", out CurrentSFX);
        Effects.value = Mathf.Pow(10, (CurrentSFX / 20f));

    }

    public void SetMaster()
    {
        //Attach this to your slider that messes with the master volume to on value changed.
        //Pull the float from the slider, then set the cooresponding volume to the mixer, and finally push it to player prefabs.
        float sliderValue = Master.value;
        Mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        //PlayerPrefs.SetFloat("MasterVol", sliderValue);
    }

    public void SetMusic()
    {
        //Attach this to your slider that messes with the music volume to on value changed.
        //Pull the float from the slider, then set the cooresponding volume to the mixer, and finally push it to player prefabs.
        float sliderValue = Music.value;
        Mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        //PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    public void SetSFX()
    {
        //Attach this to your slider that messes with the Sound Effects volume to on value changed.
        //Pull the float from the slider, then set the cooresponding volume to the mixer, and finally push it to player prefabs.
        float sliderValue = Effects.value;
        Mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        //PlayerPrefs.SetFloat("SFXVol", sliderValue);
    }

}
