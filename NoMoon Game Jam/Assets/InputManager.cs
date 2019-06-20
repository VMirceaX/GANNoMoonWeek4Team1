using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Gamepad[] gamepads = new Gamepad[4];
    public Keyboard keyboard;

    void Update()
    { 
        for (int i = 0; i < gamepads.Length; i++)
        {
            if (gamepads[i] == null)
            {
                gamepads[i] = Gamepad.current;
                Debug.Log("Controller Assigned");

                if (gamepads[i] == null)
                {
                    Debug.Log("No More Controllers!");
                }
            }

            else if(gamepads[i] != null)
            {
                Debug.Log("All Cool Here chief");
            }
        }
    }
}
