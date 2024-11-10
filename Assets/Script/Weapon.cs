using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireRate = 1f; // Time between shots
    protected float nextFireTime = 0f;

    public abstract void Fire();
}
