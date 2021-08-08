using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject[] CellItems;
    [SerializeField] private string winWord;
    private ICell[] items;
    private int stage;
    private string enterWord;
    private IInput _input;
    public IInput input
    { 
        set 
        {
            if (_input == null)
            {
                _input = value;
                _input.SetWord(winWord);
            }
        } 
    }

    private void Awake()
    {
        Load();
        items = new ICell[CellItems.Length];
        for (int i = 0; i < CellItems.Length; i++)
        {
            items[i] = CellItems[i].GetComponent<ICell>();
            items[i].Init(this);
        }
        GameUpdate();
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
        return false;
    }
    private void Win()
    {

    }
    public void OpenNext()
    {
        stage++;
        GameUpdate();
    }
    public void EnterWord (string word)
    {
        enterWord = word;
        if (WinCheck())
            Win();
    }
    public void Load()
    {
        stage = 0;
        enterWord = "";
    }
}

