using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private List<HpField> _hpFieldList;

    public void SetHpBar(int hp, int maxHp, int shieldHp)
    {
        hp = Mathf.Clamp(hp, 0, 200);
        shieldHp = Mathf.Clamp(shieldHp, 0, 200);

        bool useShield = (shieldHp > 0f);
        string hpColorTag = (!useShield) ? "000000" : "00FFE7";
        _hpText.text = $"<color=#{hpColorTag}><size={(useShield ? 24 : 20)}>{hp + shieldHp}</color></size> / {maxHp}";

        SetHpField(hp, maxHp);
        SetSieldField(shieldHp, maxHp);
    }

    private void SetHpField(int hp, int maxHp)
    {
        int cnt = 0;

        if (hp > 0f)
        {
            int fieldMaxHp = maxHp / _hpFieldList.Count;
            while (hp >= fieldMaxHp)
            {
                _hpFieldList[cnt++].SetFillAmount(1f, false);

                hp -= fieldMaxHp;
            }

            if (hp > 0f)
            {
                float amount = (float)hp / fieldMaxHp; 
                _hpFieldList[cnt++].SetFillAmount(amount, false);

            }
        }
        

        for (int i = cnt; i < _hpFieldList.Count; i++)
        {
            _hpFieldList[i].SetFillAmount(0f, false);
        }
    }

    private void SetSieldField(int shieldHp, int maxHp)
    {
        int cnt = 0;

        if (shieldHp != 0f)
        {
            int fieldMaxHp = maxHp / _hpFieldList.Count;

            while (shieldHp >= fieldMaxHp)
            {
                _hpFieldList[cnt++].SetFillAmount(1f, true);

                shieldHp -= fieldMaxHp;
            }


            if (shieldHp > 0f)
            {
                float amount = (float)shieldHp / fieldMaxHp;
                _hpFieldList[cnt++].SetFillAmount(amount, true);
            }
        }
        

        for(int i = cnt; i < _hpFieldList.Count; i++)
        {
            _hpFieldList[i].SetFillAmount(0f, true);
        }
    }
}

