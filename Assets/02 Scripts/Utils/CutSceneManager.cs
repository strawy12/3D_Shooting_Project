using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutSceneManager : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private TextPanel _textPanel;

    #region 1_Scene
    [Header("1. Scene")]
    [SerializeField] private List<GameObject> _unActiveObjList;
    #endregion
}
