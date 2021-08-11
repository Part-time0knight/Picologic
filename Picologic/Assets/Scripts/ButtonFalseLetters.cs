using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFalseLetters : MonoBehaviour, IButton, IScore
{
    public bool Active
    {
        get { return button.enabled; }
        set
        {
            if (!forsedBlock)
            {
                button.enabled = value;
                if (value)
                    image.color = active;
                else
                    image.color = blocked;
            }
        }
    }
    private bool forsedBlock = false;
    [SerializeField] private GameObject gameMain;
    [SerializeField] private int count = 50;
    [SerializeField] private Text countView;
    [SerializeField] private Color active = Color.white;
    [SerializeField] private Color blocked = new Color(0.75f, 0.75f, 075f, 0.5f);
    private IGameController game;
    private Button button;
    private Image image;
    private int mainCount;
    private void Awake()
    {
        game = gameMain.GetComponent<IGameController>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(KillFalseLetter);
        
        countView.text = "-" + count;
    }
    private void Start()
    {
        ScoreController.Score.AddItem(this);
    }
    private void OnDestroy()
    {
        ScoreController.Score.RemoveItem(this);
    }
    private void KillFalseLetter()
    {
        game.BlockFalseLetters(UnBlock);
        ScoreController.Score.ScoreUpdate(-count);
        Active = false;
        forsedBlock = true;
    }
    private void UnBlock()
    {
        forsedBlock = false;
        Active = true;
        UpdateCount(mainCount);
    }
    public void UpdateCount(int count)
    {
        mainCount = count;
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
