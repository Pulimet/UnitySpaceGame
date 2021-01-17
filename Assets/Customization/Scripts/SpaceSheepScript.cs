using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSheepScript : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float xMin, xMax, zMin, zMax;

    Rigidbody StarShip;

    public GameObject lazerShot;
    public Transform lazerGun;
    public Transform lazerGunL1;
    public Transform lazerGunL2;
    public Transform lazerGunR1;
    public Transform lazerGunR2;

    public float fireDeltaForMainGun;
    public float fireDeltaForSecondaryGuns;
    private float mainGunNextShotTime;
    private float secondaryGunNextShotTime;

    // Start is called before the first frame update
    void Start()
    {
        StarShip = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.isGameStarted) return;

        ShipVelocityControl();
        ShipPositionClamp();
        ShipRotationControl();
        FireControl();
    }

    private void ShipVelocityControl()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        StarShip.velocity = new Vector3(h, 0, v) * speed;
    }

    private void ShipPositionClamp()
    {
        float x = Mathf.Clamp(StarShip.position.x, xMin, xMax);
        float z = Mathf.Clamp(StarShip.position.z, zMin, zMax);

        StarShip.position = new Vector3(x, 0, z);
    }

    private void ShipRotationControl()
    {
        StarShip.rotation = Quaternion.Euler(StarShip.velocity.z * tilt, 0, -StarShip.velocity.x * tilt);
    }

    private void FireControl()
    {
        MainGunFireControl();
        SecondaryGunsFireControl();
    }

    private void MainGunFireControl()
    {
        if (Input.GetButton("Fire1") && Time.time > mainGunNextShotTime)
        {
            mainGunNextShotTime = Time.time + fireDeltaForMainGun;
            GameObject lazer = Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
            lazer.SendMessage("SetDefaultSpeed");
        }
    }

    private void SecondaryGunsFireControl()
    {
        if (Input.GetButton("Fire2") && Time.time > secondaryGunNextShotTime)
        {
            secondaryGunNextShotTime = Time.time + fireDeltaForSecondaryGuns;

            instantiateandScale(lazerGunL1);
            instantiateandScale(lazerGunL2);
            instantiateandScale(lazerGunR1);
            instantiateandScale(lazerGunR2);
        }
    }

    private void instantiateandScale(Transform lazerTransform)
    {
        // TODO CAn use quternion
        GameObject lazer = Instantiate(lazerShot, lazerTransform.position, Quaternion.identity);
        lazer.transform.localScale /= 4;
        float angle = 45.0f;
        if (lazerTransform.name == "LaserGunL1" || lazerTransform.name == "LaserGunL2")
        {
            angle = -45.0f;
        }
        lazer.SendMessage("ChangeVelocityAngle", angle);
    }
}
