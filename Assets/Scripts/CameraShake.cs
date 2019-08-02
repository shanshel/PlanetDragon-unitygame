using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public Transform camTransform;

    // How long the object should shake for.
    private float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.01f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        instance = this;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {

 
        if (GameManager.instance.gameTimeScale == 0) return;

    
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= (Time.deltaTime * GameManager.instance.gameTimeScale) * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void shakeCamera(float duration = .1f, float amount = 0.01f)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }


}