/*****************************************************************************
// File Name :         GameManager.cs
// Author :            John H. Weber
// Creation Date :     Apr 9th, 2023
//
// Brief Description : Mainly just for testing purposes, real use will be decided later.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class GameManager : MonoBehaviour
{
    public GameObject enemyTest;
    public GameObject player1;
    public GameObject player2;
    //List<InputDevice> inputDevices;

    private void Start()
    {
        //PlayerInput.Instantiate(player1, 0, controlScheme: "Player1Input", pairWithDevice: Gamepad.all[0]);
        //PlayerInput.Instantiate(player2, 1, controlScheme: "Player2Input", pairWithDevice: Gamepad.all[1]);
        //inputDevices = new List<InputDevice>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //enemyTest.SetActive(true);
        }
    }
}
