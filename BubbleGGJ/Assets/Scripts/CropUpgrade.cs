using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropUpgrade : MonoBehaviour
{

    public Crop Crop;
    [SerializeField]
    private Image CropImage;
    [SerializeField]
    private TextMeshProUGUI CurrentMultiplier;
    [SerializeField]
    private TextMeshProUGUI UpgradedMultiplier;
    [SerializeField]
    private GameObject UpgradeButton;
    [SerializeField]
    private TextMeshProUGUI ButtonText;
    [SerializeField]
    private TextMeshProUGUI Name;


    public void NewCrop(Crop NewCrop)
    {
        Crop = NewCrop;
        Name.text = Crop.Name;
        CropImage.sprite = Crop.CropImage;
        UpdatePrice();
    }

    public void UpdatePrice()
    {
        CurrentMultiplier.text = Crop.GetUpgradeMultiplier().ToString();
        UpgradedMultiplier.text = Crop.GetNextUpgradeMultiplier().ToString();
        ButtonText.text = Crop.GetNextUpgradeMultiplierPrice().ToString();
    }

    private void Update()
    {
        //If( current money is not enough to buy upgrade)
        UpgradeButton.GetComponent<Button>().interactable = false;

        //If(enough money)
        UpgradeButton.GetComponent<Button>().interactable = true;
    }

    public void Upgraded()
    {
        Crop.Upgraded();
        UpdatePrice();
    }

}
