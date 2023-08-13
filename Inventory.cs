using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject Empty_GAMEOBJECT;
    [SerializeField] GameObject camParent;
    [SerializeField] GameObject[] slot_images;
    [SerializeField] Transform item_position;
    private int lastItem_index;
    private Item current_item;
    private Item[] items;
    void Start()
    {
        items = new Item[9];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new Item();
            items[i].Object = Empty_GAMEOBJECT;
            items[i].id = i;
        }
        current_item = items[0];
    }
    void Update()
    {
        Inputs();
    }
    public void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(camParent.transform.position, camParent.transform.forward, out hit, 50f))
            {
               if (slot_images[current_item.id].GetComponent<Image>().enabled == false)
               {
                  hit.transform.GetComponent<object_Item>().itemInfo.id = current_item.id;
                  items[current_item.id] = hit.transform.GetComponent<object_Item>().itemInfo;
                  items[current_item.id].Object = hit.transform.gameObject;
                  slot_images[current_item.id].GetComponent<Image>().enabled = true;
                  slot_images[current_item.id].GetComponent<Image>().sprite = hit.transform.GetComponent<object_Item>().itemInfo.sprite;
                  hit.transform.parent = item_position;
                  current_item = items[current_item.id];
               }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            switchItem(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            switchItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            switchItem(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            switchItem(8);
        }
    }
    public void switchItem(int index)
    {
        print("before:"+current_item.id);
        if (items[current_item.id].Object != null)
        {
            if (items[current_item.id].Object.GetComponent<Renderer>() != null)
            {
                print(items[current_item.id].name);

                items[current_item.id].Object.GetComponent<Renderer>().enabled = false;
            }
            if (items[current_item.id].Object.GetComponent<Collider>() != null)
            {
                items[current_item.id].Object.GetComponent<Collider>().enabled = false;
            }
        }

        if (current_item != null)
        {
            current_item = items[index];
        }
        print("after:" + current_item.id);

        if (current_item.Object != null)
        {
            if (current_item.Object.GetComponent<Renderer>() != null)
            {
                current_item.Object.GetComponent<Renderer>().enabled = true;
            }
            if (current_item.Object.GetComponent<Collider>() != null)
            {
                current_item.Object.GetComponent<Collider>().enabled = true;
            }
        }
    }
}
