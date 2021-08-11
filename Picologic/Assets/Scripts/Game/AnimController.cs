using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour, IMouse
{
    public static IMouse Mouse { get { return mouse; } }
    public bool MouseActive { get { return buttonActive; } set { if (value) ActivateButtons(); else BlockButtons(); } }
    private bool buttonActive = true;
    private static IMouse mouse;
    private readonly List<IButton> buttons = new List<IButton>();
    private void Awake()
    {
        if (mouse == null)
            mouse = this;

    }
    public void AddButton(IButton button)
    {
        buttons.Add(button);
    }
    public void DeleteButton(IButton button)
    {
        buttons.Remove(button);
    }
    private void ActivateButtons()
    {
        buttonActive = true;
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Active = true;
        }
    }
    private void BlockButtons()
    {
        buttonActive = false;
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Active = false;
        }
    }

}
