using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Slider playerSlider;
    public GameObject infoHolder;
    public TextMeshProUGUI playerCount;

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mishu Scene");
    }


    public void PlayerSliderChange()
    {
        infoHolder.GetComponent<VariableStorage>().playersChosen = Convert.ToInt16(playerSlider.value);
        playerCount.text = Convert.ToString(playerSlider.value);
    }
}
