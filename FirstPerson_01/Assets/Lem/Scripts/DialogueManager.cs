using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private AudioClip dialogueAudio;

    private string[] fileLines;

    //Subtitle variables
    private List<string> subtitleLines = new List<string>();

    private List<string> subtitleTimingStrings = new List<string>();
    public List<float> subtitleTimings = new List<float>();

    public List<string> subtitleText = new List<string>();

    private int nextSubtitle = 0;

    //Triggers
    private List<string> triggerLines = new List<string>();

    private List<string> triggerTimingStrings = new List<string>();
    public List<float> triggerTimings = new List<float>();

    private List<string> triggers = new List<string>();
    public List<string> triggerObjectNames = new List<string>();
    public List<string> triggerMethodNames = new List<string>();

    private int nextTrigger = 0;

    //Singleton
    public static DialogueManager Instance { get; private set; }
    public AudioClip DialogueClip;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        gameObject.AddComponent<AudioSource>();
    }

    public void BeginDialogue (AudioClip dialogueClip)
    {
        dialogueAudio = dialogueClip;

        //Reset Lists
        subtitleLines = new List<string>();
        subtitleTimingStrings = new List<string>();
        subtitleTimings = new List<float>();
        subtitleText = new List<string>();

        triggerLines = new List<string>();
        triggerTimingStrings = new List<string>();
        triggerTimings = new List<float>();
        triggers = new List<string>();
        triggerObjectNames = new List<string>();
        triggerMethodNames = new List<string>();

        nextSubtitle = 0;
        nextTrigger = 0;

        //Get strings from text file
        TextAsset temp = Resources.Load("Dialogue/" + dialogueAudio.name) as TextAsset;

        //Set and Play Audio Clip
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = dialogueAudio;
        audio.Play();
        
    }
}
