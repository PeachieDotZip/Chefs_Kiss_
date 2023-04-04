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

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Test"))
        {

        }
        if (!other.gameObject.CompareTag("Player"))
        {
            print("Ingredient Added: " + other.gameObject.tag);
        }
    }
}
