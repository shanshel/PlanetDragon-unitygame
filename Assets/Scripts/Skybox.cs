using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public float rotateSpeed = 0.01f;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGamePaused()) return;
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
