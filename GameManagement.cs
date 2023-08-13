using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [Header("Lightning")]
    [SerializeField] GameObject Light;
    [SerializeField] Quaternion LightRot;
    [SerializeField] Quaternion DarkRot;
    [Header("Dark & White Sky Maps")]
    [SerializeField] Material skyboxColor;
    [SerializeField] Material SaveMaterial_dark;
    [SerializeField] Material SaveMaterial_white;
    public bool enabledV = false;
    public bool enabledB = false;
    [Header("Obstacles")]
    [SerializeField] GameObject[] white_Obstacles;
    [SerializeField] GameObject[] dark_Obstacles;
    
    private void Start()
    {

    }
    private void Update()
    {
        if (enabledV)
        {
            print("a");
            Light.transform.rotation = Quaternion.Lerp(LightRot, DarkRot, 0.8f);
            RenderSettings.skybox.Lerp(skyboxColor, SaveMaterial_dark, Time.deltaTime * 5f);
        }
        else if (enabledB)
        {
            print("v");
            Light.transform.rotation = Quaternion.Lerp(DarkRot, LightRot, 0.8f);
            RenderSettings.skybox.Lerp(skyboxColor, SaveMaterial_white, Time.deltaTime * 5f);
        }
        Inputs();
    }
    public void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            enabledV = true;
            enabledB = false;
            foreach (GameObject o in white_Obstacles)
            {
                o.GetComponent<Renderer>().enabled = false;
                o.GetComponent<Collider>().enabled = false;
            }
            foreach (GameObject o in dark_Obstacles)
            {
                o.GetComponent<Renderer>().enabled = true;
                o.GetComponent<Collider>().enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            enabledB = true;
            enabledV = false;
            Light.transform.rotation = Quaternion.Lerp(DarkRot, LightRot,Time.deltaTime * 0.1f);
            foreach (GameObject o in dark_Obstacles)
            {
                o.GetComponent<Renderer>().enabled = false;
                o.GetComponent<Collider>().enabled = false;
            }
            foreach (GameObject o in white_Obstacles)
            {
                o.GetComponent<Renderer>().enabled = true;
                o.GetComponent<Collider>().enabled = true;
            }
        }
    }
}
