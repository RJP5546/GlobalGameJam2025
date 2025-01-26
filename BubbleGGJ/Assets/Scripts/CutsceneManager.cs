using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [SerializeField] private CinemachineCamera defaultCam;
    [SerializeField] private CinemachineCamera cutsceneCam;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private AudioSource cutsceneAudioSource;

    private void Start()
    {
        if(videoPlayer.isPlaying) { videoPlayer.Stop(); }
        videoPlayer.time = 0;
    }

    public void PlayCutscene()
    {
        StartCoroutine(Cutscene());
    }

    private IEnumerator Cutscene()
    {
        yield return screenFader.FadeOut(1f);
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(1f);
        hudCanvas.enabled = false;
        cutsceneCam.gameObject.SetActive(true);
        defaultCam.gameObject.SetActive(false);

        
        videoPlayer.time = 0;
        videoPlayer.Play();
        yield return screenFader.FadeIn(0.5f);

        yield return new WaitForSeconds(3f);
        yield return screenFader.FadeOut(0.5f);
        hudCanvas.enabled = true;
        defaultCam.gameObject.SetActive(true);
        cutsceneCam.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        yield return screenFader.FadeIn(2f);
    }

}
