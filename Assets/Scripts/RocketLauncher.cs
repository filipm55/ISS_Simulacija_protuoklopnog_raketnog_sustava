using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject missilePrefab;   // Drag your missile prefab here
    public Transform launchPoint;      // Drag the launch point here
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            FireMissile();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireMissile()
    {
        Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
        Debug.Log("Missile Fired!");
    }
}