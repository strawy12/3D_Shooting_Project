using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private float _showDuration = 1f;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowUI(string text)
    {
        _interactionText.text = text;
        _canvasGroup.DOFade(1f, _showDuration);
    }

    public void UnShowUI()
    {
        _canvasGroup.DOFade(0f, _showDuration);
    }
}
