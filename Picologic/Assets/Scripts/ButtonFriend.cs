using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFriend : MonoBehaviour, IButton
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
    [SerializeField] private int count = 0;
    [SerializeField] private Text countView;
    [SerializeField] private Color active = Color.white;
    [SerializeField] private Color blocked = new Color(0.75f, 0.75f, 075f, 0.5f);
    private Button button;
    private Image image;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(Share);
        countView.text = "" + count;
    }
    private void Share()
    {

    }
}
