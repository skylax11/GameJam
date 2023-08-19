using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Displayer : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] Transform aim;
    GameObject temporary;
    void Start()
    {

    }

    void Update()
    {
        if (Physics.Raycast(aim.position, aim.forward, out hit, 30f))
        {
            if (hit.transform.GetComponentInChildren<Canvas>() != null)
            {
                hit.transform.GetComponentInChildren<Canvas>().enabled = true;
                temporary = hit.transform.gameObject;
            }
            else
            {
                if (temporary != null)
                {
                    temporaryFunc();
                }
            }
        }
        else
        {
            temporaryFunc();
        }
    }
    public void temporaryFunc()
    {
        if (temporary != null)
        {
            if(!temporary.CompareTag("Player") && temporary.GetComponentInChildren<Canvas>() != null)
            temporary.GetComponentInChildren<Canvas>().enabled = false;
        }
    }
}
