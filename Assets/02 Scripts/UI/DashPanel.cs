using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DashPanel : CoolTimePanel
{
    [SerializeField] private TMP_Text _countText;

    public void SetCountText(int cnt)
    {
        _countText.text = cnt.ToString();
    }
}
