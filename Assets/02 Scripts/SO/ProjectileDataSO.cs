using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/ProjectileData")]
public class ProjectileDataSO : ScriptableObject
{
    [Range(0, 10)] public int damage = 1;
    [Range(0, 100f)] public float bulletSpeed = 1;

    [Range(0, 5f)] public float explosionRadius = 3f; 

    public float friction = 0f;
    public bool bounce = false; 
    public bool goTroughtHit = false; 
    public bool isRayCast = false;
    public bool isCharing = false;

    [Range(1, 20)] public float knockBackPower = 5f;
    [Range(0.01f, 1f)] public float knockBackDelay = 0.1f;

    public GameObject impactObstanclePrefab;

    public float lifeTime = 2f;
}
