using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 0.5f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);  // Destroy after time
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  // Move forward
    }

    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("Missile hit: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject); 
    }
}