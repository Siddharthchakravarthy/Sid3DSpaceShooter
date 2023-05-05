using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    void Start()
    {
        RemainingShipsLetIntoTheAtmosphere = ShipsThatCanBeLetIntoTheAtmosphere;
        TimeRemainingInSeconds = GameTotalTime;
        ScoreText.text = "Score: " + Score;
        Damage.text = "Damage: " + Lives;
        ShipsRemainingText.text = "Ships Earth Can Handle: " + RemainingShipsLetIntoTheAtmosphere;
        GameOverText.gameObject.SetActive(false);
        GameWonText.gameObject.SetActive(false);
    }
    public void GameOverScreen() {
        GameOvera = true;
        FloatingJoyStick1.gameObject.SetActive(false);
        FloatingJoyStick2.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true);
    }

    public void GameWonScreen() {
        
        FloatingJoyStick1.gameObject.SetActive(false);
        FloatingJoyStick2.gameObject.SetActive(false);
        GameWonText.gameObject.SetActive(true);
        GameWonAnimations gameAnimation = GameWonText.GetComponent<GameWonAnimations>();
        StartCoroutine(gameAnimation.PlayTheTextSequence());
    }
    void Update()
    {
        TimeRemainingInSeconds = TimeRemainingInSeconds - Time.deltaTime;
        if(TimeRemainingInSeconds < 0 && GameWonOnce == false) {
            TimeRemainingInSeconds = 0;
            GameWonOnce = true;
            GameWonScreen();
        }
        if(GameWonOnce) {
            TimeRemainingInSeconds = 0;
            RemainingShipsLetIntoTheAtmosphere = 100000;
        }
        if(GameOvera) {
            TimeRemainingInSeconds = GameTotalTime;
            RemainingShipsLetIntoTheAtmosphere = 0;
        }

        minutes = Mathf.FloorToInt(TimeRemainingInSeconds / 60);
        seconds = Mathf.FloorToInt(TimeRemainingInSeconds % 60);
        
        RemainingTimer.text = string.Format("REINFORCEMENTS IN : {00}:{1:00}", minutes, seconds);
        ScoreText.text = "Score: " + Score;
        Damage.text = "Damage: " + Lives;
        ShipsRemainingText.text = "Ships Earth Can Handle: " + RemainingShipsLetIntoTheAtmosphere;
    }
    [SerializeField]
    private Joystick FloatingJoyStick1;
    [SerializeField]
    private Joystick FloatingJoyStick2;
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text GameOverText;
    [SerializeField]
    private TMP_Text GameWonText;
    [SerializeField]
    private TMP_Text Damage;

    [SerializeField]
    private Text RemainingTimer;
    public static int Score = 0;
    [SerializeField]
    private TMP_Text ShipsRemainingText;
    private int ShipsThatCanBeLetIntoTheAtmosphere = 10;
    public static int RemainingShipsLetIntoTheAtmosphere;
    //Global counters for time
    public static float TimeRemainingInSeconds = 10.00f;
    public static float minutes;
    public static float seconds;
    private bool GameWonOnce = false;

    private float GameTotalTime = 300.0f;
    public static int Level = 0;
    public static int Lives = 5;

    private bool GameOvera = false;

    public static bool Easy = false;
    public static bool Medium = false;
    public static bool Hard = false;
}