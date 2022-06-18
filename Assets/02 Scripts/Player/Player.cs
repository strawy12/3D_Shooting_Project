using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    public bool IsEnemy { get; set; }

    public Vector3 HitPoint { get; set; }
    public int HitCount { get; set; }

    public void GetHit(int damage, GameObject damagerDealer)
    {
    }

}
