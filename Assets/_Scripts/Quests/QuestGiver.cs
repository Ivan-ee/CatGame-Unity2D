using System.Collections;
using System.Collections.Generic;
using _Scripts.Quests;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest quest;

    public void GiveQuest()
    {
        var quests = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        quests.AddQuest(quest);
    }
}
