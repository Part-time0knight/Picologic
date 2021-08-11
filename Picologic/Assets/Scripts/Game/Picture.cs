using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour, ICell, IButton
{
    [SerializeField] private GameObject photo;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject buy;
    [SerializeField] private Image panelZoom;
    [SerializeField] private int cost = 2;
    [SerializeField] private Vector2 zoom = new Vector2(929.2f, 929.2f);
    public bool Active { get { return button.enabled; } set { button.enabled = value; if (zoomIn) ZoomOut(); } }
    private bool setPhoto {
        set {
            if (value == true)
            {
                photo.SetActive(true);
                block.SetActive(false);
                buy.SetActive(false);
            }
        }
    }
    private bool setBlock
    {
        set
        {
            if (value == true)
            {
                photo.SetActive(false);
                block.SetActive(true);
                buy.SetActive(false);
            }
        }
    }
    private bool setBuy
    {
        set
        {
            if (value == true)
            {
                photo.SetActive(false);
                block.SetActive(false);
                buy.SetActive(true);
            }
        }
    }
    private Button button;
    private Image imagePhoto;
    private Image imageBuy;
    private Image imageBlock;
    private Game controller;
    private RectTransform rectPhoto;
    private Button buttonZoom;
    private bool zoomIn = false;
    private Vector2 standart = new Vector2();
    private Vector2 standartPos = new Vector2();
    public void Init(Game controller)
    {
        this.controller = controller;
    }
    private void Awake()
    {
        button = GetComponent<Button>();
        imagePhoto = photo.GetComponent<Image>();
        imageBuy = buy.GetComponent<Image>();
        imageBlock = block.GetComponent<Image>();
        rectPhoto = photo.GetComponent<RectTransform>();
        buttonZoom = panelZoom.GetComponent<Button>();
        standart.x = rectPhoto.rect.width;
        standart.y = rectPhoto.rect.height;
        standartPos = rectPhoto.anchoredPosition;

    }
    private void Start()
    {
        AnimController.Mouse.AddButton(this);
    }
    private void OnDestroy()
    {
        AnimController.Mouse.DeleteButton(this);
    }
    public void SetOpen()
    {
        setPhoto = true;
        button.targetGraphic = imagePhoto;
    }
    public void SetLock()
    {
        setBlock = true;
        button.targetGraphic = imageBlock;
    }
    public void SetBuy()
    {
        setBuy = true;
        button.targetGraphic = imageBuy;
    }
    public void ClickOnPicture()
    {
        if (buy.activeSelf && ScoreController.Score.getScore > cost)
        {
            BuyPicture();
        }
        else if (photo.activeSelf)
        {
            if (!zoomIn)
                ZoomIn();
            else
                ZoomOut();
        }
    }
    private void ZoomIn()
    {
        rectPhoto.SetParent(panelZoom.transform);
        rectPhoto.sizeDelta = zoom;
        rectPhoto.anchoredPosition = Vector2.zero;
        
        panelZoom.raycastTarget = true;
        buttonZoom.onClick.AddListener(ZoomOut);
        zoomIn = true;
    }
    private void ZoomOut()
    {
        rectPhoto.SetParent(this.transform);
        rectPhoto.sizeDelta = standart;
        rectPhoto.anchoredPosition = standartPos;
        panelZoom.raycastTarget = false;
        zoomIn = false;
    }
    private void BuyPicture()
    {
        SetOpen();
        ScoreController.Score.ScoreUpdate(-cost);
        controller.OpenNext();
    }
}
