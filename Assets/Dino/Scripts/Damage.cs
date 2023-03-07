using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float height;
    public bool canMove;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, height), transform.position.z);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has been hit" + other.gameObject.name);
            if (FirstPersonController.Instance.isAlive)
            {
                FirstPersonController.Instance.isAlive = false;
                other.gameObject.GetComponent<Player>().PlayerDie();
            }
        }
    }
}
