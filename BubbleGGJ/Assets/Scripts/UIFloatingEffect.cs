using UnityEngine;

public class UIFloatingEffect : MonoBehaviour
{
    public float floatSpeed = 1f;  // How fast the image floats
    public float floatAmount = 10f;  // How far the image moves up and down

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    void Start()
    {
        // Get the RectTransform of the UI element
        rectTransform = GetComponent<RectTransform>();
        // Store the original position of the image
        originalPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave for smooth up and down motion
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        // Set the new anchored position of the RectTransform
        rectTransform.anchoredPosition = new Vector2(originalPosition.x, newY);
    }
}
