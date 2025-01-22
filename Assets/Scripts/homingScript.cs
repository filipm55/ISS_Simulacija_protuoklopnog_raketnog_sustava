using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingScript : MonoBehaviour
{
    public Transform target;
    private float missileSpeed = 20f;
    private bool missileActive = false;
    private Rigidbody rb;

    [SerializeField]
    private float acceleration = 28f;
    [SerializeField]
    private float accelerationTime = 2f;
    [SerializeField]
    private bool isAccelerating = false;
    [SerializeField]
    private float turnRate = 20f;
    [SerializeField]
    private float trackingDelay = 1f;
    [SerializeField]
    private GameObject explosion;

    private Quaternion guideRotation;
    private bool targetTracking = false;

    private float accelerateActiveTime = 0f;


    // --- Script Variables ---
    private bool targetHit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ActivateMissile();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetHit) return;
        Run();
        guideMissile();
    }

    private void ActivateMissile()
    {
        missileActive = true;
        accelerateActiveTime = Time.time;
        if (target != null)
    {
        Transform targetPoint = target.Find("TargetPoint");
        if (targetPoint != null)
        {
            target = targetPoint;
            Debug.Log("TargetPoint je uspješno postavljen kao cilj.");
        }
        else
        {
            Debug.LogWarning("Target nema dijete pod nazivom 'TargetPoint'!");
        }
    }
        else
    {
        Debug.LogWarning("Target nije postavljen!");
    }

    StartCoroutine(TargetTrackingDelay());
    }

    IEnumerator TargetTrackingDelay()
    {
        yield return new WaitForSeconds(trackingDelay);
        targetTracking = true;
        Debug.Log("tracking");
    }

    private void Run()
    {
        if (Since(accelerateActiveTime) > accelerationTime)
            isAccelerating = false;
        else
            isAccelerating = true;

        if (!missileActive) return;
        if (isAccelerating)
            missileSpeed += acceleration * Time.deltaTime;
        rb.velocity = transform.forward * missileSpeed;

        if (targetTracking)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, guideRotation, turnRate * Time.deltaTime);
        //Debug.Log("Missile Speed " + missileSpeed);

    }

    private float Since(float since)
    {
        return Time.time - since;
    }

    private void guideMissile()
    {
        if (target == null) return;
        if (targetTracking)
        {
            Vector3 relativePosition = target.position - transform.position;
            guideRotation = Quaternion.LookRotation(relativePosition, transform.up);
        }
    }

    /*public IEnumerator SendHoming()
    {
        while (Vector3.Distance(target.transform.position, gameObject.transform.position) > 0.3f)
        {
            gameObject.transform.position += (target.transform.position - gameObject.transform.position).normalized * speed * Time.deltaTime;
            gameObject.transform.LookAt(target.transform);
            yield return null;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {

        Instantiate(explosion, transform.position, transform.rotation);
        // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
        Destroy(gameObject);
    }

}

