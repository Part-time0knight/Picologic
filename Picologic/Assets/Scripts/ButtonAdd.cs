using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAdd : MonoBehaviour, IButton
{
    public bool Active
    {
        get { return button.enabled; }
        set
        {
            button.enabled = value;
            if (value)
                image.color = active;
            else
                image.color = blocked;

        }
    }
    [SerializeField] private int count = 25;
    [SerializeField] private GameObject addObject;
    [SerializeField] private Text countView;
    [SerializeField] private Color active = Color.white;
    [SerializeField] private Color blocked = new Color(0.75f, 0.75f, 075f, 0.5f);
    private IAdd add;
    private Button button;
    private Image image;
    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        add = addObject.GetComponent<IAdd>();
        button.onClick.AddListener(ShowAdd);
        countView.text = "+" + count;
    }
    private void ShowAdd()
    {
        add.ShowAdd(ShowEnd);
    }
    private void ShowEnd()
    {
        ScoreController.Score.ScoreUpdate(count);
    }
}
