using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   public FirstPersonController firstPersonController;
   public GameObject UIDead;
   public Vector3 respawnPoint;
   public GameObject player;

   public int point;
   public TMP_Text pointText;
   
   
    void Start()
   {
       UIDead.SetActive(false);
       respawnPoint = player.transform.position;
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
   }

    public void PlayerDie()
    {
        UIDead.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResetPlayer()
    {
        // player.transform.position = respawnPoint;
        // UIDead.SetActive(false);
        // firstPersonController.enabled = true;
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
    
    public void AddPoint()
    {
        point++;
        pointText.text = point.ToString();
    }

}
