using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartingStarSceneAnimation : MonoBehaviour
{
    public IEnumerator StarAnimation(){
        float i = 1;
        float Increment = 0.1f;
        while(i < 30.0f){
            transform.localScale = Vector3.right * transform.localScale.x + Vector3.up * transform.localScale.y + Vector3.forward * i;
            i = i + Increment;
            yield return null;
        }
        while(i > 0) {
            transform.localScale = Vector3.right * transform.localScale.x + Vector3.up * transform.localScale.y + Vector3.forward * i;
            i = i - Increment;
            if(i < 15.0f) {
                Increment = Increment + 0.1f;
            }
            yield return null;
        }
        yield return null;
        SceneManager.LoadScene(1);
    }
}
