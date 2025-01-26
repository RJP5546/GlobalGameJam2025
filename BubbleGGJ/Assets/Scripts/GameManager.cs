using SimpleAudioManager;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float goldPerSecond;
    [SerializeField] private float foodPerSecond;
    [SerializeField] private float targetFoodPerSecond;
    [SerializeField] private float endFoodPerSecond;
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
    [SerializeField] private Button timeTravelButton;

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
        CheckForEnd();
        CalculateScience();
        UpdateUI();
    }

    private void UpdateUI()
    {
        goldUI.text = $"Gold: {currentGold}";
        goldGenerationUI.text = $"Gold/s: {goldPerSecond}";
        currentScienceUI.text = currentScience.ToString();
        foodUI.value = foodPerSecond / targetFoodPerSecond;

        if (foodPerSecUI != null && foodPerSecUI.gameObject.activeSelf)
        {
            foodPerSecUI.text = $"{foodPerSecond}/s";
            totalFoodUI.text = currentFood.ToString();
            pendingScienceUI.text = pendingScience.ToString();
            timeTravelButton.interactable = pendingScience > 0;
            Debug.Log("In");
        }
    }

    private void CheckForEnd()
    {
        if (foodPerSecond >= endFoodPerSecond)
        {
            CancelInvoke("AddResources");
            SceneSwapper.Instance.LoadScene(2);
            //Destroy(Manager.instance);
        }
    }

    private void CalculateScience()
    {
        if(foodPerSecond >= targetFoodPerSecond)
        {
            pendingScience = ((Mathf.Log(foodPerSecond / targetFoodPerSecond) / Mathf.Log(2)) + 1)*2;
            pendingScience = Mathf.Round(pendingScience*100)/100;
        }
    }

    public void EarnScience()
    {
        AddScience(pendingScience);
        pendingScience = 0;
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

    public void TimeTravel()
    {
        ShopManager.Instance.ResetCrops();
        ShopManager.Instance.Start();
        CancelInvoke("AddResources");
        currentFood = 0;
        currentGold = 0;
        goldPerSecond = 0;
        foodPerSecond = 0;
        EarnScience();
        pendingScience = 0;
        targetFoodPerSecond = targetFoodPerSecond * 3;
        CutsceneManager.Instance.PlayCutscene();
        Start();
    }
}
