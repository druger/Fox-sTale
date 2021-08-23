using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {
    [SerializeField] private float lifeTime = 0.5f;

    void Update() {
        Destroy(gameObject, lifeTime);
    }
}