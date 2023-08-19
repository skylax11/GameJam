using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceInfo : MonoBehaviour
{
    public float sensX=700;
    public float sensY=700;
    public float voiceLevel;
    public static VoiceInfo instance;
    private void Start()
    {
        sensX = 700f;
        sensY = 700f;
        instance = this;
        DontDestroyOnLoad(this);
    }
    public void SetVoice(float x)
    {
        voiceLevel = x;
    }
    public void SensX(float deger)
    {
        sensX = deger;
        sensY = deger;
    }

}
