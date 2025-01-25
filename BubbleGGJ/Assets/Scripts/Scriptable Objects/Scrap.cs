/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : 24-25 Production
* FILE NAME       : Scrap.cs
* DESCRIPTION     : Scriptable Object template for scrap
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/22		Ben Jenkins    		 Created
* 2024/10/7         Ben Jenkins          Added selling methods
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scrap", menuName = "New Scrap")]
public class Scrap : ScriptableObject
{
    //Fields for basic scrap
    public string Name;
    public string Description;

    public Sprite Artwork;

    public int Cost;
    public int PlayerAmount;
    public int ShopAmount;

    public void SoldToShop()
    {
        if (PlayerAmount > 0)
        {
            ShopAmount++;
            PlayerAmount--;
        }
        else
            Debug.LogError("Player does not have the item to sell");

    }

    public void BoughtByPlayer()
    {
        if(ShopAmount>0)
        {
            ShopAmount--;
            PlayerAmount++;
        }
        else
            Debug.LogError("The shop does not have the item to sell");
    }

}
