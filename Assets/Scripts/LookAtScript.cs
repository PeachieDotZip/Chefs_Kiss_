/*****************************************************************************
// File Name :         LookAtScript.cs
// Author :            John H. Weber
// Creation Date :     Apr 24th, 2023
//
// Brief Description : Used to make objects/enemies face the player at all times.
*****************************************************************************/
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    Transform target;
    public GameObject player;

    public float speed;

    public float rotationModifier;

    private void Start()
    {
        target = GameObject.Find("Player1").transform;
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }

    }
}
