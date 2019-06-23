using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GroupInventory : MonoBehaviour
{
    public float groupWeight, maxWeight, score;
    public Dictionary<string, int> itemAmounts;
    public bool itemPickedUp;
    public Equipment Rope, Gold, Body;
    public TextMeshProUGUI weightText, scoreText;

    void Start()
    {
        maxWeight = GetComponent<InputManager>().players * 10;
        itemAmounts = new Dictionary<string, int>();
        itemAmounts.Add("Rope", 0);
        itemAmounts.Add("Gold", 0);
        itemAmounts.Add("Body", 0);
        groupWeight += GetComponent<InputManager>().players;
        UIUpdate();
    }

    public void ItemCollected()
    {
        groupWeight = (itemAmounts["Rope"] * Rope.weight) + (itemAmounts["Gold"] * Gold.weight) + (itemAmounts["Body"] * Body.weight) + GetComponent<InputManager>().players;
        UIUpdate();
        itemPickedUp = false;
    }

    void UIUpdate()
    {
        weightText.text = groupWeight + " / " + maxWeight;
        score = itemAmounts["Gold"] * 100;
        scoreText.text =  score + "";
    }
}
