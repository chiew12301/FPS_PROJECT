using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public AudioClip dialogueClip;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.BeginDialogue(dialogueClip);
        }
    }
}
