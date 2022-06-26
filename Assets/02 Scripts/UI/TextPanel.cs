using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;  
    [SerializeField] private TMP_Text _nameText;  
    [SerializeField] private CanvasGroup _canvasGroup;  
    [SerializeField] private float _textSpeed = 0.03f;

    private void Start()
    {
        ShowTextPanel("Çý¿¬¾Æ »ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ", "À¯ÇÏÁØ");
    }

    public void ShowTextPanel(string text, string name)
    {
        Sequence seq = DOTween.Sequence();
        _nameText.text = name;
        seq.Append(_canvasGroup.DOFade(1f, 0.5f));
        seq.Insert(0.3f,DOTween.To(() => _messageText.text,
                    value => _messageText.text = value,
                    text, text.Length * _textSpeed)
            );
    }
}
