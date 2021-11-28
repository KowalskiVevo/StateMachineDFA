using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttackBox : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,0.11f);
    }
}
