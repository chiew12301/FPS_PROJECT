using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("Assign Setting Obj")]
    public GameObject sObj;

    [Header("Slider")]
    [SerializeField] Slider BGMslider;
    [SerializeField] Slider SFXslider;
    // Start is called before the first frame update
    void Start()
    {
        BGMslider.value = AudioManager.instance.allBGMVolume;
        SFXslider.value = AudioManager.instance.allSFXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.instance.allBGMVolume = BGMslider.value;
        AudioManager.instance.allSFXVolume = SFXslider.value;
        EnemyAudioAddIn.instance.AdjustAll3DAudioVolume(SFXslider.value);
    }
}
