using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{

    public int numberOfSeconds = 120;
    public TMP_Text timerTextBox;
    

    void Start()
    {
        StartCoroutine(Timer());
    }


    void Update()
    {

    }
    IEnumerator Timer()
    {
        while (numberOfSeconds > 0)
        {
         
            yield return new WaitForSeconds(1);
            numberOfSeconds -= 1;
            timerTextBox.text = numberOfSeconds.ToString();
        }
    }
}
