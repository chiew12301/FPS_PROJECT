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

    [Header("Player Model")]
    public GameObject PlayerObject;

    public Text CurrentObjectiveDescription;

    public FadeOut fo;
    public FadeIn fi;
    private int LastObjective = 0;
    private bool isFirstTime = false;
    void Start()
    {
        LastObjective = 0;
        isFirstTime = false;
        isCompletedFirstObjective = false;
        //fi = GameObject.Find("QuestLocationText").GetComponent<FadeIn>();
        //fo = GameObject.Find("QuestLocationText").GetComponent<FadeOut>();
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
            //checking for second objective
            float LatestDistance = 0.0f;
            for(int i = 1;i < PlayerObjectives.Length; i++)
            {
                if(PlayerObjectives[i].Status == Objective.ObjectiveStatus.Pending)
                {
                    float CalculateDistance = Vector3.Distance(PlayerObject.transform.position, PlayerObjectives[i].gameObject.transform.position);
                    if (LatestDistance <= 0.0f)
                    {
                        LatestDistance = CalculateDistance;
                        LastObjective = i;
                        isFirstTime = true;
                    }
                    if(LatestDistance > CalculateDistance)
                    {
                        LatestDistance = CalculateDistance;
                        LastObjective = i;
                    }
                }
            }
            CurrentObjective = PlayerObjectives[LastObjective];
            if (fo.gameObject.activeSelf != false)
            {
                fo.fadeout();
            }
            if (fi.gameObject.activeSelf != false)
            {
                fi.fadeIn();
            }
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
