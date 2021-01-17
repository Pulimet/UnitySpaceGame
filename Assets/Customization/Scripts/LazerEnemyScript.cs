using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnemyScript : MonoBehaviour
{

    public float enemyGunSpeed;
    public GameObject playerExplosion;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ship = GameObject.FindGameObjectWithTag("Player");
        if (ship != null)
        {
            transform.LookAt(ship.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * enemyGunSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Destroy(gameObject); // Destroy Lazer
            Destroy(collider.gameObject); // Destroy other object

            GameObject clone = Instantiate(playerExplosion, collider.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 4.0f);

        }
        if (collider.tag == "Enemy")
        {
            Destroy(gameObject); // Destroy Lazer
            Destroy(collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject); // Destroy Enemy

            GameObject clone = Instantiate(playerExplosion, collider.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 4.0f);
        }
    }
}
