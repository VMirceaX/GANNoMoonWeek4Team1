using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionScript : MonoBehaviour
{
    public Equipment item;

    void Gold()
    {

    }

    void Rope()
    {

    }

    void Update()
    {
        Invoke(item.itemName, 0);
    }
}
