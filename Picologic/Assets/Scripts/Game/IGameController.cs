using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void ResetButton();
public interface IGameController
{
    public IInput input { set; }
    public void EnterLetter(char letter);
    public void DeleteLetter(int index);
    public void BlockFalseLetters(ResetButton falseLetters);
    public void SetTrueLetter();
    public void Restart();
}
