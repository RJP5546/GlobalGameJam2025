using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [SerializeField] private CinemachineCamera defaultCam;
    [SerializeField] private CinemachineCamera cutsceneCam;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Canvas hudCanvas;

    public bool play;

    private void Start()
    {
        if(videoPlayer.isPlaying) { videoPlayer.Stop(); }
        videoPlayer.time = 0;
    }

    public void PlayCutscene()
    {
        cutsceneCam.gameObject.SetActive(true);
        defaultCam.gameObject.SetActive(false);
        videoPlayer.time = 0;
        videoPlayer.Play();
    }

    private IEnumerator Cutscene()
    {
        yield return null;
    }

}
