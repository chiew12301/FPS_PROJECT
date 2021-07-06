using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform reticle;
    public Transform player;
    public GameObject hitFeedback;

    public float size;
    public float runSize;
    public float walkSize;
    public float speed;
    private float currentSize;
    // Start is called before the first frame update
    void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovementNew>().isRunning)
        {
            currentSize = Mathf.Lerp(currentSize, runSize, Time.deltaTime * speed);
        }
        else if(player.GetComponent<PlayerMovementNew>().isWalking)
        {
            currentSize = Mathf.Lerp(currentSize, walkSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, size, Time.deltaTime * speed);
        }
        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }
    
}
