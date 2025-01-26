using NUnit.Framework;
using System.Collections.Generic;
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
    [SerializeField] private float startGoldGen;
    [SerializeField] private float foodGenerationPerSec;
    [SerializeField] private float startFoodGen;
    [SerializeField] private float upgradeMultiplier = 1f;
    private float nextUpgradeMultiplierPrice;
    [SerializeField]
    private int cropAmount;
    [SerializeField]
    private float baseCropPrice;
    [SerializeField]
    private float nextCropPrice;
    [SerializeField]
    private List<GameObject> cropList;
    private int cropCount = 0;

    private void Awake()
    {
        startGoldGen = goldGenerationPerSec;
        startFoodGen = foodGenerationPerSec;
        NextNewCropPrice();
        NewUpgradeMultiplierPrice();
    }

    public float GetUpgradeMultiplier(){ return upgradeMultiplier; }
    public float GetNextUpgradeMultiplier() { return upgradeMultiplier + 1; }
    public float GetNextUpgradeMultiplierPrice() { return nextUpgradeMultiplierPrice; }
    public float GetNextCropPrice() { return nextCropPrice; }
    public int GetCropAmount() { return cropAmount; }
    public float GetGoldGenerationPerSec() { return goldGenerationPerSec; }
    public float GetBaseCropPrice() { return baseCropPrice; }

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
        SFXManager.Instance.PlayscienceSound();
        GameManager.Instance.SubtractScience(nextUpgradeMultiplierPrice);

        GameManager.Instance.SubtractGoldPerSec(goldGenerationPerSec * cropAmount * upgradeMultiplier);
        GameManager.Instance.SubtractFoodPerSec(foodGenerationPerSec * cropAmount * upgradeMultiplier);

        IncreaseMultiplier();
        
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec * cropAmount * upgradeMultiplier);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec * cropAmount * upgradeMultiplier);
    }

    public void Purchased()
    {
        SFXManager.Instance.PlaycoinsSound();
        GameManager.Instance.SubtractGold(nextCropPrice);
        IncreaseCropAmount();
        GameManager.Instance.AddGoldPerSec(goldGenerationPerSec * upgradeMultiplier);
        GameManager.Instance.AddFoodPerSec(foodGenerationPerSec * upgradeMultiplier);
        ShowNextVeg();
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

    public void ShowNextVeg()
    {
        if (cropCount >= cropList.Count)
        {
            return;
        }
        cropList[cropCount].SetActive(true);
        cropCount++;
    }

    public void ResetCrop()
    {
        cropCount = 0;
        cropAmount = 0;
        upgradeMultiplier = 1f;
        ShopManager.Instance.Start();

        foreach (GameObject crop in cropList)
        {
            crop.SetActive(false);
        }

        goldGenerationPerSec = startGoldGen;
        foodGenerationPerSec = startFoodGen;
        NextNewCropPrice();
        NewUpgradeMultiplierPrice();

    }

}
