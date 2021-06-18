using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private AudioClip dialogueAudio;

    //Bitrate of audio 
    private const float _RATE = 44100.0f;

    private string[] fileLines;

    //Subtitle variables
    private List<string> subtitleLines = new List<string>();

    private List<string> subtitleTimingStrings = new List<string>();
    public List<float> subtitleTimings = new List<float>();

    public List<string> subtitleText = new List<string>();

    private int nextSubtitle = 0;

    private string displaySubtitle;

    //Triggers
    private List<string> triggerLines = new List<string>();

    private List<string> triggerTimingStrings = new List<string>();
    public List<float> triggerTimings = new List<float>();

    private List<string> triggers = new List<string>();
    public List<string> triggerObjectNames = new List<string>();
    public List<string> triggerMethodNames = new List<string>();

    private int nextTrigger = 0;

    //GUI
    private GUIStyle subtitleStyle = new GUIStyle();

    //Singleton
    public static DialogueManager Instance { get; private set; }
    public AudioClip DialogueClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        gameObject.AddComponent<AudioSource>();
    }

    public void BeginDialogue(AudioClip dialogueClip)
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
        TextAsset temp = Resources.Load("Dialogue/" + "DialogueScripts") as TextAsset;
        fileLines = temp.text.Split('\n');

        //Split subtitle and trigger lines into different lists
        foreach (string line in fileLines)
        {
            if (line.Contains("<trigger/>"))
            {
                triggerLines.Add(line);
            }
            else
            {
                subtitleLines.Add(line);
            }
        }

        //Split subtitle elements
        for (int i = 0; i < subtitleLines.Count; i++)
        {
            string[] splitTemp = subtitleLines[i].Split('|');
            subtitleTimingStrings.Add(splitTemp[0]);
            subtitleTimings.Add(float.Parse(CleanTimeString(subtitleTimingStrings[i])));
            subtitleText.Add(splitTemp[1]);
        }

        //Split trigger elements
        for(int i = 0; i < triggerLines.Count; i++)
        {
            string[] splitTemp1 = triggerLines[i].Split('|');
            triggerTimingStrings.Add(splitTemp1[0]);
            triggerTimings.Add(float.Parse(CleanTimeString(triggerTimingStrings[i])));
            triggers.Add(splitTemp1[1]);

            string[] splitTemp2 = triggers[i].Split('-');
            splitTemp2[0] = splitTemp2[0].Replace("<trigger/>", "");
            triggerObjectNames.Add(splitTemp2[0]);
            triggerMethodNames.Add(splitTemp2[1]);
        }

        //sett initial subtitle text
        if(subtitleText[0] != null)
        {
            displaySubtitle = subtitleText[0];
        }

        //Set and Play Audio Clip
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = dialogueAudio;
        audio.Play();

    }

    //Remove characters that are not timing 
    private string CleanTimeString(string timeString)
    {
        //If things fk up it's this line.
        Regex digitsOnly = new Regex(@"[^\d+(\.\d+)*s]");
        return digitsOnly.Replace(timeString, "");
    }

    private void OnGUI()
    {
        //check if dialogueAudio is there
        if(dialogueAudio != null && GetComponent<AudioSource>().clip.name == dialogueAudio.name)
        {
            //Check for <break/> or negative nextSubtitle
            if (nextSubtitle > 0 && !subtitleText[nextSubtitle - 1].Contains("<break/"))
            {
                //Create GUI
                GUI.depth = -1001;
                subtitleStyle.fixedWidth = Screen.width / 1.5f;
                subtitleStyle.wordWrap = true;
                subtitleStyle.alignment = TextAnchor.MiddleCenter;
                subtitleStyle.normal.textColor = Color.white;
                subtitleStyle.fontSize = Mathf.FloorToInt(Screen.height * 0.0225f);

                Vector2 size = subtitleStyle.CalcSize(new GUIContent());
                //Create highlight for text
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(Screen.width / 2 - size.x / 2 + 1, Screen.height / 1.25f - size.y + 1, size.x, size.y), displaySubtitle, subtitleStyle);
                GUI.contentColor = Color.white;
                GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 1.25f - size.y, size.x, size.y), displaySubtitle, subtitleStyle);
            }

            //Next Subtitle when time point is hit
            if (nextSubtitle < subtitleText.Count)
            {
                AudioSource audio = GetComponent<AudioSource>();
                if (audio.timeSamples / _RATE > subtitleTimings[nextSubtitle])
                {
                    displaySubtitle = subtitleText[nextSubtitle];
                    nextSubtitle++;
                }
            }

            if (nextTrigger < triggers.Count)
            {
                AudioSource audio = GetComponent<AudioSource>();
                if (audio.timeSamples / _RATE > subtitleTimings[nextSubtitle])
                {
                    GameObject.Find(triggerObjectNames[nextTrigger]).SendMessage(triggerMethodNames[nextTrigger]);
                    nextTrigger++;
                }
            }
        }        
    }
}
