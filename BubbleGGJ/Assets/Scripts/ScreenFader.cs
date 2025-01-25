using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public float defaultFadeDuration = 2f;  // Duration for the fade effect
    private Image fadeImage;
    private bool isFading;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        StartCoroutine(FadeIn(defaultFadeDuration));
    }

    public IEnumerator FadeIn(float _fadeDuration)
    {
        // Fade in over the fadeDuration
        float timer = 0f;
        while (isFading) { yield return null; }
        while (timer <= _fadeDuration)
        {
            isFading = true;
            float alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);  // Start opaque, end transparent
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0f, 0f, 0f, 0f);  // Ensure fully transparent at the end
        isFading = false;
    }

    public IEnumerator FadeOut(float _fadeDuration)
    {
        // Fade out over the fadeDuration
        float timer = 0f;
        while (isFading) { yield return null; }
        while (timer <= _fadeDuration)
        {
            isFading = true;
            float alpha = Mathf.Lerp(0f, 1f, timer / _fadeDuration);  // Start transparent, end opaque
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0f, 0f, 0f, 1f);  // Ensure fully opaque at the end
        isFading = false;
    }

}
