using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class TutorialTextAudio
{
    public AudioClip audio;
    public string text;
    public bool isStop;
    public UnityEvent action;
    
}

public class CutSceneManager : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private AudioSource _audioSource;

    #region Tutorial_Scene
    [Header("Tutorial_Scene")]
    [SerializeField] private CoolTimePanel _dashPanel;
    [SerializeField] private CoolTimePanel _trapPanel;
    [SerializeField] private CoolTimePanel _hoshiTanPanel;
    [SerializeField] private GameObject _tutorialEnemy;
    [SerializeField] private Transform _tutorialTrs;
    [SerializeField] private List<TutorialTextAudio> _tutorialTextAudios;

    public void TutorialStart()
    {
        StartCoroutine(TutorialLoop());
    }

    private IEnumerator TutorialLoop()
    {
        foreach(var tutorial in _tutorialTextAudios)
        {
            _audioSource.Stop();
            _audioSource.clip = tutorial.audio;
            _audioSource.Play();
            _textPanel.ShowTextPanel(tutorial.text, "¡ÿ»Ò", Color.green);

            yield return new WaitForSeconds(tutorial.audio.length + 0.5f);

            tutorial.action?.Invoke();
            if (tutorial.isStop) yield break;

        }
    }

    public void SpawnEnemy()
    {
        float x = Random.Range(-3f, 3f);
        float z = Random.Range(-3f, 3f);
        Instantiate(_tutorialEnemy, _tutorialTrs.position + new Vector3(x, 0f, z), Quaternion.identity);
    }
    #endregion
}
