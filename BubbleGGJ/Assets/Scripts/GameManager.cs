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
    [SerializeField] private float currentScience;
    [SerializeField] private float pendingScience;

    [SerializeField] private TMP_Text goldUI;
    [SerializeField] private TMP_Text goldGenerationUI;
    [SerializeField] private TMP_Text currentScienceUI;
    [SerializeField] private TMP_Text pendingScienceUI;
    [SerializeField] private TMP_Text foodPerSecUI;
    [SerializeField] private TMP_Text totalFoodUI;
    [SerializeField] private Slider foodUI;

    void Start()
    {
        InvokeRepeating("AddResources", 1f, 1f);  //1s delay, repeat every 1s
    }

    public float GetCurrentGold() { return currentGold; }
    public float GetCurrentScience() { return currentScience;}

    void AddResources()
    {
        currentGold += goldPerSecond;
        currentFood += foodPerSecond;
        CalculateScience();
        UpdateUI();
    }

    private void UpdateUI()
    {
        goldUI.text = $"Gold: {currentGold}";
        goldGenerationUI.text = $"Gold/s: {goldPerSecond}";
        currentScienceUI.text = currentScience.ToString();
        foodUI.value = foodPerSecond / targetFoodPerSecond;

        if (foodPerSecUI.gameObject.activeSelf)
        {
            foodPerSecUI.text = $"Food/s: {foodPerSecond}";
            totalFoodUI.text = currentFood.ToString();
            pendingScienceUI.text = pendingScience.ToString();
        }
    }

    private void CalculateScience()
    {
        if(foodPerSecond >= 1000000000)
        {
            pendingScience = (Mathf.Log(foodPerSecond / 1000000000) / Mathf.Log(2)) + 1;
        }
    }

    public void EarnScience()
    {
        AddScience(pendingScience);
    }

    public void AddGoldPerSec(float gps)
    {
        goldPerSecond += gps;
        UpdateUI();
    }

    public void AddScience(float science)
    {
        currentScience += science;
        UpdateUI();
    }

    public void AddFoodPerSec(float fps)
    {
        foodPerSecond += fps;
        UpdateUI();
    }

    public void SubtractGoldPerSec(float gold)
    {
        goldPerSecond -= gold;
        UpdateUI();
    }

    public void SubtractGold(float gold)
    {
        currentGold -= gold;
        UpdateUI();
    }

    public void SubtractScience(float science)
    { 
        currentScience -= science;
        UpdateUI();
    }

    public void SubtractFoodPerSec(float food)
    {
        foodPerSecond -= food;
        UpdateUI();
    }
}
