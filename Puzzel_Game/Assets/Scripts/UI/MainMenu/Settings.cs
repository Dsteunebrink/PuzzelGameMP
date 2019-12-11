using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer MainMixer;

    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Toggle fullScreenToggle;

    private float soundEffectsPF;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start () {
        FillResolutionDrowDown ();

        SetSoundFX (PlayerPrefs.GetFloat ("SoundFX"));
        SetMusic (PlayerPrefs.GetFloat ("Music"));
        SetFullscreen (intToBool (PlayerPrefs.GetInt ("FullScreen")));
        SetResolution (PlayerPrefs.GetInt("Resolution"));

        resolutionDropdown.value = PlayerPrefs.GetInt ("Resolution");
        musicSlider.value = PlayerPrefs.GetFloat ("Music");
        soundSlider.value = PlayerPrefs.GetFloat ("SoundFX");
        fullScreenToggle.isOn = intToBool (PlayerPrefs.GetInt ("FullScreen"));

        this.gameObject.SetActive (false);
    }

    public void SetSoundFX (float soundEffects) {
        MainMixer.SetFloat ("SoundFXGroup", soundEffects);
        PlayerPrefs.SetFloat ("SoundFX", soundEffects);
    }

    public void SetMusic (float music) {
        MainMixer.SetFloat ("MusicGroup", music);
        PlayerPrefs.SetFloat ("Music", music);
    }

    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt ("FullScreen", boolToInt(isFullscreen));
    }

    public void SetResolution (int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt ("Resolution", resolutionIndex);
    }

    private void FillResolutionDrowDown () {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions ();
        List<string> options = new List<string> ();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add (option);

            if (resolutions[i].Equals (Screen.currentResolution)) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions (options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue ();
    }

    int boolToInt (bool val) {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool (int val) {
        if (val != 0)
            return true;
        else
            return false;
    }
}
