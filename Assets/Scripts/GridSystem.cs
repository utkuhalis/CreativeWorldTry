using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int gridSize = 10; // Adjust this to change the grid density.
    public GameObject targetSphere; // Drag the sphere GameObject you want to create a grid around into this field in the Inspector.
    public GameObject cubePrefab; // Drag the cube prefab you want to spawn into this field in the Inspector.

    private List<Vector3> gridPoints;

    private void Start()
    {
        GenerateSphereGrid();
        SpawnCubes();
    }

    private void GenerateSphereGrid()
    {
        if (targetSphere == null)
        {
            Debug.LogError("Target sphere is not assigned!");
            return;
        }

        gridPoints = new List<Vector3>();

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float phi = 2 * Mathf.PI * i / gridSize;
                float theta = Mathf.PI * j / gridSize;

                float x = targetSphere.transform.position.x + targetSphere.transform.localScale.x * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = targetSphere.transform.position.y + targetSphere.transform.localScale.y * Mathf.Sin(theta) * Mathf.Sin(phi);
                float z = targetSphere.transform.position.z + targetSphere.transform.localScale.z * Mathf.Cos(theta);

                Vector3 point = new Vector3(x, y, z);

                gridPoints.Add(point);
            }
        }
    }

    private void SpawnCubes()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube prefab is not assigned!");
            return;
        }

        foreach (Vector3 point in gridPoints)
        {
            // Instantiate a cube at a random position on the sphere grid.
            Vector3 randomOffset = Random.insideUnitSphere * 0.5f; // Adjust 0.5f for cube size.
            Vector3 cubePosition = point + randomOffset;

            // Instantiate the cube prefab.
            GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        }
    }
}