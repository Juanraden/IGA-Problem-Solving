using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score;

    public Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        ScoreText.text = "Score: " + GetScore().ToString();
    }

    public float GetScore()
    {
        return score;
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
    }


}
