using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public bool IsEnemy { get; }
    public Vector3 HitPoint { get; set; }
    public void GetHit(int damage, GameObject damagerDealer);
}
