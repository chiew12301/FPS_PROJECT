using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();

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

        quest.PrintPath();
    }
}
