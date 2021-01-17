using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public float shipGunSpeed;
    public GameObject playerExplosion;

    void SetDefaultSpeed()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, shipGunSpeed);
    }

    void ChangeVelocityAngle(float newAngle)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(newAngle, 0, shipGunSpeed);
        transform.Rotate(0, newAngle, 0, Space.Self);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            Destroy(gameObject); // Destroy Lazer
            Destroy(collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject); // Destroy Enemy

            GameObject clone = Instantiate(playerExplosion, collider.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 4.0f);

            if (collider.tag != "Player" || collider.tag != "Enemy")
            {
                Controller.score += 50;
            }

        }
    }
}
