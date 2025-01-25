using System;
using UnityEngine;

public class CustomMath
{
    public static float Sigmoid(float value)
    {
        float k = Mathf.Exp(value);
        return 1 / (1.0f + k);
    }
}
