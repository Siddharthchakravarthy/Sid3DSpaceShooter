using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartsAndStarsSparkle : MonoBehaviour
{   
    public static bool GameHasStarted = false; 
    void Update() {
        if(UIManager.TimeRemainingInSeconds < 300.0f) {
            GameHasStarted = true;
        }
    }
}
