using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{

    //10 different vegetables, 20 plots
    //carrot starts as 1st and already appearing
    //new veggies need to be added to viewport/content for scrolling
    //seprate purchas from upgrade

    [SerializeField] private List<CropPurchase> cropPurchaseList;
    [SerializeField] private List<CropUpgrade> cropUpgradeList;
    [SerializeField] private List<Crop> crops;

    public void Start()
    {
        int count = 0;
        foreach(Crop crop in crops)
        {
            cropPurchaseList[count].NewCrop(crop);
            cropUpgradeList[count].NewCrop(crop);
            count++;
        }
        //HideCrops();
    }
    private void Update()
    {
        //CheckToUnhide();
    }

    private void HideCrops()
    {
        for(int i =0;i<cropPurchaseList.Count;i++)
        {
            cropPurchaseList[i].enabled = false;
            cropUpgradeList[i].enabled = false;
        }
    }

    private void CheckToUnhide()
    {
        for(int i=0;i<crops.Count;i++)
        {
            if (GameManager.Instance.GetCurrentGold() >= (crops[i].GetBaseCropPrice()*0.8))
            {
                cropPurchaseList[i].enabled = true;
                cropUpgradeList[i].enabled = true;
            }
        }
    }

    public void ResetCrops()
    {
        for(int i = 0; i < crops.Count; i++)
        {
            crops[i].ResetCrop();
        }

    }

}
