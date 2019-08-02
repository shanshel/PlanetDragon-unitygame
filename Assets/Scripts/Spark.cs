using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    private float timeStart;
    private float collidSafetime = .5f;
    private MeshCollider meshCollider;
    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponentInChildren<MeshCollider>();
        meshCollider.enabled = false;
        
        timeStart = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGamePaused()) return;

        if (Time.time > timeStart + collidSafetime && meshCollider.enabled == false)
        {
            meshCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.playerInstance.die();
        }
    }
}
