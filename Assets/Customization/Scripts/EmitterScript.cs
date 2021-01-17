using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject enemy;
    public float minAsteroidDelay, maxAsteroidDelay;
    public float minEnemyDelay, maxEnemyDelay;
    private float nextAsteroidLaunchTime;
    private float nextEnemyLaunchTime;

    // Update is called once per frame
    void Update()
    {
        if (!Controller.isGameStarted) return;

        LaunchAsteroidsWithDelay();
        LaunchEnemiesWithDelay();
    }

    private void LaunchEnemiesWithDelay()
    {
        if (Time.time > nextEnemyLaunchTime)
        {
            nextEnemyLaunchTime = Time.time + Random.Range(minEnemyDelay, maxEnemyDelay);
            LaunchEnemy();
        }
    }

    private void LaunchEnemy()
    {
        float half = transform.localScale.x / 2;
        float left = -half + half * 0.3f;
        float right = half - half * 0.3f;
        float posX = Random.Range(left, right);
        float posY = transform.position.y;
        float posZ = transform.position.z;

        Instantiate(enemy, new Vector3(posX, posY, posZ), Quaternion.Euler(0, 180, 0));
    }

    private void LaunchAsteroidsWithDelay()
    {
        if (Time.time > nextAsteroidLaunchTime)
        {
            nextAsteroidLaunchTime = Time.time + Random.Range(minAsteroidDelay, maxAsteroidDelay);
            LaunchAsteroid();
        }
    }

    private void LaunchAsteroid()
    {
        float left = -transform.localScale.x / 2;
        float right = transform.localScale.x / 2;
        float posX = Random.Range(left, right);
        float posY = transform.position.y;
        float posZ = transform.position.z;

        Instantiate(getRandomAsteroidObject(), new Vector3(posX, posY, posZ), Quaternion.identity);
    }

    private GameObject getRandomAsteroidObject()
    {
        GameObject randomAsteroid;
        switch (Random.Range(0, 3))
        {
            case 0:
                randomAsteroid = asteroid;
                break;
            case 1:
                randomAsteroid = asteroid2;
                break;
            case 2:
                randomAsteroid = asteroid3;
                break;
            default:
                randomAsteroid = asteroid;
                break;
        }

        return randomAsteroid;
    }
}
