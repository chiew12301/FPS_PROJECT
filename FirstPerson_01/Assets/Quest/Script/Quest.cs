using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();
    
    //List<QuestEvent> pathList = new List<QuestEvent>();

    public Quest() { }

    public QuestEvent AddQuestEvent(string n, string d)
    {
        QuestEvent questEvent = new QuestEvent(n, d);
        questEvents.Add(questEvent);
        return questEvent;
    }
    
    public void AddPath(string fromQuestEvent, string toQuestEvent)
    {
        QuestEvent from = FindQuestEvent(fromQuestEvent);
        QuestEvent to = FindQuestEvent(toQuestEvent);

        if(from !=null && to !=null)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathlist.Add(p);
        }
    }

    QuestEvent FindQuestEvent(string id)
    {
        foreach (QuestEvent n in questEvents)
        {
            if (n.GetId() == id)
                return n;
        }
        return null;
    }

    //BFS = Breadth first search
    public void BFS(string id, int orderNumber = 1)
    {
        QuestEvent thisEvent = FindQuestEvent(id);
        thisEvent.order = orderNumber;

        foreach (QuestPath e in thisEvent.pathlist)
        {
            if (e.endEvent.order == -1)
                BFS(e.endEvent.GetId(), orderNumber + 1);
        }
    }

    public void PrintPath()
    {
        foreach (QuestEvent n in questEvents)
        {
            Debug.Log(n.naming + " " + n.order);
        }
    }
}
