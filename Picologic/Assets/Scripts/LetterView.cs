using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterView : MonoBehaviour
{
    [SerializeField] private Text letterUI;
    [SerializeField] private Image trueBack;
    [SerializeField] private Image enterBack;
    [SerializeField] private Image falseBack;
    private string letter = "";
    private Image image;
    private Sprite empty;
    private void Awake()
    {
        letterUI.text = letter;
        image = GetComponent<Image>();
        empty = image.sprite;
    }

    public void SetLetter(string letter)
    {
        this.letter = letter;
        letterUI.text = letter;
        image.sprite = enterBack.sprite;
    }
    public void SetCorrect(bool correct)
    {
        if (correct)
            enterBack.sprite = trueBack.sprite;
        else
            enterBack.sprite = falseBack.sprite;
    }
    
}
