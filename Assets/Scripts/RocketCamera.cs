using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCamera : MonoBehaviour
{
    private Camera rocketCamera; 
    private Camera mainCamera;  
    private Camera launcherCamera;
    private bool followRocket = false;

    void Start()
    {
        mainCamera = Camera.main;

        rocketCamera = GameObject.Find("RocketCamera").GetComponent<Camera>();
        if (rocketCamera != null)
        {
            rocketCamera.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            followRocket = !followRocket;

            if (followRocket)
            {
                mainCamera.enabled = false; 
                if (rocketCamera != null)
                {
                    rocketCamera.enabled = true; 
                }
            }
            else
            {
                if (rocketCamera != null)
                {
                    rocketCamera.enabled = false; 
                }
                mainCamera.enabled = true; 
            }
        }

        if (followRocket && rocketCamera != null)
        {
            rocketCamera.transform.position = transform.position - transform.forward * 10 + Vector3.up * 5;
            rocketCamera.transform.LookAt(transform);

            if (transform == null)
            {
                StartCoroutine(ReturnToMainCameraAfterDelay(1f)); 
            }
        }
    }

    IEnumerator ReturnToMainCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rocketCamera != null)
        {
            rocketCamera.enabled = false;
        }
        if (mainCamera != null)
        {
            mainCamera.enabled = true; 
        }
        
    }
}