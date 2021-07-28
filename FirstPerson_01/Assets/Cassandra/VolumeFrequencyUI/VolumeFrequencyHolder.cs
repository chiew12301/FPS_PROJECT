using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeFrequencyHolder : MonoBehaviour
{
    Image myImageComponent;

    public Sprite smallVolume;
    public Sprite mediumVolume;
    public Sprite loudVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        myImageComponent = gameObject.GetComponent<Image>();
        myImageComponent.sprite = smallVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && myImageComponent.sprite == loudVolume)
        {
            myImageComponent.sprite = smallVolume;
        }
        else if(Input.GetKeyDown(KeyCode.F) && myImageComponent.sprite == smallVolume)
        {
            myImageComponent.sprite = mediumVolume;
        }
        else if (Input.GetKeyDown(KeyCode.F) && myImageComponent.sprite == mediumVolume)
        {
            myImageComponent.sprite = loudVolume;
        }
    }

}
