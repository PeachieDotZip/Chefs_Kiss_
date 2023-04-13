/*****************************************************************************
// File Name :         PotBehaviour.cs
// Author :            John H. Weber
// Creation Date :     Apr 5th, 2023
//
// Brief Description : Controls pot collision and holds values for ingredient numbers for dishes.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    private Collider2D potCollider;

    //ingredient numbers
    public int testNum;
    public int eggNum;
    public int carrotNum;
    public int pepperNum;
    public int spiceNum;

    // Start is called before the first frame update
    void Start()
    {
        potCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Debug code used for testing collisions with objects of different tags. Reports objects to console.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Test"))
        {

        }
        if (!other.gameObject.CompareTag("Player"))
        {
            print("Ingredient Added: " + other.gameObject.name);
        }
    }
}
