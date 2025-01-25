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
    private float upgradeMultiplier = 1f;
    private float nextUpgradeMultiplierPrice;
    [SerializeField]
    private int cropAmount;
    [SerializeField]
    private float baseCropPrice;
    private float cropPrice;
    private float nextCropPrice;

    private void Start()
    {
        NextNewCropPrice();
        NewUpgradeMultiplierPrice();
    }

    public float GetUpgradeMultiplier(){ return upgradeMultiplier; }
    public float GetNextUpgradeMultiplier() { return upgradeMultiplier * 2; }
    public float GetNextUpgradeMultiplierPrice() { return nextUpgradeMultiplierPrice; }
    public float GetCropPrice() { return cropPrice; }
    public float GetNextCropPrice() { return nextCropPrice; }
    public int GetCropAmount() { return cropAmount; }
    public float GetGoldGenerationPerSec() { return goldGenerationPerSec; }

    private void OnEnable()
    {
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec);
    }
    private void OnDisable()
    {
        GameManager.Instance.AddGoldPerSec(-goldGenerationPerSec);
        GameManager.Instance.AddFoodPerSec(-foodGenerationPerSec);
    }

    //Price function = e^(0.05x), x is the CropAmount
    //Upgrade function = 10*2^x, x is the current upgrade multiplier

    public void Upgraded()
    {
        IncreaseMultiplier();
    }

    public void Purchased()
    {
        IncreaseCropAmount();
    }

    private void IncreaseCropAmount() 
    {
        cropAmount++;
        NextNewCropPrice();
    }

    private void NextNewCropPrice()
    {
        nextCropPrice = baseCropPrice * Mathf.Pow((float)System.Math.E, 0.05f * cropAmount);
    }

    private void IncreaseMultiplier() 
    {
        upgradeMultiplier = upgradeMultiplier * 2;
        NewUpgradeMultiplierPrice();
    }

    private void NewUpgradeMultiplierPrice() 
    {
        nextUpgradeMultiplierPrice = 10*Mathf.Pow(2f, upgradeMultiplier-1);
    }
}
