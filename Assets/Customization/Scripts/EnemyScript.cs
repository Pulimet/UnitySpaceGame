using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject lazerShot;
    public Transform lazerGun;
    public float speed;
    public float fireDeltaForMainGun;
    public float maxShipAngle;
    public GameObject playerExplosion;
    private float mainGunNextShotTime;
    private float shipAngle;

    // Start is called before the first frame update
    void Start()
    {
        shipAngle = Random.Range(-maxShipAngle, maxShipAngle);
        GetComponent<Rigidbody>().velocity = new Vector3(shipAngle, 0, -speed);
        transform.Rotate(0, shipAngle * -3.5f, 0, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        MainGunFireControl();

        GameObject ship = GameObject.FindGameObjectWithTag("Player");
        if (ship != null)
        {
            transform.LookAt(ship.transform);
        }
    }

    private void MainGunFireControl()
    {
        if (Time.time > mainGunNextShotTime)
        {
            mainGunNextShotTime = Time.time + fireDeltaForMainGun;
            GameObject lazer = Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
            lazer.transform.localScale /= 2;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy" || collider.tag == "Player")
        {
            Destroy(gameObject); // Destroy Enemy
            GameObject clone = Instantiate(playerExplosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 4.0f);
        }

        if (collider.tag == "Player")
        {
            Destroy(collider.gameObject); // Destroy Player
            GameObject clone2 = Instantiate(playerExplosion, collider.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone2, 4.0f);
        }
    }
}
