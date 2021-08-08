using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LetterInput : MonoBehaviour
{

    [SerializeField] private Text text;
    [SerializeField] private Color active = new Color(1f, 1f, 1f);
    [SerializeField] private Color blocked = new Color(0.27f, 0.25f, 0.34f);
    private char letter;
    private Button button;
    private GameInput input;

    private void Awake()
    {
        text.color = active;
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }
    public void SetLetter( char letter, GameInput input)
    {
        this.letter = letter;
        text.text = "" + letter;
        if (!this.input)
            this.input = input;
    }
    public void Click()
    {
        input.EnterLetter(letter);
        text.color = blocked;
        button.enabled = false;
    }
    public void Block()
    {

    }
}
