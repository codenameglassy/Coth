using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPaintGrass : MonoBehaviour
{
    public Terrain terrain; // Reference to your Terrain object
    public GameObject grassPrefab; // Grass prefab to use for painting
    public int detailDensity = 5; // Number of grass objects per unit
    public float minHeight = 0.0f; // Minimum height to place grass
    public float maxHeight = 1.0f; // Maximum height to place grass

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain is not assigned.");
            return;
        }

        if (grassPrefab == null)
        {
            Debug.LogError("Grass prefab is not assigned.");
            return;
        }

        PaintDetails();
    }

    void PaintDetails()
    {
        TerrainData terrainData = terrain.terrainData;

        // Create detail layer if it doesn't exist
        if (terrainData.detailPrototypes.Length == 0)
        {
            DetailPrototype grassDetail = new DetailPrototype
            {
                prototype = grassPrefab,
                usePrototypeMesh = true,
                renderMode = DetailRenderMode.GrassBillboard,
            };

            terrainData.detailPrototypes = new[] { grassDetail };
        }

        int detailResolution = terrainData.detailResolution;
        int[,] detailLayer = new int[detailResolution, detailResolution];

        for (int y = 0; y < detailResolution; y++)
        {
            for (int x = 0; x < detailResolution; x++)
            {
                float normalizedX = (float)x / detailResolution;
                float normalizedY = (float)y / detailResolution;

                float worldX = normalizedX * terrainData.size.x;
                float worldZ = normalizedY * terrainData.size.z;

                float height = terrainData.GetHeight(
                    (int)(normalizedX * terrainData.heightmapResolution),
                    (int)(normalizedY * terrainData.heightmapResolution)
                );

                float normalizedHeight = height / terrainData.size.y;

                if (normalizedHeight >= minHeight && normalizedHeight <= maxHeight)
                {
                    detailLayer[x, y] = detailDensity; // Apply grass
                }
                else
                {
                    detailLayer[x, y] = 0; // No grass
                }
            }
        }

        terrainData.SetDetailLayer(0, 0, 0, detailLayer);
        Debug.Log("Grass details successfully applied to the terrain.");
    }
}

