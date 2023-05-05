using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject player;
    public StartingStarSceneAnimation StarAnimation;
    void Update()
    {
        player.transform.position = new Vector3(player.transform.position.x, Mathf.Cos(Time.time) * 0.1f + 3.0f, player.transform.position.z);
    }
    public void StartingScene() {
        SceneManager.LoadScene(0);
    }
    public void StartingSceneEasy() {
        UIManager.Easy = true;
        StartCoroutine(StarAnimation.StarAnimation());
    }
    public void StartingSceneMedium() {
        UIManager.Medium = true;
        StartCoroutine(StarAnimation.StarAnimation());
    }
    public void StartingSceneHard() {
        UIManager.Hard = true;
        StartCoroutine(StarAnimation.StarAnimation());
    }
}
