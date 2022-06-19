using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryBookShelf : MonoBehaviour
{
    [SerializeField] private List<BoxCollider> _bookTempList;
    [SerializeField] private List<Transform> _booksPos;
    [SerializeField] private int _groupGenerateCnt = 12;

    static int cnt = 1;

    private void Start()
    {
        gameObject.name = $"BookShelf_{cnt++}";
        for(int i = 0; i < _booksPos.Count; i++)
        {
            for(int j = 0; j < _groupGenerateCnt; j++)
            {
                int idx = Random.Range(0, _bookTempList.Count);
                Collider book = Instantiate(_bookTempList[idx], transform);
                book.transform.position = _booksPos[i].position + Vector3.back * 0.08f * j;
                book.gameObject.SetActive(true);
                book.gameObject.name = book.gameObject.name.Replace("(Clone)", "");
                Destroy(book);
            }
        }

        Destroy(_bookTempList[0].transform.parent.gameObject);
        Destroy(_booksPos[0].transform.parent.gameObject);
    }

}
