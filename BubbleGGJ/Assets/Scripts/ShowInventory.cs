
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject inventory;
    public bool isActive;
    public GameObject pI;
    

    private void Start()
    {
        inventory.SetActive(isActive);
    }

    public void OnInventory()
    {
        Debug.Log($"{inventory.activeSelf}");
        if (inventory.activeSelf)
        {
            inventory.SetActive(false);
            pI.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
            pI.SetActive(true);
        }
    }
}
