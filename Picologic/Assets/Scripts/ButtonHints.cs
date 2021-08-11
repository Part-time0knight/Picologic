using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHints : MonoBehaviour, IButton
{
    public bool Active { get { return button.enabled; } set { button.enabled = value; } }
    [SerializeField] private GameObject hintsPanel;
    [SerializeField] private bool show;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowHintsPanel);
    }

    private void ShowHintsPanel()
    {
        hintsPanel.SetActive(show);
    }
}
