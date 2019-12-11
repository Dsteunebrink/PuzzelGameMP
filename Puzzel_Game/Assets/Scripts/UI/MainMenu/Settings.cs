using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer MainMixer;

    [SerializeField] private Dropdown resolutionDropdown;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start () {
        FillResolutionDrowDown ();
    }

    public void SetSoundFX (float soundEffects) {
        MainMixer.SetFloat ("SoundFXGroup", soundEffects);
    }

    public void SetMusic (float music) {
        MainMixer.SetFloat ("MusicGroup", music);
    }

    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex) {
        Debug.Log (resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
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
}
