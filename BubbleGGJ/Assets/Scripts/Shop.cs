

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : InventoryManager
{
    //Add to NPC shop gameobject and set content to whatever contains the InventorySlots
    public GameObject PlayerInventorySpot;

    [SerializeField]
    private InventoryManager _pInventoryManager;
    private Scrap CurrentSelected;
    [SerializeField]
    private GameObject _playerInventory;
    private Vector3 _previousPISpot;
    private Vector3 _previousPIScale;
    private Vector3 _DBScale;
    private GameObject _SIC;
    private GameObject _DB;

    // Awake is called once at instantiation
    void Awake()
    {
        //FindInventory();
        _previousPISpot = _playerInventory.transform.position;
        _previousPIScale = _playerInventory.transform.localScale;
        _DBScale = _DB.transform.localScale;
    }

    //When enabled, subscribe to display board selection
    private void OnEnable()
    {
        
        if (_pInventoryManager != null)
        {
            _pInventoryManager.NewSelected += UpdateSelected;
        }
        _playerInventory.SetActive(true);
        _playerInventory.GetComponent<RectTransform>().anchoredPosition = new Vector3(-320f, 260f, 0f);
        _playerInventory.GetComponent<RectTransform>().transform.localScale = new Vector3(2f, 2f, 2f);
        _DB.GetComponent<RectTransform>().transform.localScale = new Vector3(2f, 2f, 2f);
        _DB.GetComponent<Image>().enabled = true;
        _SIC.SetActive(true);
        
        //hard coded for demo
        foreach(Transform transform in _SIC.transform)
        {
            if(transform.gameObject.CompareTag("TurnOff"))
            {
                transform.gameObject.SetActive(false);
            }
            if(transform.gameObject.CompareTag("TurnOffImage"))
            {
                transform.gameObject.GetComponent<Image>().enabled = false;
            }
        }


    }

    //When disabled, unsubscribe
    private void OnDisable()
    {
        if (_pInventoryManager != null)
        {
            _pInventoryManager.NewSelected -= UpdateSelected;
        }
        _playerInventory.SetActive(false);
        _playerInventory.GetComponent<RectTransform>().transform.position = _previousPISpot;
        _playerInventory.GetComponent<RectTransform>().transform.localScale = _previousPIScale;
        _DB.GetComponent<RectTransform>().transform.localScale = _DBScale;
        _DB.GetComponent<Image>().enabled = false;
        _SIC.SetActive(false);

        //hard coded for demo
        foreach (Transform transform in _SIC.transform)
        {
            if (transform.gameObject.CompareTag("TurnOff"))
            {
                transform.gameObject.SetActive(true);
            }
            if (transform.gameObject.CompareTag("TurnOffImage"))
            {
                transform.gameObject.GetComponent<Image>().enabled = true;
            }
        }
    }
    /*
    private void FindInventory()
    {
        foreach(InventoryManager inv in Resources.FindObjectsOfTypeAll<InventoryManager>())
        {
            if (inv.gameObject.CompareTag("DisplayBoard"))
            {
                _pInventoryManager = inv;
                _DB = inv.gameObject;
            }
        }
        foreach(GameObject gameObject in Object.FindObjectsByType(typeof(GameObject), )
        {
            if(gameObject.CompareTag("Inventory"))
            {
                _playerInventory = gameObject;
            }
        }
        foreach (GameObject gameObject in Object.FindObjectsByType(typeof(GameObject), includeInactive: true))
        {
            if (gameObject.CompareTag("ShipInteriorCanvas"))
            {
                _SIC = gameObject;
            }
        }
    } */

    //Subscription
    private void UpdateSelected(Scrap scrap)
    {
        CurrentSelected = scrap;
    }

    //Put on sell button in shop. Uses subscribed scrap to add to shop inventory, remove from player, and give them tonsks
    public void Sold()
    {
        if(CurrentSelected == null)
        {
            Debug.Log("Nothing selected");
            return;
        }
        AddToInventory(CurrentSelected);
        CurrentSelected.SoldToShop();
        _pInventoryManager.AddTonsk(CurrentSelected.Cost);
        if(CurrentSelected.PlayerAmount<=0)
        {
            _pInventoryManager.RemoveFromPlayerInventory(CurrentSelected);
        }
    }

    //modified AddToPlayerInventory
    public void AddToInventory(Scrap scrap)
    {
        (bool FoundBool, Scrap FoundScrap) FoundReturn = FindItemInList(scrap);

        if (!FoundReturn.FoundBool)//If scrap does not exist in the HashSet, add it
        {
            ScrapInInventory.Add(scrap);
            //Debug.Log("Added to hashset");
            //Debug.Log("increased amount");
            foreach (Transform child in Content.gameObject.transform)
            {
                //Debug.Log(child.gameObject.tag);
                if (child.gameObject.tag.Equals("InventorySlot"))
                {
                    //Debug.Log("Found slot");
                    if (child.gameObject.GetComponent<InventorySlots>().Scrap == null)
                    {
                        child.gameObject.GetComponent<InventorySlots>().UpdateScrap(scrap);
                        //Debug.Log("added to shop");
                        break;
                    }
                }
            }
        }
        else if (FoundReturn.FoundScrap)//If scrap does exist in the HashSet, increase the players amount
        {
            if (scrap == FoundReturn.FoundScrap)
            {
                Debug.Log("increased shop amount");
            }
        }
    }

}
