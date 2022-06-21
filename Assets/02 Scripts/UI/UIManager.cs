using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _range;
    [SerializeField] private float _maxDDPos;
    [SerializeField] private float _jumpPower;

    private void Start()
    {
        StartCoroutine(Co());
    }
    private IEnumerator Co()
    {
        while(true)
        {
            GenerateDamagePopup(Random.Range(0, 1000), true);

            yield return new WaitForSeconds(2f);
        }
    }
    public void GenerateDamagePopup(int damage, bool isCritical)
    {
        _text.text = damage.ToString();
        _text.DOFade(0f, 0f);
        _text.transform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(_text.DOFade(1f, 1f));
        seq.Join(_text.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.InOutBounce));

        float xPos = Random.Range(-1f, 1f) * Random.Range(0.1f, _range);
        float yPos = Random.Range(1f, _maxDDPos);

        seq.Join(_text.transform.DOJump(new Vector2(xPos, yPos), _jumpPower, 1,0.5f).SetDelay(0.2f));
    }
}
