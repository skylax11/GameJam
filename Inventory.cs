using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Hit")]
    RaycastHit hit;
    [Header("Inventory Stuff")]
    [SerializeField] public GameObject Empty_GAMEOBJECT;
    [SerializeField] GameObject camParent;
    [SerializeField] public GameObject[] slot_images;
    [SerializeField] Transform item_position;
    public int lastItem_index;
    public Item current_item;
    public Item[] items;
    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }
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
                  if (hit.transform.CompareTag("Item"))
                    {
                        if (hit.transform.GetComponent<Collider>() != null)
                        {
                            hit.transform.GetComponent<Collider>().enabled = false;
                        }
                       
                        hit.transform.gameObject.layer = LayerMask.NameToLayer("Item");
                      hit.transform.GetComponent<object_Item>().itemInfo.id = current_item.id;
                      items[current_item.id] = hit.transform.GetComponent<object_Item>().itemInfo;
                      items[current_item.id].Object = hit.transform.gameObject;
                      slot_images[current_item.id].GetComponent<Image>().enabled = true;
                      slot_images[current_item.id].GetComponent<Image>().sprite = hit.transform.GetComponent<object_Item>().itemInfo.sprite;
                        hit.transform.parent = item_position;
                        hit.transform.rotation = new Quaternion(0, 0, 90, 0);
                        hit.transform.localPosition = Vector3.zero;
                        current_item = items[current_item.id];
                        if (current_item.name == "Key")
                        {
                            GameManagement.instance.otherVoice.clip = GameManagement.instance.key;
                            GameManagement.instance.otherVoice.Play();
                        }
                    }
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
        if (items[current_item.id].Object != null)
        {
            if (items[current_item.id].Object.GetComponent<Renderer>() != null)
            {
                print(items[current_item.id].name);

                items[current_item.id].Object.GetComponent<Renderer>().enabled = false;
            }
        }
        if (current_item != null)
        {
            current_item = items[index];
        }
        if (current_item.Object != null)
        {
            if (current_item.Object.GetComponent<Renderer>() != null)
            {
                current_item.Object.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
