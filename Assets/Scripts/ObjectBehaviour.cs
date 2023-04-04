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

    // Start is called before the first frame update
    void Start()
    {
        objCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.Find("Player1").transform;
        p1 = playerPos.gameObject.GetComponent<Player1Controller>();
    }


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
            p1.holdTarget = null;
            isGrabbed = false;
        }

        if (isGrabbed == true)
        {

        }
    }
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
        //Spawn dust particle at position
    }
    /// <summary>
    /// Destroys object. Also called during "cook" animation.
    /// </summary>
    public void DestroyObject()
    {
        Debug.Log(gameObject.name + " deleted!");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistanceFromObj);
    }
}
