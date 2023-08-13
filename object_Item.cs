using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_Item : MonoBehaviour
{
    public Item itemInfo;
    private void Start()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;

    }
}
