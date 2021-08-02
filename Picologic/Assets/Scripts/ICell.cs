using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    public void SetOpen();
    public void SetLock();
    public void SetBuy();
}
