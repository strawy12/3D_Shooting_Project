using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Title : MonoBehaviour
{
    [SerializeField] private Transform _quitPanel;

    private bool _panelOpen;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ActivePanel();
        }
    }

    public void ActivePanel()
    {
        _panelOpen = !_panelOpen;
        _quitPanel.DOScale(_panelOpen ? Vector3.one : Vector3.zero, 0.5f).SetEase(Ease.InOutBounce);
    }
    public void StartGame()
    {
        if (_panelOpen) return;

        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
