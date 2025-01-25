/*******************************************************************
* COPYRIGHT       : 2023
* PROJECT         : 24-25 Production
* FILE NAME       : PlayerInventorySlot.cs
* DESCRIPTION     : Player inventory slot
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/3		Ben Jenkins    		 Created
* 2024/10/7     Ben Jenkins          Fixed the display issue
* 
*
/******************************************************************/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlot : MonoBehaviour
{
    public Scrap Scrap;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI PlayerAmount;
    public Image Sprite;
    public GameObject Tooltip;
    private InventoryManager InventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        if(Scrap==null)
        {
            Sprite.enabled = false;
            PlayerAmount.enabled = false;
        }
        else
        {
            Name.text = Scrap.Name;
            Description.text = Scrap.Description;
            Cost.text = "$" + Scrap.Cost.ToString();
            PlayerAmount.text = Scrap.PlayerAmount.ToString();
            Sprite.sprite = Scrap.Artwork;
            
        }
            InventoryManager = GameObject.Find("DisplayBoard").GetComponent<InventoryManager>();
    }

    public void Update()
    {
        if(PlayerAmount.isActiveAndEnabled == true)
        {
            PlayerAmount.text=Scrap.PlayerAmount.ToString();
        }
    }

    public void UpdateScrap(Scrap scrap)
    {
        Scrap = scrap;
        Sprite.enabled = true;
        PlayerAmount.enabled=true;
        Name.text = Scrap.Name;
        Description.text = Scrap.Description;
        Cost.text = "$" + Scrap.Cost.ToString();
        PlayerAmount.text = Scrap.PlayerAmount.ToString();
        Sprite.sprite = Scrap.Artwork;
    }

    public void DisplayScrap()
    {
        InventoryManager.DisplayItem(Scrap);
    }
}
