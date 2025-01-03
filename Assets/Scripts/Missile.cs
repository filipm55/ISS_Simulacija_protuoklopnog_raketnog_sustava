using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 0.5f;
    public float lifeTime = 5f;

    [SerializeField] GameObject explosionPrefab;

    void Start()
    {
        Destroy(gameObject, lifeTime);  // Objekt se brise nakon nekog vremena
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  // Kretanje naprijed
    }

    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("Missile hit: " + collision.gameObject.name);
        // eksplozija na mjestu sudara
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy (explosion, 2f);

        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject); 
    }
}