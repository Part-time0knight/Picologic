using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReset : MonoBehaviour, IButton
{
    public bool Active { get { return button.enabled; } set { button.enabled = value; } }
    [SerializeField] private GameObject gameMain;
    private IGameController game;
    private Button button;
    private void Awake()
    {
        game = gameMain.GetComponent<IGameController>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Restart);
    }
    private void Restart()
    {
        game.Restart();
    }
}
