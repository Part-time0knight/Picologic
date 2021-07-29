using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Score { get { return score; } }
    private static ScoreController score;

    [SerializeField] private Text ScoreText;

    private int scoreCount;

    private void Awake()
    {
        if (!score)
            score = this;
        scoreCount = Load();
        ScoreUpdate();
    }
    public void ScoreUpdate(int addNumber = 0)
    {
        scoreCount += addNumber;
        ScoreText.text = "" + scoreCount;
    }
    public int Load()
    {
        return 0;
    }
}
