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
    }

    private void Update()
    {
        //If( current money is not enough to buy upgrade)
        PurchaseButton.GetComponent<Button>().interactable = false;

        //If(enough money)
        PurchaseButton.GetComponent<Button>().interactable = true;
    }

    public void Purchase()
    {
        Crop.Purchased();
    }
}
