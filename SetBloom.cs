using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class SetBloom : MonoBehaviour
{
    [Header("Post Processing Variables")]
    [SerializeField] Volume volume;
    Bloom bloom;
    LiftGammaGain lift;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Close_Panel;
    [SerializeField] AudioClip WinningClip;

    public float value = 0.1f;
    public float value2 = 1f;
    bool completed = false;

    private void Start()
    {
        volume.profile.TryGet(out lift);
    }
    public void SetTheBloom()
    {
        GetComponent<Collider>().enabled = false;
        InvokeRepeating("RepeatBloom", 0.1f, 0.1f);
        InvokeRepeating("SetMusic", 0.1f, 0.2f);  
    }
    public void SetMusic()
    {
        while (GameManagement.instance.musicPlayer.volume >= 0f)
        {
            GameManagement.instance.musicPlayer.volume -= 0.08f;
            break;
        }
        if (GameManagement.instance.musicPlayer.volume <= 0f)
        {
            completed = true;
        }
        if (completed)
        {
            GameManagement.instance.whiteClip_played = 0f;
            GameManagement.instance.blackClip_played = 0f;
            CancelInvoke("SetMusic");
        }
    }
    IEnumerator Teleport_Level2()
    {
        yield return new WaitForSeconds(5f);
        CancelInvoke("RepeatBloom");
        InvokeRepeating("DecreaseBloom", 0.1f, 0.1f);
        Player.transform.localPosition = GameManagement.instance.white2;
        GameManagement.instance.theLevel.Door = GameManagement.instance.Door2;
        GameManagement.instance.theLevel.getLevel = level.level_2;
        GameManagement.instance.theLevel.white_Transform = GameManagement.instance.white2;
        GameManagement.instance.theLevel.dark_Transform = GameManagement.instance.dark2;
    }
    IEnumerator Teleport_Level3()
    {
        yield return new WaitForSeconds(5f);
        CancelInvoke("RepeatBloom");
        InvokeRepeating("DecreaseBloom", 0.1f, 0.1f);
        Player.transform.localPosition = GameManagement.instance.white3;
        GameManagement.instance.theLevel.Door = GameManagement.instance.Door3;
        GameManagement.instance.theLevel.getLevel = level.level_3;
        GameManagement.instance.theLevel.white_Transform = GameManagement.instance.white3;
        GameManagement.instance.theLevel.dark_Transform = GameManagement.instance.dark3;

        GameManagement.instance.system_black = GameManagement.instance.black2;
        GameManagement.instance.system_white = GameManagement.instance.whitee2;
        GameManagement.instance.system_dark_Unlock = GameManagement.instance.dark_Unlock2;
        GameManagement.instance.system_white_Unlock = GameManagement.instance.white_Unlock2;
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(4f);
        Panel.GetComponent<Canvas>().enabled = true;
        Close_Panel.GetComponent<Canvas>().enabled = false;
        GameManagement.instance.musicPlayer.clip = WinningClip;
        GameManagement.instance.musicPlayer.volume = 1;
        GameManagement.instance.once = false;
        GameManagement.instance.musicPlayer.Play();
    }
    public void RepeatBloom()
    {
        while (value < 2f)
        {
            value += 0.03f;
            break;
        }
        if (value <= 2)
        {
            lift.lift.Override(new Vector4(value, value, value, value));
        }
    }
    public void DecreaseBloom()
    {
        while (value >= 0)
        {
            value -= 0.03f;
            break;
        }
        if (value >= 0)
        {
            lift.lift.Override(new Vector4(value, value, value, value));
        }
        if (value <= 0)
        {
            lift.lift.Override(new Vector4(0, 0, 0, 0));
            GameManagement.instance.musicPlayer.pitch = 1f;
            GameManagement.instance.musicPlayer.time = 0f;
            GameManagement.instance.musicPlayer.volume = GameManagement.instance.maxSes;
            CancelInvoke("DecreaseBloom");
        }
    }
    public void executeBlackSwitch()
    {
        InvokeRepeating("switchDimensionBlack", 0.1f, 0.2f);
    }
    public void executeWhiteSwitch()
    {
        InvokeRepeating("switchDimensionWhite", 0.1f, 0.2f);
    }
    IEnumerator switchWhiteTransition_IENUMERATOR()
    {
        yield return new WaitForSeconds(0.4f);
        CancelInvoke("switchDimensionWhite");
        InvokeRepeating("lowerWhite", 0.1f, 0.1f);
    }
    IEnumerator switchBlackTransition_IENUMERATOR()
    {
        yield return new WaitForSeconds(0.4f);
        CancelInvoke("switchDimensionBlack");
        InvokeRepeating("lowerBlack", 0.1f, 0.1f);
    }
    public void switchDimensionWhite()
    {
        while (value2 < 1f)
        {
            value2 += 0.07f;
            break;
        }
        if (value2 <= 1f)
        {
            lift.lift.Override(new Vector4(value2, value2, value2, value2));
        }
        if (value2 >= 1f)
        {
            StartCoroutine("switchWhiteTransition_IENUMERATOR");
        }
    }
    public void lowerWhite()
    {
        while (value2 >= 0)
        {
            value2 -= 0.07f;
            break;
        }
        if (value2 >= 0)
        {
            lift.lift.Override(new Vector4(value2, value2, value2, value2));
        }
        if (value2 <= 0)
        {
            print("aasdasdasdad");
            lift.lift.Override(new Vector4(0, 0, 0, 0));
            CancelInvoke("lowerWhite");
            value2 = -0.1f;
            GameManagement.instance.waitForWhite = true;
        }
    }
    public void switchDimensionBlack()
    {
        while (value2 > -0.7f)
        {
            value2 -= 0.07f;
            break;
        }
        if (value2 > -0.7f)
        {
            lift.lift.Override(new Vector4(value2, value2, value2, value2));
        }
        if (value2 <= -0.7f)
        {
            StartCoroutine("switchBlackTransition_IENUMERATOR");
        }
    }
    public void lowerBlack()
    {
        while (value2 <= 0)
        {
            value2 += 0.07f;
            break;
        }
        if (value2 <= 0)
        {
            lift.lift.Override(new Vector4(value2, value2, value2, value2));
        }
        if (value2 >= 0)
        {
            lift.lift.Override(new Vector4(0, 0, 0, 0));
            CancelInvoke("lowerBlack");
            value2 = 0.7f;
            GameManagement.instance.waitForBlack=true;
        }
    }
}

