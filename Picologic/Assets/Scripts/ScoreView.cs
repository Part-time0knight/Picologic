using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour, IScore
{

    private Text _text;
    private void Start()
    {
        _text = GetComponent<Text>();
        ScoreController.Score.AddItem(this);
    }
    public void UpdateCount(int count)
    {
        _text.text = "" + count;
    }
    private void OnDestroy()
    {
        ScoreController.Score.RemoveItem(this);
    }
}
