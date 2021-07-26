using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public Objective CurrentObjective;
    private Objective[] PlayerObjectives;
    //public Image CurrentObjectiveArrow;

    public Text CurrentObjectiveDescription;

    void Start()
    {
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

    void OnGUI()
    {
        this.CurrentObjectiveDescription.text = this.CurrentObjective.Description;
    }
}
