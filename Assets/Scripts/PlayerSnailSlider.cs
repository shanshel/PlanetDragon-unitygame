using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSnailSlider : MonoBehaviour
{
    public Image NameBackground, SnailImage;
    public Text PlayerName;
    public Slider localSlider;
 
    private Color defaultColor;
    
     
    public void setSnailAsDisconnected()
    {
        ColorBlock cb = localSlider.colors;
        cb.normalColor = new Color(0.16023f, 0.1886792f, 0.1575293f);
    }

    public void setSnailAsDead()
    {
        SnailImage.color = new Color(.18f, .18f, .18f);
    }

    public void setMyPlayerDefaultColor()
    {
        defaultColor = new Color(0.1745283f, 1f, 0.2479036f);
        ColorBlock cb = localSlider.colors;
        cb.normalColor = defaultColor;
    }

    public void backToDefaultColor()
    {
        SnailImage.color = new Color(1f, 1f, 1f);
    }

}
