using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public static PlayerHead headInstante;

    private void Awake()
    {
        headInstante = this;
    }
 
}
