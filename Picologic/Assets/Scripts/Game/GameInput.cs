using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInput : MonoBehaviour, IInput
{
    private const float TIME = 2f;
    [SerializeField] private GameObject gameObj;

    [SerializeField] private GameObject letterViewPanel;
    [SerializeField] private GameObject letterClickPanel;
    [SerializeField] private LetterView letterViewPrefab;
    [SerializeField] private LetterInput letterClickPrefab;
    [SerializeField] private float sizeMax = 162f;
    [SerializeField] private float sizeMin = 100f;
    [SerializeField] private int startResize = 6;
    [SerializeField] private int MaxSize= 9;
    private IGameController game;
    private string mainString;
    private int letterClickPanelCount;
    private int viewLength;
    private readonly List<LetterView> letterViews = new List<LetterView>();
    private readonly List<LetterInput> letterClicks = new List<LetterInput>();
    private readonly List<int> order = new List<int>();
    private void Awake()
    {
        game = gameObj.GetComponent<IGameController>();
        game.input = this;
    }
    public void Init(string main, int viewLength, int InputCount)
    {
        mainString = main;
        this.viewLength = viewLength; 
        letterClickPanelCount = InputCount;
        ViewInit();
        InputInit();

    }
    //-----создание поля вывода
    private void ViewInit()
    {
        float newSize = ViewSizeGet();
        for (int i = 0; i < viewLength; i++)
        {
            letterViews.Add(Instantiate(letterViewPrefab, letterViewPanel.transform));
            letterViews[letterViews.Count - 1].InitViewItem(this, i);
            RectTransform rect = letterViews[letterViews.Count - 1].GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(newSize, newSize);
        }
    }
    //-----создание поля ввода
    private void InputInit() 
    {
        for (int i = 0; i < letterClickPanelCount; i++)
        {
            LetterInput item = Instantiate(letterClickPrefab, letterClickPanel.transform);
            letterClicks.Add(item);
            letterClicks[i].SetLetter(this, mainString[i]);
        }
    }
    private float ViewSizeGet()
    {
        if (viewLength <= startResize)
            return sizeMax;
        float res = sizeMax - (sizeMax - sizeMin) / (MaxSize - startResize) * (viewLength - startResize);
        return res;
    }
    public void DeleteLetter(char letter, int pos)
    {
        letterViews[pos].ResetLetter();
        LetterInput item = FindLetter(letter, false);
        item.Active = true;
        game.DeleteLetter(pos);
    }
    private LetterInput FindLetter(char letter, bool active)
    {
        for (int i = 0; i < letterClicks.Count; i++)
        {
            if (letterClicks[i].Active == active && letterClicks[i].Letter == letter)
            {
                return letterClicks[i];
            }
        }
        return null;
    }
    public void SetLetter(char letter, int pos, bool solid)
    {
        letterViews[pos].SetLetter(letter);
        if (solid)
            letterViews[pos].Correct = true;
    }
    public void BlockLetter(char letter)
    {
        for (int i = 0; i < letterViews.Count; i++)
        {
            letterViews[i].ResetItem();
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].Active = true;
        }
        LetterInput item = FindLetter(letter, true);
        item.LetterBlock();
    }
    public void SetTrueLetter(char letter, int pos)
    {
        for (int i = 0; i < letterViews.Count; i++)
        {
            letterViews[i].ResetItem();
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].Active = true;
        }
        SetLetter(letter, pos, true);
        LetterInput item = FindLetter(letter, true);
        item.LetterBlock();
    }
    public void EnterLetter( char letter )
    {
        game.EnterLetter(letter);
    }

    public void InputRestart()
    {
        for (int i = 0; i < letterViews.Count; i++)
        {
            letterViews[i].ResetItem();
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].Active = true;
        }
    }
    public void InputWin()
    {
        AnimController.Mouse.MouseActive = false;
        for (int i = 0; i < letterViews.Count; i++)
        {
            letterViews[i].Correct = true;
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].LetterUnBlock();
            letterClicks[i].Active = false;
        }
        StartCoroutine(EndWin());
    }
    public void InputLose()
    {
        AnimController.Mouse.MouseActive = false;
        for (int i = 0; i < letterViews.Count; i++)
        {
            if (!letterViews[i].Correct)
                letterViews[i].Correct = false;
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].Active = false;
        }
        StartCoroutine(EndLose());
    }
    private IEnumerator EndWin()
    {
        yield return new WaitForSeconds(TIME);
        for (int i = 0; i < letterViews.Count; i++)
        {
            letterViews[i].ResetLetter();
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].Active = true;
        }
        AnimController.Mouse.MouseActive = true;
        SceneController.sceneController.WinGame();
    }
    private IEnumerator EndLose()
    {
        yield return new WaitForSeconds(TIME);
        for (int i = 0; i < letterViews.Count; i++)
        {
            letterViews[i].ResetItem();
        }
        for (int i = 0; i < letterClicks.Count; i++)
        {
            letterClicks[i].Active = true;
        }
        AnimController.Mouse.MouseActive = true;
    }
}
