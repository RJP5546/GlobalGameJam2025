using UnityEngine;

public class UIToggleActive : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;
    public void ToggleUI()
    {
        uiElement.SetActive(!uiElement.activeSelf);
    }
}
