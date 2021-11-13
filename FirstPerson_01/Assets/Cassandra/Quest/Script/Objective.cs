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

    public ActionOnReach[] ActionsOnReach;

    public FadeOut fo;
    public FadeIn fi;

    [Header("For Objectives that required to pick up certain items")]
    public Item[] itemData;

    private bool forPickQuestLoop = false;
    private bool playerHitting = false;
    //public Animator animator;
    //public string TriggerName;

    private void Start()
    {
        forPickQuestLoop = false;
        playerHitting = false;
        //fi = GameObject.Find("QuestLocationText").GetComponent<FadeIn>();
        //fo = GameObject.Find("QuestLocationText").GetComponent<FadeOut>();
        if (fi.gameObject.activeSelf != false)
        {
            fi.fadeIn();
        }
    }

    private void Update()
    {
        if(forPickQuestLoop == true && playerHitting == true)
        {
            OnReach();
        }
    }

    private void OnReach()
    {
        if (Kind == ObjectiveType.Reach) // the objective type is reach the destination and complete
        {
            if (this.ParentScript.CurrentObjective.name == "Quest")
            {
                ParentScript.isCompletedFirstObjective = true;
            }

            if (this.ActionsOnReach.Contains(ActionOnReach.MarkAsAchieved))
                this.Status = ObjectiveStatus.Achieved;

            if (this.ActionsOnReach.Contains(ActionOnReach.Count)) //this work because is only apply for our second objective, is not flexible
            {
                this.Status = ObjectiveStatus.Achieved;
                this.ParentScript.secondObjectiveCompletedCount++;
            }


            if (this.ActionsOnReach.Contains(ActionOnReach.PlayCinematic))
                this.PlayCinematic();

            if (this.ActionsOnReach.Contains(ActionOnReach.PlayAnimation))
                this.PlayAnimation();
        }
        else if (Kind == ObjectiveType.Pick) //pick up the item only complete
        {//objective 2
            bool CheckIsEnough = false;
            forPickQuestLoop = true;
            foreach (Item i in itemData)
            {
                if (Inventory.instance.CheckHaveItem(i) == true) //this also work for multiple item, because if not enough item will break and return false.
                {
                    //means have item
                    CheckIsEnough = true;
                }
                else
                {
                    //means don't have
                    CheckIsEnough = false;
                    break; //break to say not enough
                }
                Debug.Log("ITEM FOUND STATUS IN OBJECTIVE" + CheckIsEnough);
            }
            if (CheckIsEnough == true) //means enough item, objective completed
            {
                if (this.ActionsOnReach.Contains(ActionOnReach.Count)) //this work because is only apply for our second objective, is not flexible
                {
                    this.Status = ObjectiveStatus.Achieved;
                    this.ParentScript.secondObjectiveCompletedCount++;
                }        

                if (this.ActionsOnReach.Contains(ActionOnReach.PlayCinematic))
                {
                    this.PlayCinematic();
                }


                if (this.ActionsOnReach.Contains(ActionOnReach.PlayAnimation))
                {
                    this.PlayAnimation();
                }
                    
            }
            else //means not enough, objective not complete
            {
                this.Status = ObjectiveStatus.Pending;
            }       

        }
        else if(Kind == ObjectiveType.Defeat) //defeat = complete
        {
            //not for now
        }
        else if(Kind == ObjectiveType.Talk) // talk = complete
        {
            //not for now
        }

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
        Debug.Log("Entering Zone" + this.name);

        if (other.tag == "Player")
        {
            playerHitting = true;
            if(this.ParentScript.CurrentObjective != null)
            {
                if(this.ParentScript.CurrentObjective.name == this.name)
                {
                    OnReach();

                    //fo.fadeout();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " Exiting");
        if (other.tag == "Player")
        {
            playerHitting = false;
        }
    }


    public Objectives ParentScript { get; set; }
}
