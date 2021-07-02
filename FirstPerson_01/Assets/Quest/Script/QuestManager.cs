using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox;
    public GameObject buttonPrefab;

    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    void Start()
    {
        //create each event
        QuestEvent a = quest.AddQuestEvent("test1", "description 1");
        QuestEvent b = quest.AddQuestEvent("test2", "description 2");
        QuestEvent c = quest.AddQuestEvent("test3", "description 3");
        QuestEvent d = quest.AddQuestEvent("test4", "description 4");
        QuestEvent e = quest.AddQuestEvent("test5", "description 5");

        //path order
        quest.AddPath(a.GetId(), b.GetId()); //a to b
        quest.AddPath(b.GetId(), c.GetId()); //b to c
        quest.AddPath(c.GetId(), d.GetId()); //c to d
        quest.AddPath(d.GetId(), e.GetId()); //d to e

        quest.BFS(a.GetId());

        QuestButton button = CreateButton(a).GetComponent<QuestButton>();
        A.GetComponent<QuestLocation>().Setup(this, a, button);

        button = CreateButton(b).GetComponent<QuestButton>();
        B.GetComponent<QuestLocation>().Setup(this, a, button);

        button = CreateButton(c).GetComponent<QuestButton>();
        C.GetComponent<QuestLocation>().Setup(this, a, button);

        button = CreateButton(d).GetComponent<QuestButton>();
        D.GetComponent<QuestLocation>().Setup(this, a, button);

        button = CreateButton(e).GetComponent<QuestButton>();
        E.GetComponent<QuestLocation>().Setup(this, a, button);

        quest.PrintPath();
    }

    GameObject CreateButton(QuestEvent e)
    {
        GameObject b = Instantiate(buttonPrefab);
        b.GetComponent<QuestButton>().Setup(e, questPrintBox);
        if(e.order == 1)
        {
            b.GetComponent<QuestButton>().UpdateButton(QuestEvent.EventStatus.CURRENT);
            e.status = QuestEvent.EventStatus.CURRENT;
        }
        return b;
    }

    public void UpdateQuestOnCompletion(QuestEvent e)
    {
        foreach (QuestEvent n in quest.questEvents)
        {
            //if this event is in next order
            if(n.order == (e.order + 1))
            {
                //make the next in line available for completion
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }
        }
    }
}
