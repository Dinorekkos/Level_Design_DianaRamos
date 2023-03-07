using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Vector3 rotation;

    private Vector3 _center;
    MeshRenderer _meshRenderer;
    private bool isMoving = false;
    private float _distance = 0.5f;
    

    void Update()
    {
        transform.Rotate(rotation *  Time.deltaTime);

        if (isMoving)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time, _distance), transform.position.y, transform.position.z);

        }
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
