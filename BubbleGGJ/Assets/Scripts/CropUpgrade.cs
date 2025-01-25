using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropUpgrade : MonoBehaviour
{

    //public Crop Crop

    [SerializeField]
    private Image CropImage;
    [SerializeField]
    private TextMeshProUGUI CurrentPerSecond;
    [SerializeField]
    private TextMeshProUGUI UpgradedPerSecond;
    [SerializeField]
    private GameObject UpgradeButton;

    
    public void NewCrop(/*Crop NewCrop*/)
    {
        //Crop = NewCrop;
        //CurrentPerSecond = Crop.CPS;
        //UpgradedPerSecond = Crop.CPS*1.1;

    }

    private void Update()
    {
        //If( current money is not enough to buy upgrade)
        UpgradeButton.GetComponent<Button>().interactable = false;

        //If(enough money)
        UpgradeButton.GetComponent<Button>().interactable = true;
    }


}
