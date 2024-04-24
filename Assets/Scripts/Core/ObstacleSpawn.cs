using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject Obstacle;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float TimeBtSpawn;
    private float SpawnTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > SpawnTime)
        {
            Spawn();
            SpawnTime = Time.time + TimeBtSpawn;
        }
        
    }
    void Spawn()
    {
        float X = Random.Range(minX, maxY);
        float Y = Random.Range(minY, maxY);

        Instantiate(Obstacle, transform.position + new Vector3(X, Y, 0), transform.rotation);
    }
}
