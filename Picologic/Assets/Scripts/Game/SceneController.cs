using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, IScene
{
    public static IScene sceneController { get { return _sceneController; } }
    private static IScene _sceneController;
    private void Awake()
    {
        if (_sceneController == null)
            _sceneController = this;

    }
    public void WinGame()
    {
        ScoreController.Score.ScoreUpdate(+50);
    }
}
