using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Image storeTab;
    [SerializeField] private Image specialTab;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private GameObject specialPanel;

    public void DisplayShop()
    {
        storeTab.color = activeColor;
        specialTab.color = inactiveColor;
        specialPanel.SetActive(false);
        storePanel.SetActive(true);
    }

    public void DisplaySpecial() 
    {
        storeTab.color = inactiveColor;
        specialTab.color = activeColor;
        storePanel.SetActive(false);
        specialPanel.SetActive(true);
    }
}
