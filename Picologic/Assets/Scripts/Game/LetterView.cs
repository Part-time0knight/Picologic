using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterView : MonoBehaviour, IButton
{
    public bool Active { get { return button.enabled; } set { button.enabled = value; } }
    public bool Correct { get { return image.sprite == trueBack.sprite; } set { SetCorrect(value); } }
    [SerializeField] private Text letterUI;
    [SerializeField] private Image trueBack;
    [SerializeField] private Image enterBack;
    [SerializeField] private Image falseBack;
    private char letter = ' ';
    private Button button;
    private Image image;
    private Sprite empty;
    private Color emptyColor;
    private IInput input;
    private int index;
    private void Awake()
    {
        letterUI.text = "" + letter;
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        empty = image.sprite;
        emptyColor = Color.white;
        button.onClick.AddListener(ClickLetter);
        button.enabled = false;
    }
    private void Start()
    {
        AnimController.Mouse.AddButton(this);
    }
    private void OnDestroy()
    {
        AnimController.Mouse.DeleteButton(this);
    }
    public void InitViewItem(IInput input, int index, char letter = ' ')
    {
        this.input = input;
        this.index = index;
        this.letter = letter;
        letterUI.text = "" + letter;
    }
    private void ClickLetter()
    {
        input.DeleteLetter(letter, index);
    }
    public void ResetLetter()
    {
        letterUI.text = "";
        image.sprite = empty;
        image.color = emptyColor;
        button.enabled = false;
    }
    public void ResetItem()
    {
        if (image.sprite != trueBack.sprite)
            ResetLetter();
    }
    public void SetLetter(char letter)
    {
        this.letter = letter;
        letterUI.text = "" + letter;
        image.sprite = enterBack.sprite;
        button.enabled = true;
    }
    private void SetCorrect(bool correct)
    {
        button.enabled = false;
        if (correct)
        {
            image.sprite = trueBack.sprite;
            image.color = trueBack.color;
        }
        else
        {
            image.sprite = falseBack.sprite;
            image.color = falseBack.color;
        }
    }
    
}
