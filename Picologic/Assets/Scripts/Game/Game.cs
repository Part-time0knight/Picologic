using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour, IGameController
{
    [SerializeField] GameObject[] CellItems;
    [SerializeField] private string winWord;
    [SerializeField] private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    [SerializeField] private int InputCount = 14;
    private ResetButton FalseLetters;
    private string charSet = "";
    private string charSetFalse = "";
    private bool[] opened;
    private readonly List<int> freeIndex = new List<int>();
    private ICell[] items;
    private int stage = 0;
    private char[] enterWord;
    private IInput _input;
    public IInput input
    { 
        set 
        {
            if (_input == null)
            {
                _input = value;
                GenerateCharSet();
                _input.Init(charSet, winWord.Length, InputCount);
            }
        } 
    }

    private void Awake()
    {
        items = new ICell[CellItems.Length];

        for (int i = 0; i < CellItems.Length; i++)
        {
            items[i] = CellItems[i].GetComponent<ICell>();
            items[i].Init(this);
        }
        enterWord = new char[winWord.Length];
        opened = new bool[enterWord.Length];
        for (int i = 0; i < enterWord.Length; i++)
        {
            enterWord[i] = ' ';
            opened[i] = false;
            freeIndex.Add(i);
        }
        
        Load();

    }
    private void Start()
    {
        GameUpdate();
    }
    private void GenerateCharSet()
    {
        List<int> order = new List<int>();
        List<char> charSet = new List<char>();
        string res = "";
        for (int i = 0; i < InputCount; i++)
            order.Add(i);
        for (int i = 0; i < winWord.Length; i++)
            charSet.Add(winWord[i]);
        for (int i = charSet.Count; i < InputCount; i++)
        {
            char letter = alphabet[Random.Range(0, alphabet.Length)];
            charSet.Add(letter);
            charSetFalse += letter;
        }
        for (int i = 0; i < InputCount; i++)
        {
            int index = Random.Range(0, order.Count);
            char item = charSet[order[index]];
            res += item;
            order.RemoveAt(index);
        }
        this.charSet = res;
    }
    private void GameUpdate()
    {
        for (int i = 0; i <= stage; i++)
            items[i].SetOpen();
        for (int i = stage + 2; i < items.Length; i++)
            items[i].SetLock();
        if (stage + 1 < items.Length)
            items[stage + 1].SetBuy();
    }
    private bool WinCheck() 
    {
        for (int i = 0; i < enterWord.Length; i++)
            if (enterWord[i] != winWord[i])
                return false;
        return true;
    }
    private void Win()
    {
        for (int i = 0; i < enterWord.Length; i++)
        {
            enterWord[i] = ' ';
            opened[i] = false;
            freeIndex.Add(i);
        }
        if (FalseLetters != null)
        {
            FalseLetters();
            FalseLetters = null;
        }
        _input.InputWin();
        stage = 0;
        GameUpdate();
    }
    public void OpenNext()
    {
        stage++;
        GameUpdate();
    }
    public void DeleteLetter(int pos)
    {
        enterWord[pos] = ' ';
    }
    private int FreePos()
    {
        int index = -1;
        for (int i = 0; i < enterWord.Length; i++)
        {
            if (enterWord[i] == ' ')
                return i;
        }
        return index;
    }
    private void Lose()
    {
        for (int i = 0; i < enterWord.Length; i++)
        {
            if (!opened[i])
                enterWord[i] = ' ';
        }
        _input.InputLose();
    }
    public void EnterLetter (char letter)
    {
        int index = FreePos();
        enterWord[index] = letter;
        _input.SetLetter(letter, index, false);
        if (WinCheck())
        {
            Win();
        }
        else if (FreePos() < 0)
        {
            Lose();
        }
    }
    public void Restart()
    {
        for (int i = 0; i < enterWord.Length; i++)
        {
            if (!opened[i])
                enterWord[i] = ' ';
        }
        _input.InputRestart();
    }
    public void BlockFalseLetters(ResetButton FalseLetters)
    {
        for (int i = 0; i < charSetFalse.Length; i++)
        {
            char letter = charSetFalse[i];
            _input.BlockLetter(letter);
        }
        this.FalseLetters = FalseLetters;
    }
    public void SetTrueLetter()
    {
        Debug.Log(freeIndex.Count);
        int index = freeIndex[Random.Range(0, freeIndex.Count)];
        freeIndex.Remove(index);
        char letter = winWord[index];
        enterWord[index] = letter;
        opened[index] = true;
 

        _input.SetTrueLetter(letter, index);
        for (int i = 0; i < enterWord.Length; i++)
        {
            if (!opened[i])
                enterWord[i] = ' ';
        }
        if (WinCheck())
        {
            Win();
        }
    }
    public void Load()
    {
        stage = 0;
    }
}

