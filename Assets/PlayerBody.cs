using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private float timeStart;
    private float collidSafetime = .5f;
    private BoxCollider boxCollider;
    public static float dieTimer = 1.3f;
    void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        boxCollider.enabled = false;
        timeStart = Time.time;
       
    
        Destroy(this.gameObject, dieTimer);
      
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGamePaused()) return;
        if (Time.time > timeStart + collidSafetime && boxCollider.enabled == false)
        {
            boxCollider.enabled = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Time.time > timeStart + collidSafetime)
        {
            Player.playerInstance.die();
        }
        
      
    }

  
}
