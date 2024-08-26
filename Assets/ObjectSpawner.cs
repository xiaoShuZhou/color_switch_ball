using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] obstacles;   // Array to store obstacle prefabs
    public GameObject item;          // Item prefab
    public GameObject star;          // Star prefab
    public float spawnInterval = 2f; // Time interval between spawns
    public float spawnDistance = 3f; // Fixed vertical distance between spawned objects

    private float lastYPosition;     // Y position of the last spawned object
    private GameObject lastObject;   // Reference to the last spawned object

    void Start()
    {
        lastYPosition = 0f;
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            float newYPosition = lastYPosition + spawnDistance;
            GameObject newObject = null;

            // Ensure at least one collectible is spawned
            if (lastObject == null)
            {
                // Randomly spawn either an item or a star
                int randomChoice = Random.Range(0, 2);
                if (randomChoice == 0)
                {
                    newObject = Instantiate(item, new Vector3(0, newYPosition, 0), Quaternion.identity);
                }
                else
                {
                    newObject = Instantiate(star, new Vector3(0, newYPosition, 0), Quaternion.identity);
                }
            }
            else
            {
                // Based on the last object spawned, choose the next object
                if (lastObject.name == item.name + "(Clone)")
                {
                    // If the last object was an item, spawn an obstacle
                    newObject = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(0, newYPosition, 0), Quaternion.identity);
                    if (newObject.name == "fengche" + "(Clone)")
                    {
                        newObject.transform.position = new Vector3(0.7f, newYPosition, 0);
                    }
                }
                else if (lastObject.name == star.name + "(Clone)")
                {
                    // If the last object was a star, spawn an obstacle
                    newObject = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(0, newYPosition, 0), Quaternion.identity);
                    if (newObject.name == "fengche" + "(Clone)")
                    {
                        newObject.transform.position = new Vector3(0.7f, newYPosition, 0);
                    }
                }
                else
                {
                    // If the last object was an obstacle, randomly spawn an item or a star
                    int randomChoice = Random.Range(0, 2);
                    if (randomChoice == 0)
                    {
                        newObject = Instantiate(item, new Vector3(0, newYPosition, 0), Quaternion.identity);
                    }
                    else
                    {
                        newObject = Instantiate(star, new Vector3(0, newYPosition, 0), Quaternion.identity);
                    }
                }
            }

            if (newObject != null)
            {
                lastObject = newObject;
                lastYPosition = newYPosition;
            }
        }
    }
}