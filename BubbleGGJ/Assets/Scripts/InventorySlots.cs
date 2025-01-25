/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : 24-25 Production
* FILE NAME       : Scrap.cs
* DESCRIPTION     : Script for adding Scriptable Objects to the Inventory
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/22		Ben Jenkins    		 Created
* 2024/10/3         Ben Jenkins          Updated for player inventory slots
*
/******************************************************************/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Scrap Scrap;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Amount;
    public Image Sprite;
    public GameObject Tooltip;

    // Start is called before the first frame update
    //Connects the scrap data fields to the text fields in the UI
    void Start()
    {
        if (Scrap == null)
        {
            Sprite.enabled = false;
            Amount.enabled = false;
        }
        else
        {
            Name.text = Scrap.Name;
            Description.text = Scrap.Description;
            Cost.text = "$" + Scrap.Cost.ToString();
            Amount.text = Scrap.PlayerAmount.ToString();
            Sprite.sprite = Scrap.Artwork;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Amount.isActiveAndEnabled)
        {
            Amount.text = Scrap.ShopAmount.ToString();
        }
    }
    public void UpdateScrap(Scrap scrap)
    {
        Scrap = scrap;
        Sprite.enabled = true;
        Amount.enabled = true;
        Name.text = Scrap.Name;
        Description.text = Scrap.Description;
        Cost.text = "$" + Scrap.Cost.ToString();
        Amount.text = Scrap.ShopAmount.ToString();
        Sprite.sprite = Scrap.Artwork;
    }

}
