/*****************************************************************************
// File Name :         GameManager.cs
// Author :            John H. Weber
// Creation Date :     Apr 9th, 2023
//
// Brief Description : Mainly just for testing purposes, real use will be decided later.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyTest;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyTest.SetActive(true);
        }
    }
}
