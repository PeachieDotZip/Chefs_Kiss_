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
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    private Collider2D objCollider;
    [SerializeField]
    private int objectID;
    private PotBehaviour pot = null;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform playerPos;
    private readonly float maxDistanceFromObj = 2.5f;
    private Player1Controller p1;
    private bool isGrabbed;
    public GameObject dustEffect;
    [SerializeField]
    private GameObject icon;

    // Start is called before the first frame update
    void Start()
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
    }
    /// <summary>
    /// Tells the object what to do when coming in contact with the pot.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pot"))
            {
            pot = other.gameObject.GetComponent<PotBehaviour>();
            anim.SetTrigger("cook");
            objCollider.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }

    }
    /// <summary>
    /// Handles what ingredient this object should be counted as and spawn dust effect.
    /// Called during "cook" animation.
    /// </summary>
    public void CookObject()
    {
        switch (objectID)
        {
            case 0:
                Debug.Log("!!!");
                pot.testNum += 1;
                break;
            case 1:
                pot.eggNum += 1;
                break;
            case 2:
                pot.carrotNum += 1;
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
