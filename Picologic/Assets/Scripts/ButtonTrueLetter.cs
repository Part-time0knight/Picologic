using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrueLetter : MonoBehaviour, IButton, IScore
{
    [SerializeField] private GameObject gameMain;
    [SerializeField] private int count = 30;
    [SerializeField] private Text countView;
    [SerializeField] private Color active = Color.white;
    [SerializeField] private Color blocked = new Color(0.75f, 0.75f, 075f, 0.5f);
    private IGameController game;
    private Button button;
    private Image image;
    public bool Active
    {
        get { return button.enabled; }
        set
        {
            button.enabled = value;
            if (value)
                image.color = active;
            else
                image.color = blocked;

        }
    }
    private void Awake()
    {
        game = gameMain.GetComponent<IGameController>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(SetTrueLetter);
        countView.text = "-" + count;
    }
    private void SetTrueLetter()
    {
        game.SetTrueLetter();
        ScoreController.Score.ScoreUpdate(-count);
    }
    private void Start()
    {
        ScoreController.Score.AddItem(this);
    }
    public void UpdateCount(int count)
    {
        if (count < this.count)
        {
            Active = false;
        }
        else
        {
            Active = true;
        }
    }
}
