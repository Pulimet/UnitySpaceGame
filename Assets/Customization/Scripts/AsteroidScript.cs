using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;
    public Rigidbody asteroidBody;
    public float rotationSpeed;
    public float speed;

    float size;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody Asteroid = GetComponent<Rigidbody>();

        float speedX = 0;
        if (Random.Range(0, 100) < 30)
        {
            speedX = speed * Random.Range(-0.5f, 0.5f);
        }

        size = Random.Range(0.6f, 1.8f);
        Vector3 speedVector = new Vector3(speedX, 0, -speed) / size;

        Asteroid.velocity = speedVector;
        Asteroid.transform.localScale *= size;

        asteroidBody.velocity = speedVector;
        asteroidBody.transform.localScale *= size;
        asteroidBody.angularVelocity = Random.insideUnitSphere * rotationSpeed;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Asteroid" || collider.tag == "Boundary")
        {
            return;
        }

        // transform.position - Current position
        GameObject clone1 = Instantiate(asteroidExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(clone1, 4.0f);

        if (collider.tag == "Player" || collider.tag == "Enemy")
        {
            GameObject clone2 = Instantiate(playerExplosion, collider.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone2, 4.0f);
        }

        Destroy(gameObject); // Destroy Asteroid
        Destroy(collider.gameObject); // Destroy other object

        if (collider.tag == "Enemy")
        {
            Destroy(collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject);
        }

        if (collider.tag != "Player" || collider.tag != "Enemy")
        {
            Controller.score += 10;
        }

    }
}
