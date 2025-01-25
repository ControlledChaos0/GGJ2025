using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfExtension
{
    // https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
    public static int Mod(int x, int m) {
        return (x%m + m)%m;
    }

    public static float RandomizeByPercent(this float n, float percent) {
        return n * (1 + Random.Range(-percent, percent));
    }
}
