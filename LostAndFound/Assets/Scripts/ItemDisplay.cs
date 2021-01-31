using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    int numberOfItems;
    [SerializeField] TextMeshProUGUI itemText;

    void Start() 
    {
        itemText.text = numberOfItems.ToString();
        if(numberOfItems==0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    
    public void AddItemsToInventory()
    {
        numberOfItems++;
        itemText.text = numberOfItems.ToString();
        if(numberOfItems > 0)
        {
            gameObject.SetActive(true);
        }
    }

    public void TakeItemsFromInventory(int itemsTaken)
    {
        numberOfItems -= itemsTaken;
        itemText.text = numberOfItems.ToString();
    }

    public int GetNumberOfItems()
    {
        return numberOfItems;
    }

}
