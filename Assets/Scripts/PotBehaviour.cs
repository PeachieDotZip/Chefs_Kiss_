/*****************************************************************************
// File Name :         PotBehaviour.cs
// Author :            John H. Weber
// Creation Date :     Apr 5th, 2023
//
// Brief Description : Controls pot collision and holds values for ingredient numbers for dishes.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    private Collider2D potCollider;

    //ingredient numbers
    public int testNum;
    public int carrotNum;
    public int waterNum;
    public int lettuceNum;
    public int flourNum;
    public int pepperNum;
    public int eggNum;
    public int cheeseNum;
    public int butterNum;
    [SerializeField]
    private int potID;

    private bool roomComplete = false;
    public Animator roomAnim;

    // Start is called before the first frame update
    void Start()
    {
        potCollider = GetComponent<Collider2D>();
        roomAnim = GetComponent<Animator>();
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
            print("Pot Touched " + other.gameObject.name);
        }
    }

    public void FixedUpdate()
    {
        switch (potID)
        {
            case 0:
                //Scrambled Eggs
                if (eggNum == 2)
                {
                    roomAnim.SetTrigger("Advance");
                    Debug.Log("Room 1 Complete!");
                }
                break;
            case 1:
                //Egg Salad
                if (eggNum == 2 && lettuceNum == 2)
                {
                    roomAnim.SetTrigger("Advance");
                    Debug.Log("Room 2 Complete!");
                }
                break;
            case 2:
                //Carrot Cake
                if (eggNum == 2 && lettuceNum == 1 && flourNum == 1 && carrotNum == 1 && waterNum == 1)
                {
                    roomAnim.SetTrigger("Advance");
                    Debug.Log("Room 3 Complete!");
                    Debug.Log("Tutorial Complete!");
                }
                break;
            default:
                print("potID out of knowable range!");
                break;
        }
    }
}
