using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpField : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _fillShieldImage;

    private void Awake()
    {
        if(_fillImage == null)
        {
            _fillImage =  transform.Find("Fill").GetComponent<Image>();
        }

        if(_fillShieldImage == null)
        {
            _fillShieldImage = transform.Find("ShieldFill").GetComponent<Image>();
        }
    }

    public void SetFillAmount(float amount, bool isShield)
    {
        if(isShield)
        {
            _fillShieldImage.fillAmount = amount;
        }

        else
        {
            _fillImage.fillAmount = amount;
        }
    }

}
