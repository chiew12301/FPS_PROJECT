using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeFrequencyHolder : MonoBehaviour
{
    Image myImageComponent;

    public Sprite lowVolume;
    public Sprite mediumVolume;
    public Sprite loudVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        myImageComponent = gameObject.GetComponent<Image>();
        myImageComponent.sprite = lowVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && myImageComponent.sprite == loudVolume)
        {
            myImageComponent.sprite = lowVolume;
        }
        else if(Input.GetKeyDown(KeyCode.F) && myImageComponent.sprite == lowVolume)
        {
            myImageComponent.sprite = mediumVolume;
        }
        else if (Input.GetKeyDown(KeyCode.F) && myImageComponent.sprite == mediumVolume)
        {
            myImageComponent.sprite = loudVolume;
        }
    }

}
