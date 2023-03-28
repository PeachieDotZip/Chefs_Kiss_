using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    private Collider2D collider;
    [SerializeField]
    private int objectID;
    private PotBehaviour pot = null;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pot"))
            {
            pot = other.gameObject.GetComponent<PotBehaviour>();
            StartCoroutine(CookObject());
            }

    }


    public IEnumerator CookObject()
    {
        switch (objectID)
        {
            case 0:
                print("!!!");
                pot.testNum += 1;
                break;
            case 1:
                pot.eggNum += 1;
                break; 
            default:
                print("objectID out of knowable range!");
                break;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
