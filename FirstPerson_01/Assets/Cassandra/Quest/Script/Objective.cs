using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public enum ObjectiveType
    {
        Reach = 0,
        Talk = 1,
        Defeat = 2,
        Pick = 3,
    }

    public enum ObjectiveStatus
    {

        Pending = 0,
        Achieved = 1,

    }

    public enum ActionOnReach
    {
        MarkAsAchieved = 0,
        PlayCinematic = 1,
        PlayAnimation = 2,
        SetTrigger = 3,
        Count = 4,
    }

    public string Name;
    [Multiline(10)]
    public string Description;
    public ObjectiveType Kind;
    public ObjectiveStatus Status;
    public GameObject Target;
    public Objective NextObjective;
    private int count = 0;

    public ActionOnReach[] ActionsOnReach;

    public FadeOut fo;
    public FadeIn fi;

    //public Animator animator;
    //public string TriggerName;

    private void Start()
    {
        //fi = GameObject.Find("QuestLocationText").GetComponent<FadeIn>();
        //fo = GameObject.Find("QuestLocationText").GetComponent<FadeOut>();
        count = 0;
        if(fi.gameObject.activeSelf != false)
        {
            fi.fadeIn();
        }
    }

    private void OnReach()
    {
        if(this.ParentScript.CurrentObjective.name == "Quest")
        {
            ParentScript.isCompletedFirstObjective = true;
        }

        if (this.ActionsOnReach.Contains(ActionOnReach.MarkAsAchieved))
            this.Status = ObjectiveStatus.Achieved;

        if (this.ActionsOnReach.Contains(ActionOnReach.Count))
            this.Status = ObjectiveStatus.Achieved;
            count++;

        if (this.ActionsOnReach.Contains(ActionOnReach.PlayCinematic))
            this.PlayCinematic();

        if (this.ActionsOnReach.Contains(ActionOnReach.PlayAnimation))
            this.PlayAnimation();   

        //if (this.ActionsOnReach.Contains(ActionOnReach.SetTrigger))
           // this.NextObjective.Target.GetComponentInParent<animator>().SetTrigger(this.TriggerName);

        //ParentScript.CurrentObjective = this.NextObjective;
        fi.fadeIn();
    }

    private void PlayAnimation()
    {
        Debug.Log("On PlayAnimation: Not implemented yet");
    }

    private void PlayCinematic()
    {
        Debug.Log("On PlayCinematic: Not implemented yet ");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");

        if (other.tag == "Player")
        {
            if(this.ParentScript.CurrentObjective != null)
            {
                if(this.ParentScript.CurrentObjective.name == this.name)
                {
                    OnReach();

                    fo.fadeout();
                }
            }
        }
    }

   

    public Objectives ParentScript { get; set; }
}
