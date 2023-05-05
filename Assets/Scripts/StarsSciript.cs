using UnityEngine;
using System.Collections;
public class StarsSciript : GameStartsAndStarsSparkle
{
    public Material starsMaterial;
    void Awake() {
        ResetColor();
        StartCoroutine(ColorChanger());
    }

    void ResetColor() {
        starsMaterial.color = new Color(1.0f, 0.478f, 0.0f);
    }
    IEnumerator ColorChanger() {
        while(true) {
            if(GameStartsAndStarsSparkle.GameHasStarted == true) {
                starsMaterial.color = new Color(Mathf.Cos(Time.time), 0.478f * Mathf.Cos(Time.time), 0.0f);
            }
            yield return null;
        }
    }
}
