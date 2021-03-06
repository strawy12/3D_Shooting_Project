using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public bool IsEnemy { get; }
    public bool CanAttack { get; }
    public Vector3 HitPoint { get; set; }
    public int HitCount { get; set; }
    public void GetHit(int damage, GameObject damagerDealer);

    public void GetHoshiTanEffect(float duration);
}
