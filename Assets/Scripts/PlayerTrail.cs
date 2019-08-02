using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{

    void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.instance.isGamePaused()) return;

        transform.position = Vector3.MoveTowards(transform.position, Player.playerInstance.transform.position, .03f * 150f);
    }

  
}
