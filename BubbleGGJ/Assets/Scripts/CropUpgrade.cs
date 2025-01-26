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


    public void NewCrop(Crop crop)
    {
        Crop = crop;
        Name.text = Crop.Name;
        CropImage.sprite = Crop.CropImage;
        UpdatePrice();
    }

    public void UpdatePrice()
    {
        CurrentMultiplier.text = Crop.GetUpgradeMultiplier().ToString();
        UpgradedMultiplier.text = GameManager.Instance.NF.FormatNumber(Crop.GetNextUpgradeMultiplier());
        ButtonText.text = GameManager.Instance.NF.FormatNumber(Crop.GetNextUpgradeMultiplierPrice());
    }

    private void Update()
    {
        if (Crop == null)
            return;
        if(GameManager.Instance.GetCurrentScience()>=Crop.GetNextUpgradeMultiplierPrice())
        {
            UpgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            UpgradeButton.GetComponent<Button>().interactable = false;
        }
        

    }

    public void Upgraded()
    {
        Crop.Upgraded();
        UpdatePrice();
    }

}
