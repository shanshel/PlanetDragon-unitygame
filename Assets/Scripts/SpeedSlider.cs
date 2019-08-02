using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    public static SpeedSlider instance;
    public Image iconImage;

    private void Awake()
    {
        instance = this;
    }
}
