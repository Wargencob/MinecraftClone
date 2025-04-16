using BlockGen;
using UnityEngine;

public class WorldEditor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxRayLength = 10f;
    [SerializeField] private float returnSpeed = 1f;

    private ChunkReMasher chunkReMasher;
    private float currentRayLength;

    void Start()
    {
        currentRayLength = maxRayLength;
        chunkReMasher = new ChunkReMasher();
    }

    void Update()
    {
        ProcessRay();
    }

    private void ProcessRay()
    {
        var ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayLength))
        {
            currentRayLength = hit.distance;
            Debug.DrawRay(ray.origin, ray.direction * currentRayLength, Color.red);
            HandleInput(hit);
        }
        else
        {
            currentRayLength = Mathf.Lerp(currentRayLength, maxRayLength, Time.deltaTime * returnSpeed);
            Debug.DrawRay(ray.origin, ray.direction * currentRayLength, Color.gray);
        }
    }

    private void HandleInput(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateBlockMesh(hit, true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            UpdateBlockMesh(hit, false);
        }
    }

    private void UpdateBlockMesh(RaycastHit hit, bool isAdding)
    {
        Mesh updatedMesh = isAdding
            ? chunkReMasher.AddBlockToChunkMeshByHit(hit)
            : chunkReMasher.RemoveBlockFromTheMeshByHit(hit);

        var meshFilter = hit.collider.GetComponent<MeshFilter>();
        var meshCollider = hit.collider.GetComponent<MeshCollider>();

        meshFilter.mesh = updatedMesh;
        meshCollider.sharedMesh = updatedMesh;
    }
}