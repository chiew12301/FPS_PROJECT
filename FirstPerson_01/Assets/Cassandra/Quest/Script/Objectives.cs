using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public Objective CurrentObjective;
    public bool isCompletedFirstObjective = false;
    private Objective[] PlayerObjectives;
    //public Image CurrentObjectiveArrow;

    public Text CurrentObjectiveDescription;

    private FadeOut fo;
    private FadeIn fi;

    void Start()
    {
        fi = GameObject.Find("QuestLocationText").GetComponent<FadeIn>();
        fo = GameObject.Find("QuestLocationText").GetComponent<FadeOut>();
        var objectiveParentGameObject = this.CurrentObjective.transform.parent.gameObject;
        if (objectiveParentGameObject != null)
        {
            this.PlayerObjectives = objectiveParentGameObject.GetComponentsInChildren<Objective>();
            if (this.PlayerObjectives != null)
            {
                Debug.Log("Successfully found all player objectives");
                foreach (Objective singleObjective in PlayerObjectives)
                {
                    if (singleObjective != null)
                    {
                        singleObjective.ParentScript = this;
                    }
                }
            }
            else
                Debug.LogError("Unable to find objectives");
        }
    }

    private void Update()
    {
        if(isCompletedFirstObjective == true)
        {
            float dis = 0.0f;
            for(int i = 1;i < PlayerObjectives.Length; i++)
            {
                if(PlayerObjectives[i].Status == Objective.ObjectiveStatus.Pending)
                {
                    float tempdis = Vector3.Distance(this.gameObject.transform.position, PlayerObjectives[i].gameObject.transform.position);
                    if(i == 1)
                    {
                        dis = tempdis;
                        CurrentObjective = PlayerObjectives[i];
                        fo.fadeout();
                    }
                    if (tempdis < dis)
                    {
                        dis = tempdis;
                        CurrentObjective = PlayerObjectives[i];
                        fo.fadeout();
                    }
                }
            }
            fi.fadeIn();
        }
    }

    void OnGUI()
    {
        if(CurrentObjective != null)
        {
            //CurrentObjectiveDescription.gameObject.SetActive(true);

            CurrentObjectiveDescription.text = CurrentObjective.Description; 
        }
        else
        {
            //CurrentObjectiveDescription.text = "Objective Complete";

            //CurrentObjectiveDescription.gameObject.SetActive(false);
        }

    }
}
