using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float goldPerSecond;
    [SerializeField] private float foodPerSecond;
    [SerializeField] private float targetFoodPerSecond;
    [SerializeField] private float currentGold;
    [SerializeField] private float currentFood;

    [SerializeField] private TMP_Text goldUI;
    [SerializeField] private TMP_Text goldGenerationUI;
    [SerializeField] private Slider foodUI;

    void Start()
    {
        InvokeRepeating("AddResources", 1f, 1f);  //1s delay, repeat every 1s
    }

    void AddResources()
    {
        currentGold += goldPerSecond;
        currentFood += foodPerSecond;
        UpdateUI();
    }

    private void UpdateUI()
    {
        goldUI.text = $"Gold: {currentGold}";
        goldGenerationUI.text = $"Gold/s: {goldPerSecond}";
        foodUI.value = foodPerSecond / targetFoodPerSecond;
    }

    public void AddGoldPerSec(float gps)
    {
        goldPerSecond += gps;
        UpdateUI();
    }

    public void AddFoodPerSec(float fps)
    {
        foodPerSecond += fps;
        UpdateUI();
    }
}
