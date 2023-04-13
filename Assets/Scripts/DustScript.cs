/*****************************************************************************
// File Name :         DustScript.cs
// Author :            John H. Weber
// Creation Date :     Apr 12th, 2023
//
// Brief Description : This script is mostly just used to delete the dust effect so it doesnt take up space.
//                      Script can be added onto later for more use.
*****************************************************************************/
using UnityEngine;

public class DustScript : MonoBehaviour
{
    /// <summary>
    /// Destroys object and reports the destruction in the log.
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed " + gameObject.name);
    }
    // the explosion effect was mostly for funnies
}
