/*****************************************************************************
// File Name :         KnockbackFeedback.cs
// Author :            John H. Weber
// Creation Date :     Apr 26th, 2023
//
// Brief Description : Calculates and appies knockback to enemies when hit.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float strength = 1.0f, delay = 0.15f;

    public GameObject sender;


    public void Knockback()
    {
        StopAllCoroutines();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction*strength, ForceMode2D.Impulse);
        rb.freezeRotation = true;
        transform.rotation = Quaternion.identity;
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {       
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.Lerp(new Vector2(1, 1), new Vector2(0, 0), 2f);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
