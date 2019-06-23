﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Gamepad[] gamepads = new Gamepad[Gamepad.all.Count];
    public Keyboard keyboard;
    public Gamepad gamepad1, gamepad2, gamepad3, gamepad4;
    public PlayerController player1, player2, player3, player4;
    public int players, assignedControllers, playersAlive;

    void Start()
    {
        gamepads = Gamepad.all.ToArray();
        AssignControllers();
        AssignPlayers();
    }

    void Update()
    {
        
    }

    void AssignControllers()
    {
        for (int i = 0; i < gamepads.Length; i++)
        {
            if (gamepad1 == null)
            {
                gamepad1 = gamepads[i];
                Debug.Log("Assigned");
                if (gamepad1 == null)
                {

                    Debug.Log("Gamepad " + i + " unassigned");
                }
                assignedControllers += 1;
            }

            else if (gamepad2 == null)
            {
                gamepad2 = gamepads[i];
                Debug.Log("Assigned");
                assignedControllers += 1;
            }

            else if (gamepad3 == null)
            {
                gamepad3 = gamepads[i];
                Debug.Log("Assigned");
                assignedControllers += 1;
            }

            else if (gamepad4 == null)
            {
                gamepad4 = gamepads[i];
                Debug.Log("Assigned");
                assignedControllers += 1;
            }
        }
    }

    void AssignPlayers()
    {
        if (players == 4 && assignedControllers == players)
        {
            player1.thisGamepad = gamepad1;
            player2.thisGamepad = gamepad2;
            player3.thisGamepad = gamepad3;
            player4.thisGamepad = gamepad4;
        }

        else if (players == 3 && assignedControllers == players)
        {
            player1.thisGamepad = gamepad1;
            player2.thisGamepad = gamepad2;
            player3.thisGamepad = gamepad3;
            player4.thisGamepad = null;
        }

        else if (players == 2 && assignedControllers == players)
        {
            player1.thisGamepad = gamepad1;
            player2.thisGamepad = gamepad2;
            player3.thisGamepad = null;
            player4.thisGamepad = null;
        }

        else if (players == 4 && assignedControllers != players)
        {
            player1.thisKeyboard = Keyboard.current;
            player1.keyboardOrGamepad = true;

            player2.thisGamepad = gamepad1;
            player3.thisGamepad = gamepad2;
            player4.thisGamepad = gamepad3;
        }

        else if (players == 3 && assignedControllers != players)
        {
            player1.thisKeyboard = Keyboard.current;
            player1.keyboardOrGamepad = true;

            player2.thisGamepad = gamepad1;
            player3.thisGamepad = gamepad2;
            player4.thisGamepad = null;
        }

        else if (players == 2 && assignedControllers != players)
        {
            player1.thisKeyboard = Keyboard.current;
            player1.keyboardOrGamepad = true;

            player2.thisGamepad = gamepad1;
            player3.thisGamepad = null;
            player4.thisGamepad = null;
        }

        else if(players == assignedControllers + 2)
        {
            Debug.Log("Too many players, not enough controllers");
        }
    }

    public void Dead(string player)
    {
        if (player == "player1")
        {
            player1.enabled = false;
        }

        else if (player == "player2")
        {
            player2.enabled = false;
        }

        else if (player == "player3")
        {
            player3.enabled = false;
        }

        else if (player == "player4")
        {
            player4.enabled = false;
        }
    }
}
