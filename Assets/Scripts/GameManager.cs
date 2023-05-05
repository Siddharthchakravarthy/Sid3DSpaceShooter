using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool IsGameOver;

    // Update is called once per frame
    void Update()
    {
        if(IsGameOver) {
            SceneManager.LoadScene(0);
            IsGameOver = false;
        }
    }

    public void GameOver() {
        IsGameOver = true;
    }
}
