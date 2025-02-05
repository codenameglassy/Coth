using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMeshGpuInstancing : MonoBehaviour
{
    public Mesh targetMesh; // Assign the mesh to filter
    public Material material; // Assign the material to use for instancing

    private List<Matrix4x4> instanceMatrices = new List<Matrix4x4>(); // Store transformation matrices
    private List<Transform> instanceTransforms = new List<Transform>(); // Store the transforms for easy updates
    private List<Renderer> instanceRenderers = new List<Renderer>(); // Store the renderers for culling

    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        if (!material)
        {
            Debug.LogError("Material is not assigned. Please assign a material.");
            return;
        }

        if (!material.enableInstancing)
        {
            Debug.LogWarning("Material does not support instancing. Enable 'Enable GPU Instancing' in the material inspector.");
            return;
        }

        if (!targetMesh)
        {
            Debug.LogError("Target mesh is not assigned. Please assign a mesh to filter.");
            return;
        }

        // Find all objects with the target mesh
        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
        Debug.Log($"Found {meshFilters.Length} MeshFilter objects in the scene.");

        mainCamera = Camera.main; // Get the main camera reference
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene.");
            return;
        }

        foreach (var meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh == targetMesh)
            {
                instanceTransforms.Add(meshFilter.transform);
                instanceRenderers.Add(meshFilter.GetComponent<Renderer>());

                // Add initial matrix
                instanceMatrices.Add(Matrix4x4.TRS(meshFilter.transform.position, meshFilter.transform.rotation, meshFilter.transform.localScale));

                // Disable the original MeshRenderer to avoid double rendering
                MeshRenderer renderer = meshFilter.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
                else
                {
                    Debug.LogWarning($"MeshFilter on {meshFilter.name} has no MeshRenderer.");
                }
            }
        }

        if (instanceTransforms.Count == 0)
        {
            Debug.LogWarning($"No objects with the specified mesh '{targetMesh.name}' were found in the scene.");
        }
        else
        {
            Debug.Log($"Prepared {instanceTransforms.Count} objects for GPU instancing.");
        }
    }

    void Update()
    {
        if (instanceTransforms.Count == 0) return;

        // Update matrices in case transforms have changed
        for (int i = 0; i < instanceTransforms.Count; i++)
        {
            Transform t = instanceTransforms[i];
            instanceMatrices[i] = Matrix4x4.TRS(t.position, t.rotation, t.localScale);
        }

        // Culling: Use camera frustum to cull objects outside of the view
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        List<Matrix4x4> visibleMatrices = new List<Matrix4x4>();
        List<Transform> visibleTransforms = new List<Transform>();

        for (int i = 0; i < instanceTransforms.Count; i++)
        {
            Renderer renderer = instanceRenderers[i];
            if (renderer != null && GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                // Object is within the camera's frustum
                visibleMatrices.Add(instanceMatrices[i]);
                visibleTransforms.Add(instanceTransforms[i]);
            }
        }

        // Draw all instances that are visible
        int batchSize = 1023; // Max instances per batch
        for (int i = 0; i < visibleMatrices.Count; i += batchSize)
        {
            int count = Mathf.Min(batchSize, visibleMatrices.Count - i);
            Graphics.DrawMeshInstanced(targetMesh, 0, material, visibleMatrices.GetRange(i, count).ToArray(), count, null);
        }
    }

}
