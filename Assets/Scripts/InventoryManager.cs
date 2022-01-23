using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class InventoryManager : MonoBehaviour
{
    public XmlLoader xmlLoader;

    public InventoryItem slot1;
    public InventoryItem slot2;

    public Image slot1Image;
    public Image slot2Image;

    private GameObject truckObject1;
    private GameObject truckObject2;

    private int inventorySpace = 2;

    private string fileName = "Items";
    public InventoryContainer itemList;

    // Start is called before the first frame update
    void Start()
    {
        itemList = xmlLoader.Load(itemList, fileName);

        foreach(InventoryItem item in itemList._items)
        {
            if(item._leftPath != "")
            {
                item._leftSprite = Resources.Load<Sprite>(item._leftPath);
            }
            if(item._rightPath != "")
            {
                item._rightSprite = Resources.Load<Sprite>(item._rightPath);
            }
        }

        slot1Image.enabled = false;
        slot2Image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckForItem(string name)
    {
        bool success = false;

        if(slot1._name == name)
        {
            success = true;
        }
        else if(slot2._name == name)
        {
            success = true;
        }

        return success;
    }

    public bool AddItem(string name, GameObject truckObj = null)
    {
        InventoryItem newItem = null;
        bool success = false;

        foreach(InventoryItem item in itemList._items)
        {
            if(item._name == name)
            {
                newItem = item;
            }
        }

        if(newItem != null)
        {
            // Check there's space
            if(inventorySpace >= newItem._size)
            {
                if(newItem._size == 2)
                {
                    // Fill both slots
                    slot1 = newItem;
                    slot2 = newItem;

                    slot1Image.sprite = newItem._leftSprite;
                    slot2Image.sprite = newItem._rightSprite;

                    slot1Image.enabled = true;
                    slot2Image.enabled = true;

                    truckObject1 = truckObj;
                }
                else
                {
                    if(slot1Image.enabled == false)
                    {
                        slot1 = newItem;
                        slot1Image.sprite = newItem._leftSprite;
                        slot1Image.enabled = true;
                        truckObject1 = truckObj;
                    }
                    else
                    {
                        slot2 = newItem;
                        slot2Image.sprite = newItem._leftSprite;
                        slot2Image.enabled = true;
                        truckObject2 = truckObj;
                    }
                }

                inventorySpace -= newItem._size;

                success = true;
            }
        }

        return success;
    }

    public void RemoveItem(string name)
    {
        // Check slot 1
        if(slot1._name == name)
        {
            // Check size.
            if(slot1._size == 2)
            {
                // Remove both.
                slot1 = null;
                slot2 = null;

                slot1Image.enabled = false;
                slot2Image.enabled = false;

                truckObject1 = null;

                inventorySpace += 2;
            }
            else
            {
                // Remove 1 and move 2 into 1.
                slot1 = slot2;
                slot2 = null;

                slot1Image.sprite = slot2Image.sprite;
                slot1Image.enabled = slot2Image.enabled;
                slot2Image.enabled = false;

                truckObject1 = truckObject2;
                truckObject2 = null;

                inventorySpace += 1;
            }
        }
        else if(slot2._name == name)
        {
            slot2 = null;
            slot2Image.enabled = false;
            truckObject2 = null;

            inventorySpace += 1;
        }
    }

    public List<GameObject> ReturnTruckItems()
    {
        List<GameObject> truckItems = new List<GameObject>();

        // Big item
        if (truckObject1 != null && slot1._size == 2)
        {
            truckItems.Add(truckObject1);
            RemoveItem(slot1._name);
        }
        else
        {
            // Small items
            if (truckObject1 != null)
            {
                truckItems.Add(truckObject1);
                RemoveItem(slot1._name);

                // Moved from slot 2
                if(truckObject1 != null)
                {
                    truckItems.Add(truckObject1);
                    RemoveItem(slot1._name);
                }
            }
        }

        return truckItems;
    }
}

[System.Serializable]
public class InventoryItem
{
    [XmlElement("Name")]
    public string _name;

    [XmlElement("Size")]
    public int _size = 1;

    [XmlElement("ImageLeft")]
    public string _leftPath;

    [XmlElement("ImageRight")]
    public string _rightPath;

    public Sprite _leftSprite;
    public Sprite _rightSprite;
}

[System.Serializable]
[XmlRoot("ItemContainer")]
public class InventoryContainer
{
    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<InventoryItem> _items;
}

