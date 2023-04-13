/*****************************************************************************
// File Name :         SceneManageScript.cs
// Author :            John H. Weber
// Creation Date :     Apr 13th, 2023
//
// Brief Description : Mainly just for testing purposes, real use will be decided later.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageScript : MonoBehaviour
{

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
