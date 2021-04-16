using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public List<GameObject> asteroids;
    float secondsBetweenSpawn;
    float elapsedTime = 0.0f;
    GameObject[] SpawnPoints;
    GameObject newAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        secondsBetweenSpawn = 1.5f;
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime <= secondsBetweenSpawn)
        {
            return;
        }
        elapsedTime = 0;
        int spawner = Random.Range(0, SpawnPoints.Length + 1);
        int randomIndex = Random.Range(0, asteroids.Count);

        switch (spawner)
        {
            case 0:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 1:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 2:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 3:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 4:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 5:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 6:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 7:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 8:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 9:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 10:
                SpawnAsteroid(spawner, randomIndex);
                break;
            case 11:
                SpawnAsteroid(spawner, randomIndex);
                break;
            default:
                break;
        }
    }

    private void SpawnAsteroid(int spawner, int randomIndex)
    {
        newAsteroid = Instantiate(asteroids[randomIndex], SpawnPoints[spawner].transform.position, SpawnPoints[spawner].transform.rotation);
        newAsteroid.GetComponent<Asteroid>().SetSize(randomIndex+1);

        float speedY = Random.Range(10f, 300f);
        newAsteroid.GetComponent<Rigidbody2D>().AddForce(transform.up * speedY);


        float speedX = Random.Range(10f, 300f);
        int selectorX = Random.Range(0, 2);
        float dirX = 0;
        if (selectorX == 1) { dirX = -1; }
        else { dirX = 1; }
        float finalSpeedX = speedX * dirX;
        newAsteroid.GetComponent<Rigidbody2D>().AddForce(transform.right * finalSpeedX);
    }
}
