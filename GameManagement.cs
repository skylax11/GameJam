using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
    public bool hasEverPressedV = false;
    [Header("Obstacles")]
    [SerializeField] GameObject[] white_Obstacles;
    [SerializeField] GameObject[] dark_Obstacles;
    [Header("Musics")]
    [SerializeField] public AudioSource musicPlayer;
    [SerializeField] AudioClip whiteDimensionMusic;
    [SerializeField] AudioClip darkDimensionMusic;
    public float whiteDimension_MusicSpeed = 0.08f;
    public float darkDimension_MusicSpeed = 0.08f;
    public float whiteClip_played;
    public float blackClip_played;
    float playTime;
    public float playSpeed;
    public bool allowToPlayWhite;
    public bool allowToPlayBlack;
    [Header("Raycast Hit")]
    [SerializeField] RaycastHit hit;
    [SerializeField] Transform position;
    [Header("Teleport Variables")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Door1;
    [SerializeField] public GameObject Door2;
    [SerializeField] public GameObject Door3;
    [SerializeField] public Vector3 white1;
    [SerializeField] public Vector3 dark1;
    [SerializeField] public Vector3 white2;
    [SerializeField] public Vector3 dark2;
    [SerializeField] public Vector3 white3;
    [SerializeField] public Vector3 dark3;
    public bool waitForBlack = true;
    public bool waitForWhite = true;
    public levelInfo theLevel;
    [Header("White & Black Unlocker Cubes")]
    public GameObject system_black;
    public GameObject system_white;
    public GameObject system_dark_Unlock;
    public GameObject system_white_Unlock;
    [SerializeField] GameObject black;
    [SerializeField] GameObject white;
    [SerializeField] GameObject dark_Unlock;
    [SerializeField] GameObject white_Unlock;
    [SerializeField] public GameObject black2;
    [SerializeField] public GameObject whitee2;
    [SerializeField] public GameObject dark_Unlock2;
    [SerializeField] public GameObject white_Unlock2;
    [Header("Volume Manager")]
    public SetBloom volumeManager;
    [Header("End Game")]
    [SerializeField] GameObject LosePanel;
    [SerializeField] AudioClip endGameMusic;
    public bool once = true;
    [Header("Singleton")]
    public static GameManagement instance;
    [Header("Other Voices")]
    [SerializeField] public AudioSource otherVoice;
    [SerializeField] public AudioClip key;
    [SerializeField] public AudioClip gate;
    public float maxSes;
    [Header("Settings Tab")]
    [SerializeField] GameObject SettingPanel;

    private void Awake()
    {
        instance = this;
    }
    IEnumerator ReturnMenu()
    {
        skyboxColor = SaveMaterial_white;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    private void Start()
    {

        maxSes = VoiceInfo.instance.voiceLevel;
        musicPlayer.volume = maxSes;
        otherVoice.volume = maxSes;
        system_black = black;
        system_white = white;
        system_dark_Unlock = dark_Unlock;
        system_white_Unlock = white_Unlock;

        waitForBlack = true;
        waitForWhite = true;
        theLevel = new levelInfo();
        theLevel.Door = Door1;
        theLevel.getLevel = level.level_1;
        theLevel.white_Transform = white1;
        theLevel.dark_Transform = dark1;
        playSpeed = whiteDimension_MusicSpeed;
    }
    private void Update()
    {
        if (enabledV)
        {
            Light.transform.rotation = Quaternion.Lerp(LightRot, DarkRot, 0.8f);
            RenderSettings.skybox.Lerp(skyboxColor, SaveMaterial_dark, Time.deltaTime * 5f);
        }
        else if (enabledB)
        {
            Light.transform.rotation = Quaternion.Lerp(DarkRot, LightRot, 0.8f);
            RenderSettings.skybox.Lerp(skyboxColor, SaveMaterial_white, Time.deltaTime * 5f);
        }
        Inputs();
        PlayMusic();
    }
    public void PlayMusic()
    {
        if (allowToPlayWhite) { playSpeed = whiteDimension_MusicSpeed; whiteClip_played = musicPlayer.time; playTime = whiteClip_played; }
        else if (allowToPlayBlack) { playSpeed = darkDimension_MusicSpeed; blackClip_played = musicPlayer.time; playTime = blackClip_played; }

        if (once && (playTime == musicPlayer.clip.length))
        {
            musicPlayer.clip = endGameMusic;
            musicPlayer.Play();
            LosePanel.GetComponent<Canvas>().enabled = true;
            once = false;
            StartCoroutine("ReturnMenu");
        }
    }
    public void Inputs()
    {
        if (!enabledV && waitForWhite)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                waitForBlack = false;
                hasEverPressedV = true;
                enableVFunc();
            }
        }
        if (waitForBlack && (!enabledB && hasEverPressedV == true))
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                waitForWhite = false;
                enableBFunc();
            }
        }
        #region Opening Door
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Inventory.instance.current_item.name == "Key" || Inventory.instance.current_item.name == "Unlocker")
            {
                if (Physics.Raycast(position.position, position.forward, out hit, 50f))
                {
                    print(hit.transform.name);
                    if (hit.transform.CompareTag("Door"))
                    {
                        otherVoice.clip = gate;
                        otherVoice.Play();
                        theLevel.Door.GetComponent<Animator>().SetBool("openDoor", true);
                        RemoveFromInventory();
                    }
                    if (hit.transform.CompareTag("Statue_White"))
                    {
                        if (Inventory.instance.current_item.topicOfExpert == "White")
                        {
                            RemoveFromInventory();
                            system_white.GetComponent<Renderer>().enabled = true;
                            system_white_Unlock.GetComponent<Renderer>().enabled = false;
                            system_white_Unlock.GetComponent<Collider>().enabled = false;
                        }
                    }
                    if (hit.transform.CompareTag("Statue_Dark"))
                    {
                        if (Inventory.instance.current_item.topicOfExpert == "Dark")
                        {
                            RemoveFromInventory();
                            system_black.GetComponent<Renderer>().enabled = true;
                            system_dark_Unlock.GetComponent<Renderer>().enabled = false;
                            system_dark_Unlock.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
            }
        }
        #endregion
        if(Input.GetKeyDown(KeyCode.Escape)) { SettingPanel.SetActive(true); Cursor.lockState = CursorLockMode.None;Cursor.visible = true; }

    }
    public void RemoveFromInventory()
    {
        Inventory.instance.items[Inventory.instance.current_item.id].Object.SetActive(false);
        Inventory.instance.slot_images[Inventory.instance.current_item.id].GetComponent<Image>().enabled = false;
        Inventory.instance.items[Inventory.instance.current_item.id] = new Item();
        Inventory.instance.items[Inventory.instance.current_item.id].Object = Inventory.instance.Empty_GAMEOBJECT;
        Inventory.instance.items[Inventory.instance.current_item.id].id = Inventory.instance.current_item.id;
    }
    public bool completed;
    public bool stop = true;
    bool run_once = true;
    public void playMusicDelay()
    {
        while (musicPlayer.volume >= 0.1f && stop)
        {
            musicPlayer.volume -= 0.08f;
            break;
        }
        if (musicPlayer.volume <= 0.1f)
        {
            completed = true;
            run_once = true;
            if (enabledV)
            {
                musicPlayer.clip = darkDimensionMusic;
                musicPlayer.time = blackClip_played;
                allowToPlayBlack = true;
                allowToPlayWhite = false;
                musicPlayer.Play();
            }
            else if (enabledB)
            {
                musicPlayer.clip = whiteDimensionMusic;
                musicPlayer.time = whiteClip_played;
                allowToPlayBlack = false;
                allowToPlayWhite = true;
                musicPlayer.Play();
            }
        }
        if (completed)
        {
            if (run_once) { stop = false; run_once = false; }

            while (musicPlayer.volume <= maxSes)
            {
                musicPlayer.volume += 0.08f;
                break;
            }
            if (musicPlayer.volume == maxSes)
            {
                completed = false;
                CancelInvoke("playMusicDelay");
            }
        }
    }
    public void enableVFunc()
    {
        // Saving Values
        whiteClip_played = musicPlayer.time;
        darkDimension_MusicSpeed += 0.005f;
        enabledV = true;
        enabledB = false;
        playSpeed = darkDimension_MusicSpeed;
        stop = true;
        completed = false;
        volumeManager.executeBlackSwitch();
        InvokeRepeating("playMusicDelay", 0.1f, 0.25f);
        Player.transform.localPosition = theLevel.dark_Transform;

        Light.transform.rotation = Quaternion.Lerp(LightRot, DarkRot, Time.deltaTime * 0.1f);
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
    public void enableBFunc()
    {
        blackClip_played = musicPlayer.time;
        whiteDimension_MusicSpeed += 0.005f;
        enabledB = true;
        enabledV = false;
        playSpeed = whiteDimension_MusicSpeed;
        stop = true;
        completed = false;
        volumeManager.executeWhiteSwitch();
        InvokeRepeating("playMusicDelay", 0.1f, 0.25f);

        Player.transform.localPosition = theLevel.white_Transform;

        Light.transform.rotation = Quaternion.Lerp(DarkRot, LightRot, Time.deltaTime * 0.1f);
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
    #region In-Game Settings

    public void SetVolume(float maxVolume)
    {
        maxSes = maxVolume;
        musicPlayer.volume = maxSes;
    }
    public void SetSens(float x)
    {
        PlayerCamera.Instance.sensX = x * 1000f;
        PlayerCamera.Instance.sensY = x * 1000f;
    }
    public void CloseSettingsTab()
    {
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        SettingPanel.SetActive(false);
    }
    #endregion
}
public class levelInfo
{
    public int level { get; set; }
    public level getLevel { get; set; }
    public Vector3 white_Transform { get; set; }
    public Vector3 dark_Transform { get; set; }
    public GameObject Door { get; set; }    

}
public enum level
{
    level_1,
    level_2, level_3
}