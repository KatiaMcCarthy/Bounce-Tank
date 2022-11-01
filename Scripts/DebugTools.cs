using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause");
            Time.timeScale = 0;
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1;
        }
    }
}
