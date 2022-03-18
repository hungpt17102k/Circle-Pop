using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    public static float timeToMaxDifficlut = 30f;

    public static float GetDifficultyPercent() {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / timeToMaxDifficlut);
    }
}
