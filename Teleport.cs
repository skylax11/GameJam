using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Vector3 teleport;
    [SerializeField] GameObject Unlock;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.localPosition = teleport;
            Unlock.SetActive(false);
        }
    }
}
