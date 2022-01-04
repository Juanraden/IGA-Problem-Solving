using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public TapText TapTextPrefab;
    public ScoreController score;
    public AudioSource audioSource;
    public AudioClip gameOver;
    public GameObject ballGameObject;
    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public Text FinalScoreText;
    public Text HighScoreText;
    public BallControl Ball
    {
        get
        {
            return ballGameObject.GetComponent<BallControl>();
        }
    }

    private List<TapText> _tapTextPool = new List<TapText>();

    public Text TimeText;
    public float gameTime;
    public float Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
            TimeText.text = _timer.ToString("0");
        }
    }
    private float _timer;

    public bool IsGameOver { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Timer = gameTime;
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        PlayGameOver();

        score.FinishScoring();

        ballGameObject.SetActive(false);
        GameOverPanel.SetActive(true);
        GamePanel.SetActive(false);

        FinalScoreText.text = "Your Score: " + score.GetCurrentScore();
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void CollectByTap(string tag, Vector3 tapPosition, Transform parent)
    {
        TapText tapText = GetOrCreateTapText();
        tapText.transform.SetParent(parent, false);
        tapText.transform.position = tapPosition;

        if (tag == "Square")
            tapText.Text.text = $"{ score.squareScore.ToString("0") }";
        if (tag == "Coin")
            tapText.Text.text = $"+{ score.coinScore.ToString("0") }";

        tapText.gameObject.SetActive(true);
    }

    private TapText GetOrCreateTapText()
    {
        TapText tapText = _tapTextPool.Find(t => !t.gameObject.activeSelf);
        if (tapText == null)
        {
            tapText = Instantiate(TapTextPrefab).GetComponent<TapText>();
            _tapTextPool.Add(tapText);
        }

        return tapText;
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOver);
    }
}
