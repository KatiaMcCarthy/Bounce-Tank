using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath 
{
   public static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    {
        float discriminate = b * b - 4 * a * c;
        if(discriminate < 0)
        {
            root1 = Mathf.Infinity;
            root2 = -root1;

            return 0;
        }
        root1 = (-b + Mathf.Sqrt(discriminate)) / (2 * a);
        root2 = (-b - Mathf.Sqrt(discriminate)) / (2 * a);

        return discriminate > 0 ? 2 : 1;
    }
}
