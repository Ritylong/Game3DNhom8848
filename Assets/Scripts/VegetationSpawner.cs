using UnityEngine;

public class CustomTerrainPrefabSpawner : MonoBehaviour
{
    public GameObject[] terrainPrefabs; // Array of terrain prefabs (e.g., rocks, trees)
    public GameObject[] groundPieces; // Array of "terrain" prefabs making up the ground
    public int prefabCount = 50; // Number of prefabs to spawn
    public float yOffset = 0.5f; // Offset above the ground surface

    void Start()
    {
        for (int i = 0; i < prefabCount; i++)
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        // Select a random ground piece to place the prefab on
        GameObject groundPiece = groundPieces[Random.Range(0, groundPieces.Length)];

        // Get the bounds of the selected ground piece to determine spawn area
        Bounds bounds = groundPiece.GetComponent<Renderer>().bounds;

        // Generate a random position within the bounds of the selected ground piece
        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y + 10f, // Start the ray above the ground piece
            Random.Range(bounds.min.z, bounds.max.z)
        );

        // Raycast down to find the exact ground position
        if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit))
        {
            // Only proceed if we hit the selected ground piece
            if (hit.collider.gameObject == groundPiece)
            {
                // Adjust position to be slightly above the ground
                Vector3 spawnPosition = hit.point + Vector3.up * yOffset;

                // Choose a random prefab and apply random rotation
                GameObject prefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Length)];
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                // Instantiate the prefab at the calculated position and rotation
                Instantiate(prefab, spawnPosition, randomRotation, transform);
            }
        }
    }
}
