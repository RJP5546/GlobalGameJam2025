using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) { SetMasterVolumeFloat(PlayerPrefs.GetFloat("MasterVolume")); }
        if (PlayerPrefs.HasKey("SFXVolume")) { SetSFXVolumeFloat(PlayerPrefs.GetFloat("SFXVolume")); }
        if (PlayerPrefs.HasKey("MusicVolume")) { SetMusicVolumeFloat(PlayerPrefs.GetFloat("MusicVolume")); }
    }

    public void ReturnToMenu()
    {
        SceneSwapper.Instance.LoadScene(0);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    public void SetMasterVolume(Slider slider)
    {
        SetMasterVolumeFloat(slider.value);
    }

    public void SetMusicVolume(Slider slider)
    {
        SetMusicVolumeFloat(slider.value);
    }
    public void SetSFXVolume(Slider slider)
    {
        SetSFXVolumeFloat(slider.value);
    }

    private void SetMasterVolumeFloat(float val)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MasterVolume", val);
    }

    private void SetMusicVolumeFloat(float val)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("SFXVolume", val);
    }
    private void SetSFXVolumeFloat(float val)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MusicVolume", val);
    }

    private void UpdateGithupPlease()
    {
        //i hope this works
    }
}
