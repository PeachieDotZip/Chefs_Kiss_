/*****************************************************************************
// File Name :         ObjectBehaviour.cs
// Author :            John H. Weber
// Creation Date :     Apr 5th, 2023
//
// Brief Description : This script controls many things such as whether or not it is being held, 
//                     collision with the pot, and animation triggers. It also holds it's own object ID.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectBehaviour : MonoBehaviour
{
    [SerializeField]
    private int objectID;
    //Object ID is used to identify which ingredient this object counts for.
    //The list goes as follows:
    // 0 = test
    // 1 = carrot
    // 2 = water
    // 3 = lettuce
    // 4 = flour
    // 5 = pepper
    // 6 = egg
    // 7 = cheese
    // 8 = butter

    private Collider2D objCollider;
    private PotBehaviour pot = null;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform playerPos;
    public float maxDistanceFromObj = 2.5f; // max distance away from object that the player can be to grab it
    private Player1Controller p1;
    public bool isGrabbed;
    public GameObject dustEffect;
    [SerializeField]
    private GameObject icon;
    public bool isEnemy;
    private EnemyBehaviour enemyB;

    /// <summary>
    /// Start function simply assigns some private variables
    /// </summary>
    void Start()
    {
        objCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.Find("Player1").transform;
        p1 = playerPos.gameObject.GetComponent<Player1Controller>();
        if (isEnemy == true)
        {
            enemyB = GetComponent<EnemyBehaviour>();
            this.enabled = false;
        }
        else
        {
            enemyB = null;
        }
    }
    void OnEnable()
    {
        objCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.Find("Player1").transform;
        p1 = playerPos.gameObject.GetComponent<Player1Controller>();
    }

    /// <summary>
    /// This update function records the distance between this object and the player.
    /// If the player is within the given radius, the object can be picked up when the player makes a grab attempt.
    /// </summary>
    void Update()
    {
        float dist = Vector3.Distance(playerPos.position, transform.position);
        if (dist <= maxDistanceFromObj)
        {
            if (p1.grabAttempt == true)
            {
                objCollider.enabled = false;
                rb.freezeRotation = true;
                p1.isHolding = true;
                p1.holdTarget = gameObject;
                isGrabbed = true;
            }
        }
        if (p1.dropAttempt == true)
        {
            objCollider.enabled = true;
            rb.freezeRotation = false;
            p1.isHolding = false;
            p1.holdTarget = null;
            isGrabbed = false;
        }

        if (isGrabbed == true)
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //playerPos = GameObject.Find("Player1(Clone)").transform;
            //p1 = playerPos.gameObject.GetComponent<Player1Controller>();
        }
    }
    /// <summary>
    /// Tells the object what to do when coming in contact with the pot.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pot"))
        {
            if (isEnemy == true)
            {
                if (enemyB.dazed == true)
                {
                    pot = other.gameObject.GetComponent<PotBehaviour>();
                    anim.SetTrigger("cook");
                    objCollider.enabled = false;
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                }
            }
            else
            {
                pot = other.gameObject.GetComponent<PotBehaviour>();
                anim.SetTrigger("cook");
                objCollider.enabled = false;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }

    }
    /// <summary>
    /// Handles what ingredient this object should be counted as and spawn dust effect.
    /// Called during "cook" animation.
    /// </summary>
    public void CookObject()
    {
        // Object ID list:
        // 0 = test
        // 1 = carrot
        // 2 = water
        // 3 = lettuce
        // 4 = flour
        // 5 = pepper
        // 6 = egg
        // 7 = cheese
        // 8 = butter
        switch (objectID)
        {
            case 0:
                Debug.Log("!!!");
                pot.testNum += 1;
                break;
            case 1:
                pot.carrotNum += 1;
                break;
            case 2:
                pot.waterNum += 1;
                break;
            case 3:
                pot.lettuceNum += 1;
                break;
            case 4:
                pot.flourNum += 1;
                break;
            case 5:
                pot.pepperNum += 1;
                break;
            case 6:
                pot.eggNum += 1;
                break;
            case 7:
                pot.cheeseNum += 1;
                break;
            case 8:
                pot.butterNum += 1;
                break;
            default:
                print("objectID out of knowable range!");
                break;
        }

        Instantiate(dustEffect, gameObject.transform.position, Quaternion.identity);
        // Instantiates a dust particle at the position of the object for extra effect.
        // It just kinda looks cool lol.
    }
    /// <summary>
    /// Destroys object. Also called during "cook" animation.
    /// </summary>
    public void DestroyObject()
    {
        Debug.Log(gameObject.name + " deleted!");
        icon.SetActive(true);
        Destroy(gameObject);
    }

    /// <summary>
    /// Debug code. Draws the object's radius.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistanceFromObj);
    }
}
