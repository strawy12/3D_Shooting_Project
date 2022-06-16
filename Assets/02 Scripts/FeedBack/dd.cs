using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider[] cos = transform.GetComponentsInChildren<Collider>();

        foreach(var col in cos)
        {
            Destroy(col);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
