using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    //Cost for first one is static, next ones determined by some function
    //Upgrades cost science from rebirth
    //Crop contains its own data, UI asks for it

    public string Name;
    public Sprite CropImage;

    [SerializeField] private float goldGenerationPerSec;
    [SerializeField] private float foodGenerationPerSec;
    [SerializeField] private float upgradeMultiplier = 1f;
    private float nextUpgradeMultiplierPrice;
    [SerializeField]
    private int cropAmount;
    [SerializeField]
    private float baseCropPrice;
    private float nextCropPrice;

    private void Start()
    {
        NextNewCropPrice();
        NewUpgradeMultiplierPrice();
    }

    public float GetUpgradeMultiplier(){ return upgradeMultiplier; }
    public float GetNextUpgradeMultiplier() { return upgradeMultiplier + 1; }
    public float GetNextUpgradeMultiplierPrice() { return nextUpgradeMultiplierPrice; }
    public float GetNextCropPrice() { return nextCropPrice; }
    public int GetCropAmount() { return cropAmount; }
    public float GetGoldGenerationPerSec() { return goldGenerationPerSec; }

    private void OnEnable()
    {
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec *cropAmount*upgradeMultiplier);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec *cropAmount);
    }
    private void OnDisable()
    {
        GameManager.Instance.AddGoldPerSec(-goldGenerationPerSec *cropAmount*upgradeMultiplier);
        GameManager.Instance.AddFoodPerSec(-foodGenerationPerSec *cropAmount);
    }

    //Price function = e^(0.5x), x is the CropAmount
    //Upgrade function = 10*2^x, x is the current upgrade multiplier

    public void Upgraded()
    {
        GameManager.Instance.SubtractScience(nextUpgradeMultiplierPrice);

        GameManager.Instance.SubtractGoldPerSec(goldGenerationPerSec * cropAmount * upgradeMultiplier);
        GameManager.Instance.SubtractFoodPerSec(foodGenerationPerSec * cropAmount * upgradeMultiplier);

        IncreaseMultiplier();
        
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec * cropAmount * upgradeMultiplier);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec * cropAmount * upgradeMultiplier);
    }

    public void Purchased()
    {
        GameManager.Instance.SubtractGold(nextCropPrice);
        IncreaseCropAmount();
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec * upgradeMultiplier);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec * upgradeMultiplier);
    }

    private void IncreaseCropAmount() 
    {
        cropAmount++;
        NextNewCropPrice();
    }

    private void NextNewCropPrice()
    {
        nextCropPrice = Mathf.Round(baseCropPrice * Mathf.Pow((float)System.Math.E, 0.5f * cropAmount)*100)/100;
    }

    private void IncreaseMultiplier() 
    {
        upgradeMultiplier = upgradeMultiplier + 1;
        NewUpgradeMultiplierPrice();
    }

    private void NewUpgradeMultiplierPrice() 
    {
        nextUpgradeMultiplierPrice = 10*Mathf.Pow(2f, upgradeMultiplier-1);
    }
}
