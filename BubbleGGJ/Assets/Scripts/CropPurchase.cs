using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropPurchase : MonoBehaviour
{
    public Crop Crop;
    [SerializeField]
    private Image CropImage;
    [SerializeField]
    private TextMeshProUGUI CropAmount;
    [SerializeField]
    private TextMeshProUGUI GoldPerUnitPerSec;
    [SerializeField]
    private TextMeshProUGUI TotalGoldPerSec;
    [SerializeField]
    private GameObject PurchaseButton;
    [SerializeField]
    private TextMeshProUGUI ButtonText;
    [SerializeField]
    private TextMeshProUGUI Name;

    public void NewCrop(Crop crop)
    {
        Crop = crop;
        Name.text = Crop.Name;
        CropImage.sprite = Crop.CropImage;
        UpdatePrice();
    }

    public void UpdatePrice()
    {
        CropAmount.text = Crop.GetCropAmount().ToString();
        ButtonText.text = Crop.GetNextCropPrice().ToString();
        TotalGoldPerSec.text = (Crop.GetGoldGenerationPerSec() * Crop.GetUpgradeMultiplier() * Crop.GetCropAmount()).ToString();
        GoldPerUnitPerSec.text = (Crop.GetGoldGenerationPerSec() * Crop.GetUpgradeMultiplier()).ToString();
    }

    private void Update()
    {
        if (Crop == null)
            return;
        if (GameManager.Instance.GetCurrentGold() >= Crop.GetNextCropPrice())
        {
            PurchaseButton.GetComponent<Button>().interactable = true;
        }
        else 
        {
            PurchaseButton.GetComponent<Button>().interactable = false; 
        }
        
    }

    public void Purchase()
    {
        Crop.Purchased();
        UpdatePrice();
    }
}
