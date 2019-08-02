using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public float minIntensity, maxIntensity;
    private float currentMinIn, currentMaxIn;
    public float speed = 10f;
    private Light light;

    private float t;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        currentMinIn = minIntensity;
        currentMaxIn = maxIntensity;
        t = 0.1f;
    }
   
    // Update is called once per frame
    void Update()
    {

        // animate the position of the game object...
        light.intensity = Mathf.Lerp(currentMinIn, currentMaxIn, t);
        t += 0.5f * Time.deltaTime * speed;
 


        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = currentMaxIn;
            currentMaxIn = currentMinIn;
            currentMinIn = temp;
            t = 0.1f;
        }





    }
}
