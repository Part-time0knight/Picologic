using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInput : MonoBehaviour, IInput
{
    [SerializeField] private Game game;
    [SerializeField] private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    [SerializeField] private int letterClickPanelColumns = 7;
    [SerializeField] private int letterClickPanelRows = 2;
    [SerializeField] private GameObject letterViewPanel;
    [SerializeField] private GameObject letterClickPanel;
    [SerializeField] private LetterView letterViewPrefab;
    [SerializeField] private LetterInput letterClickPrefab;
    [SerializeField] private float sizeMax = 162f;
    [SerializeField] private float sizeMin = 100f;
    [SerializeField] private int startResize = 6;
    [SerializeField] private int MaxSize= 9;
    private string enterWord = "";
    private string mainWord;
    private string letterViewPrefabsString;
    private int letterClickPanelCount;
    private readonly List<LetterView> letterViews = new List<LetterView>();
    private readonly List<LetterInput> letterClicks = new List<LetterInput>();
    private readonly List<char> alphabetList = new List<char>();
    private readonly List<int> order = new List<int>();
    public void SetWord( string word )
    {
        mainWord = word;
    }
    private void Awake()
    {
        game.input = this;
        float newSize = Step();
        letterClickPanelCount = letterClickPanelColumns * letterClickPanelRows;
        for (int i = 0; i < mainWord.Length; i++)
        {
            letterViews.Add(Instantiate(letterViewPrefab, letterViewPanel.transform));
            RectTransform rect = letterViews[letterViews.Count - 1].GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(newSize, newSize);
        }
        List<int> temp = new List<int>();

        //-----порядок букв в поле ввода
        for (int i = 0; i < letterClickPanelCount; i++)
        {
            temp.Add(i);
        }
        for (int i = 0; i < letterClickPanelCount; i++)
        {
            int index = Random.Range(0, temp.Count);
            int item = temp[index];
            temp.RemoveAt(index);
            order.Add(item);
        }
        for (int i = 0; i < mainWord.Length; i++)
        {
            alphabetList.Add(mainWord[i]);
        }
        for (int i = alphabetList.Count - 1; i < letterClickPanelCount; i++)
        {
            alphabetList.Add(alphabet[Random.Range(0, alphabet.Length)]);
        }
        //-----Создание поля ввода
        for (int i = 0; i < letterClickPanelCount; i++)
        {
            LetterInput item = Instantiate(letterClickPrefab, letterClickPanel.transform);
            letterClicks.Add(item);
            item.SetLetter(alphabetList[order[i]], this);
        }
    }
    private float Step()
    {
        if (mainWord.Length <= startResize)
            return sizeMax;
        float res = sizeMax - (sizeMax - sizeMin) / (MaxSize - startResize) * (mainWord.Length - startResize);
        return res;
    }

    public bool EnterLetter( char letter )
    {
        enterWord += letter;
        if (enterWord.Length <= mainWord.Length)
        {

            letterViews[enterWord.Length - 1].SetLetter("" + letter);
        }
        else if (WinCheck())
            Win();
        else
            Restart();
        return true;

    }
    private bool WinCheck()
    {
        return false;
    }
    private void Win()
    {
    }
    private void Restart()
    {
    }
}
