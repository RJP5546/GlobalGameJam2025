using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwapper : Singleton<SceneSwapper>
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject overworldCamera;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private float screenFadeTime;

    [SerializeField] private GameObject playerHUD;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSeneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSeneCoroutine(int sceneIndex)
    {
        //Called by the Main Menu Buttons to load the New Horizon scene and start the game
        yield return screenFader.FadeOut(screenFadeTime);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return screenFader.FadeIn(screenFadeTime);
        Cursor.lockState = CursorLockMode.None;
    }

}
