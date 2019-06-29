using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VariableStorage : MonoBehaviour
{
    public int playersChosen;
    public float highestScore;
    

    public void DontDestroy()
    {
        UnityEngine.Object.DontDestroyOnLoad(this);
    }

}
