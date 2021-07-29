using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Item[] Items;
}
[System.Serializable]
public struct Item
{
    public GameObject itemObject;
    public Image blockBackGround;
}
