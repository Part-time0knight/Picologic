using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LetterInput : MonoBehaviour, IButton
{
    public char Letter { get { return letter; } }
    [SerializeField] private Text text;
    [SerializeField] private Color active = new Color(1f, 1f, 1f);
    [SerializeField] private Color blocked = new Color(0.27f, 0.25f, 0.34f);
    public bool Active { get { return button.enabled; } set { if (!_blocked) SetActive(value); } }
    private bool _blocked = false;
    private char letter;
    private Button button;
    private IInput input;

    private void Awake()
    {
        text.color = active;
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        AnimController.Mouse.AddButton(this);
    }
    private void OnDestroy()
    {
        AnimController.Mouse.DeleteButton(this);
    }
    public void SetLetter(IInput input, char letter)
    {
        this.letter = letter;
        text.text = "" + letter;
        if (this.input == null)
            this.input = input;
    }
    private void Click()
    {
        input.EnterLetter(letter);
        text.color = blocked;
        Active = false;
    }
    public void LetterBlock()
    {
        _blocked = true;
        SetActive(false);
    }
    public void LetterUnBlock()
    {
        _blocked = false;
        SetActive(true);
    }
    private void SetActive(bool active)
    {
        if (!active)
        {
            text.color = blocked;
            button.enabled = false;
        }
        else
        {
            text.color = this.active;
            button.enabled = true;
        }
    }
}
