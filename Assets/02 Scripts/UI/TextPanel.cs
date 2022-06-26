using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;  
    [SerializeField] private float _textSpeed = 0.03f;

    private void Start()
    {
        ShowTextPanel("Çý¿¬¾Æ »ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ»ç¶ûÇØ", "À¯ÇÏÁØ", Color.red);
    }

    public void ShowTextPanel(string text, string name, Color nameColor)
    {
        _messageText.text = "";
        string color = ColorUtility.ToHtmlStringRGB(nameColor);
        Sequence seq = DOTween.Sequence();
        seq.Append(_messageText.DOFade(1f, 0.5f));
        seq.Insert(0.3f,DOTween.To(() => _messageText.text,
                    value => _messageText.text = string.Format("<color=#{0}>{1}</color>:{2}",color, name, value),
                    text, text.Length * _textSpeed)
            );
    }
}
