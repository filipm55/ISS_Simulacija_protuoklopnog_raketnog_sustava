using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCamera : MonoBehaviour
{
    private Camera rocketCamera; // Nova kamera za praćenje rakete
    private Camera mainCamera;  // Glavna kamera
    private Camera launcherCamera;
    private bool followRocket = false;

    void Start()
    {

        
        // Pronađi glavnu kameru
        mainCamera = Camera.main;

        rocketCamera = GameObject.Find("RocketCamera").GetComponent<Camera>();
        // Osiguraj da je raketna kamera neaktivna na početku
        if (rocketCamera != null)
        {
            rocketCamera.enabled = false;
        }
    }

    void Update()
    {
        // Aktivacija/deaktivacija raketne kamere
        if (Input.GetKeyDown(KeyCode.C)) // Tipka za prebacivanje (npr. 'C')
        {
            followRocket = !followRocket;

            if (followRocket)
            {
                mainCamera.enabled = false; // Isključi glavnu kameru
                if (rocketCamera != null)
                {
                    rocketCamera.enabled = true; // Uključi raketnu kameru
                }
            }
            else
            {
                if (rocketCamera != null)
                {
                    rocketCamera.enabled = false; // Isključi raketnu kameru
                }
                mainCamera.enabled = true; // Uključi glavnu kameru
            }
        }

        // Ako kamera prati raketu, prilagodi joj poziciju i orijentaciju
        if (followRocket && rocketCamera != null)
        {
            rocketCamera.transform.position = transform.position - transform.forward * 10 + Vector3.up * 5; // Prilagodba udaljenosti i visine
            rocketCamera.transform.LookAt(transform);

            // Provjeri je li objekt uništen
            if (transform == null)
            {
                StartCoroutine(ReturnToMainCameraAfterDelay(1f)); // Pokreni povratak nakon 1 sekunde <- ne radi 
            }
        }
    }

    IEnumerator ReturnToMainCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rocketCamera != null)
        {
            rocketCamera.enabled = false; // Isključi raketnu kameru
        }
        if (mainCamera != null)
        {
            mainCamera.enabled = true; // Uključi glavnu kameru
        }
        
    }
}