/*****************************************************************************
// File Name :         EnemyBehaviour.cs
// Author :            John H. Weber
// Creation Date :     Apr 12th, 2023
//
// Brief Description : Controls enemy movement and provides bools to be used in animations for when enemies should move.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    private bool chasing;
    private Animator anim;
    public bool dazed;
    private Rigidbody2D rb;
    private KnockbackFeedback knockback;
    private ObjectBehaviour objB;
    // Start is called before the first frame update
    void Start()
    {
        chasing = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<KnockbackFeedback>();
        objB = GetComponent<ObjectBehaviour>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.forward;

            transform.position = Vector2.MoveTowards(this.transform.position,
            player.transform.position, speed * Time.deltaTime);
        }
        //Sets whether objectbehaviour is enabled or not to whether the enemy is dazed or not.
        //Therefore, enemies can only be picked up and cooked while dazed.
        objB.enabled = dazed;
        //rb.freezeRotation = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pan"))
        {
            anim.SetTrigger("hit");
            knockback.Knockback();
        }
    }
    /// <summary>
    /// Called during animations. Tells the enemy to chase the player.
    /// </summary>
    public void EnemyChase()
    {
        chasing = true;
        dazed = false;
    }
    /// <summary>
    /// Called during animations. Tells the enemy to stop chasing the player.
    /// </summary>
    public void EnemyStop()
    {
        chasing = false;
        rb.velocity = Vector2.zero;
    }
    public void EnemyStunEnable()
    {
        chasing = false;
        dazed = true;
    }
    public void EnemyStunDisable()
    {
        if (objB.isGrabbed)
        {
            Player1Controller p1 = FindAnyObjectByType<Player1Controller>();
            p1.Drop();
        }
        chasing = true;
        dazed = false;
        transform.rotation = Quaternion.identity;
        rb.freezeRotation = true;
        anim.ResetTrigger("hit");
    }
}
