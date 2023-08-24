using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
