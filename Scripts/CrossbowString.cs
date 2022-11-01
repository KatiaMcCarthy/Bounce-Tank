using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowString : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRend;
    [SerializeField] private Transform boltPostion;
    // Update is called once per frame
    void Update()
    {
        if (lineRend.GetPosition(1) != boltPostion.localPosition)
        {
            lineRend.SetPosition(1, boltPostion.localPosition);
        }
    }
}
