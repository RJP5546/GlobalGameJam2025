using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer audioMixer;
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
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(slider.value) * 20);
    }

    public void SetMusicVolume(Slider slider)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider.value) * 20);
    }
    public void SetSFXVolume(Slider slider)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(slider.value) * 20);
    }
}
