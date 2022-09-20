using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _distanceText;
    private CanvasGroup _currentGroup;

    public void Init()
    {
        if (_currentGroup == null)
            _currentGroup = GetComponent<CanvasGroup>();
    }

    public void ActiveUI(bool active, float distance = 0f)
    {


        if (active == true)
        {

                _distanceText.text = $"{(int)distance}m";
            gameObject.SetActive(true);
            if (_currentGroup.alpha == 0f)
                _currentGroup.DOFade(1f, 0.5f);
        }

        else
        {
            if (gameObject.activeSelf)
                _currentGroup.DOFade(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
