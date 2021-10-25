using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class EnemyAudioAddIn : MonoBehaviour
{
    public static EnemyAudioAddIn instance;

    [SerializeField]
    private AudioSource[] All3DSound;

    [SerializeField]
    private float All3DAudioVolume = 0.5f;

    void Awake()
    {
        if (instance == null) //For Multiple Scene Purpose
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        All3DAudioVolume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustAllVolume(All3DAudioVolume);
    }

    /// <summary>
    /// Function to Change all 3D audio
    /// </summary>
    /// <param name="amount">Volume Amount to Change</param>
    public void AdjustAll3DAudioVolume(float amount)
    {
        All3DAudioVolume = amount;
    }


    private void AdjustAllVolume(float amount)
    {
        foreach (AudioSource s in All3DSound)
        {
            if(s != null)
            {
                if (amount >= 1)
                {
                    amount = 1;
                }
                else if (amount <= 0)
                {
                    amount = 0;
                }
                s.volume = amount;
            }
        }
    }
}
