/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : 24-25 Production
* FILE NAME       : InventoryManager.cs
* DESCRIPTION     : Creates a list of all scriptable objects in this inventory
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/02 | Ben Jenkins | Created the script.
* 2024/10/10 | Ariana Kim | Updated into a singleton.
* 2024/10/28 | William Pittenger | Added ScrapValueInPlayerInventory()
* 2024/12/05 | Ben Jenkins | Added selling functionality with observer pattern
*
/******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager s_instance;
    public static InventoryManager s_Instance { get { return s_instance; } }

    //public List<Scrap> ScrapInInventory = new List<Scrap>();
    public HashSet<Scrap> ScrapInInventory = new HashSet<Scrap>();

    private int _tonsks=1;
    public TextMeshProUGUI DisplayName;
    public TextMeshProUGUI DisplayDescription;
    public TextMeshProUGUI DisplayCost;
    public TextMeshProUGUI DisplayPlayerAmount;
    public TextMeshProUGUI TonskAmount;
    private Scrap CurrentSelected;
    [SerializeField]
    public Image DisplaySprite;
    public GameObject Content;
    public event Action<Scrap> NewSelected;

    public UnityEvent<Scrap> ItemAdded;

    // Awake is called once at instantiation
    //Adds all scrap contained in the InventorySlots to the HashSet
    private void Awake()
    {
        if(s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        s_instance = this;
        
        foreach(Transform child in Content.gameObject.transform)
        {
            if(child.gameObject.tag.Equals("InventorySlot"))
            {
                if(child.gameObject.GetComponent<PlayerInventorySlot>().Scrap!=null)
                    ScrapInInventory.Add(child.gameObject.GetComponent<PlayerInventorySlot>().Scrap);
                //Debug.Log(ScrapInInventory.Last().Name);
            }
        }


    }

    public void AddTonsk(int amount)
    {
        _tonsks += amount;
        TonskAmount.text = "Balance: " + _tonsks;
    }

    public void SubtractTonsk(int amount)
    {
        if(_tonsks-amount>0)
        {
            _tonsks -= amount;
            TonskAmount.text = "Balance: " + _tonsks ;
        }
        else
        {
            Debug.LogError("Negative Tonsks");
        }
        
    }

    public void DisplayItem(Scrap scrap)
    {
        CurrentSelected = scrap;
        DisplayName.text = scrap.Name;
        DisplayDescription.text = scrap.Description;
        DisplayCost.text = "$" + scrap.Cost.ToString();
        DisplayPlayerAmount.text = scrap.PlayerAmount.ToString();
        DisplaySprite.sprite = scrap.Artwork;
        DisplaySprite.color = new Color(DisplaySprite.color.r, DisplaySprite.color.g, DisplaySprite.color.b, 1f);
        
        NewSelected?.Invoke(CurrentSelected);
    }

    //Check HashSet for if the scrap exists by its reference
    public (bool, Scrap found) FindItemInList(Scrap scrap)
    {
        foreach(Scrap Current in ScrapInInventory)
        {
            //Debug.Log(Current.name);
            if (Current.Equals(scrap))
                return (true,found: Current);
        }
        return (false,null);
    }//Returns a struct with a bool for if it was found and its reference if found

    //Check HashSet for if the scrap exists by its name
    public (bool, Scrap found) FindItemInList(string name)
    {
        foreach (Scrap Current in ScrapInInventory)
        {
            if (Current.name.Equals(name))
                return (true, found: Current);
        }
        return (false, null);
    }//Returns a struct with a bool for if it was found and its reference if found

    //Adds scrap to current Inventory, either by adding it to the HashSet or increasing the amount
    public void AddToPlayerInventory(Scrap scrap)
    {
        (bool FoundBool,Scrap FoundScrap) FoundReturn = FindItemInList(scrap);

        if(!FoundReturn.FoundBool)//If scrap does not exist in the HashSet, add it
        {
            ScrapInInventory.Add(scrap);
            //Debug.Log("Added to hashset");
            //Debug.Log("increased amount");
            scrap.PlayerAmount++;
            foreach (Transform child in Content.gameObject.transform)
            {
                Debug.Log(child.gameObject.tag);
                if (child.gameObject.tag.Equals("InventorySlot"))
                {
                    //Debug.Log("Found slot");
                    if (child.gameObject.GetComponent<PlayerInventorySlot>().Scrap == null)
                    {
                        child.gameObject.GetComponent<PlayerInventorySlot>().UpdateScrap(scrap);
                        //Debug.Log("added to slot");
                        break;
                    }
                    
                }
            }
        }
        else if(FoundReturn.FoundScrap)//If scrap does exist in the HashSet, increase the players amount
        {
            if(scrap == FoundReturn.FoundScrap)
            {
                scrap.PlayerAmount++;
                //Debug.Log("increased amount");
            }
        }

        ItemAdded.Invoke(scrap);
    }

    public void SubtractFromPlayerInventory(Scrap scrap)
    {
        (bool FoundBool, Scrap FoundScrap) FoundReturn = FindItemInList(scrap);

        if (!FoundReturn.FoundBool)//If scrap does not exist in the HashSet, send a log
        {
            Debug.LogWarning("Scrap not found in inventory");
        
        }
        else if (FoundReturn.FoundScrap)//If scrap does exist in the HashSet, subtract the players amount
        {
            if (scrap == FoundReturn.FoundScrap)
            {
                scrap.PlayerAmount--;
            }
        }

        if(scrap.PlayerAmount == 0)
        {
            RemoveFromPlayerInventory(scrap);
        }

    }

    public void RemoveFromPlayerInventory(Scrap scrap)
    {
        (bool FoundBool, Scrap FoundScrap) FoundReturn = FindItemInList(scrap);

        if (!FoundReturn.FoundBool)//If scrap does not exist in the HashSet, send a log
        {
            Debug.LogWarning("Scrap not found in inventory to remove");
        }
        else if (FoundReturn.FoundScrap)//If scrap does exist in the HashSet, remove it from the HashSet
        {
            if (scrap == FoundReturn.FoundScrap && scrap.PlayerAmount<=0)
            {
                ScrapInInventory.Remove(scrap);
                foreach (Transform child in Content.gameObject.transform)
                {
                    if (child.gameObject.tag.Equals("InventorySlot"))
                    {
                        if (child.gameObject.GetComponent<PlayerInventorySlot>().Scrap == scrap)
                        {
                            child.gameObject.GetComponent<PlayerInventorySlot>().Scrap=null;
                            child.gameObject.GetComponent<PlayerInventorySlot>().PlayerAmount.enabled = false;
                            child.gameObject.GetComponent<PlayerInventorySlot>().Sprite.enabled = false;

                            ClearDisplay();
                            break;
                        }

                    }
                }
            }
        }
    }

    public void ClearDisplay()
    {
        CurrentSelected = null;
        DisplayName.text = null;
        DisplayDescription.text = null;
        DisplayCost.text = null;
        DisplayPlayerAmount.text = null;
        DisplaySprite.sprite = null;
        DisplaySprite.color = new Color(DisplaySprite.color.r, DisplaySprite.color.g, DisplaySprite.color.b, 0f);
        NewSelected?.Invoke(CurrentSelected);
    }

    // Returns the total value of scrap in the player's inventory.
    public int ScrapValueInPlayerInventory()
    {
        // TODO: optimize if necessary, just store the value and update it when items are added/removed.

        int total = 0;
        foreach (Scrap scrap in ScrapInInventory)
        {
            total += scrap.PlayerAmount * scrap.Cost;
        }
        return total;
    }

    //Fix for items not going back into previous slots when previous item is removed: capture reference to previous slot during transform foreach,
    //and move scrap from current slot into previous one. Start loop with manual reference to first slot

    //Temp inventory for death: Separate Hashset that tracks what is added to the inventory from the start of a shift
    //Items still add normally, but if the player dies, items are removed from the inventory based on temp Hashset. Hashset is reset at the offshift


    //Shop is a subclass of inventory manager. At start of scene, make shop search for inventory manager. When "Sell" is pressed
    //get current display board item and sell, using Scrap.SellToShop


    /*
    public Scrap Geode;
    [ContextMenu("Add Geode")]
    public void AddGeode()
    {
        AddToPlayerInventory(Geode);
    }
    public Scrap Soda;*/
    public void AddSoda()
    {
        AddToPlayerInventory(banjo);
    }
    
    public Scrap banjo;
    public void RemoveBanjo()
    {
        SubtractFromPlayerInventory(banjo);
    }




}
