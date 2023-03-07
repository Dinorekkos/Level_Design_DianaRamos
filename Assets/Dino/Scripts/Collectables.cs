using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Vector3 rotation;

    private Vector3 _center;
    MeshRenderer _meshRenderer;

    void Update()
    {
        transform.Rotate(rotation *  Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddPoint();
            Destroy(gameObject);
        }
    }
}
