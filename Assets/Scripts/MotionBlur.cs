using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MotionBlut : MonoBehaviour
{
    public float maxBlurSpeed;
    private float blurRatio;
    PlayerMovementManagerBeta MovementManager;
    PostProcessVolume MotionBlur;

    // Start is called before the first frame update
    void Start()
    {
        MovementManager = GameObject.Find("Player").GetComponent<PlayerMovementManagerBeta>();
        MotionBlur = GameObject.Find("Motion Blur").GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRatio();
        UpdateFov();
        UpdateBlur();
    }

    void UpdateRatio()
    {
        blurRatio = Mathf.Abs(MovementManager.GetVelocityFoward() / maxBlurSpeed);
        Debug.Log(blurRatio);
        blurRatio = Mathf.Min(1f, blurRatio);
    }

    void UpdateFov()
    {
        float targetFov = 90 + (45 * blurRatio);
        float currentFov = GetComponent<Camera>().fieldOfView;

        if(targetFov > currentFov + 1)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Min(135, currentFov + 360 * Time.deltaTime);
        }
        else if(targetFov < currentFov - 1)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Max(90, currentFov - 360 * Time.deltaTime);
        }
    }

    void UpdateBlur()
    {
        MotionBlur.weight = blurRatio;
    }
}
