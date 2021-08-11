using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void ShowEnd();
public interface IAdd
{
    public void ShowAdd(ShowEnd showEnd);
}
