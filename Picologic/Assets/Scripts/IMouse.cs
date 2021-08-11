using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouse
{
    public bool MouseActive { get; set; }
    public void AddButton(IButton button);
    public void DeleteButton(IButton button);
}
