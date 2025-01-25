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
                Destroy(rocketCamera.gameObject, 6f);
            
            if (rocketCamera != null)
            {
                rocketCamera.enabled = true; // Uključi raketnu kameru
                if (rocketCamera.GetComponent<AudioListener>() != null)
                    rocketCamera.GetComponent<AudioListener>().enabled = true; // Omogući Audio Listener na raketnoj kameri
            }
}
else
{
    if (rocketCamera != null)
    {
        rocketCamera.enabled = false; // Isključi raketnu kameru
        if (rocketCamera.GetComponent<AudioListener>() != null)
            rocketCamera.GetComponent<AudioListener>().enabled = false; // Onemogući Audio Listener na raketnoj kameri
    }

    mainCamera.enabled = true; // Uključi glavnu kameru
    if (mainCamera.GetComponent<AudioListener>() != null)
        mainCamera.GetComponent<AudioListener>().enabled = true; // Omogući Audio Listener na glavnoj kameri
}
        }

        if (followRocket && rocketCamera != null)
        {

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