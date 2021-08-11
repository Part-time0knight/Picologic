using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInput
{
    public void Init(string main, int viewLength, int inputLength);
    public void DeleteLetter(char letter, int pos);
    public void EnterLetter(char letter);
    public void SetLetter(char letter, int pos, bool solid);
    public void BlockLetter(char letter);
    public void SetTrueLetter(char letter, int pos);
    public void InputWin();
    public void InputReset();
}
