using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        player.transform.position = new Vector3(player.transform.position.x, Mathf.Cos(Time.time) * 0.1f + 3.0f, player.transform.position.z);
    }
    public void StartGame() {
        SceneManager.LoadScene(1);
    }
}
