using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int startScore = 0;
    public static ScoreController Score { get { return score; } }
    private static ScoreController score;

    private readonly List<IScore> scoreItems = new List<IScore>();

    public int getScore { get { return scoreCount; } }
    private int scoreCount;

    private void Awake()
    {
        if (!score)
            score = this;
        Load();
        ScoreUpdate();
    }
    public void ScoreUpdate(int addNumber = 0)
    {
        scoreCount += addNumber;
        for (int i = 0; i < scoreItems.Count; i++)
            scoreItems[i].UpdateCount(scoreCount);
    }
    public void AddItem( IScore item )
    {
        item.UpdateCount(scoreCount);
        scoreItems.Add(item);
    }
    public void RemoveItem(IScore item)
    {
        scoreItems.Remove(item);
    }
    public void Load()
    {
        scoreCount = startScore;
    }
}
