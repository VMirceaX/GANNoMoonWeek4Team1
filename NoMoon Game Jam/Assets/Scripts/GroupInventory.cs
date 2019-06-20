using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupInventory : MonoBehaviour
{
    public float groupWeight;
    public Dictionary<string, int> itemAmounts;
    public bool itemPickedUp;
    public Equipment Rope, Gold; 

    void Start()
    {
        itemAmounts = new Dictionary<string, int>();
        itemAmounts.Add("Rope", 0);
        itemAmounts.Add("Gold", 0);
    }

    void ItemCollected()
    {
        groupWeight = (itemAmounts["Rope"] * Rope.weight) + (itemAmounts["Gold"] * Gold.weight);
    }
}
