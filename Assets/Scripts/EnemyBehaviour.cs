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
    // Start is called before the first frame update
    void Start()
    {
        chasing = false;
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
    }
    /// <summary>
    /// Called during animations. Tells the enemy to chase the player.
    /// </summary>
    public void EnemyChase()
    {
        chasing = true;
    }
    /// <summary>
    /// Called during animations. Tells the enemy to stop chasing the player.
    /// </summary>
    public void EnemyStop()
    {
        chasing = false;
    }
}
