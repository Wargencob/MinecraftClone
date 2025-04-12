using BlockGen;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WorldEditor : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;
    private Chunk chunk;
    private ChunkReMasher chunkReMasher;

    private Ray ray;
    private RaycastHit hit;

    public float maxRayLength = 10f;
    public float returnSpeed = 1f;

    private float currentRayLength;

    void Start()
    {
        currentRayLength = maxRayLength;
        chunkReMasher = new();
    }

    void Update()
    {
        ProcessRay();
    }

    private void ProcessRay()
    {
        ray = new Ray(CameraTransform.position, CameraTransform.forward);
        hit = RayLogic();

        Debug.DrawRay(ray.origin, ray.direction * currentRayLength, Color.red);
    }

    private RaycastHit RayLogic()
    {
        if (Physics.Raycast(ray, out hit, maxRayLength))
        {
            currentRayLength = hit.distance;
            InputLogic();
        }
        else
        {
            currentRayLength = Mathf.Lerp(currentRayLength, maxRayLength, Time.deltaTime * returnSpeed);
        }

        return hit;
    }

    private void InputLogic()
    {
        chunk = hit.collider.GetComponent<Chunk>();

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.normal.x + hit.normal.y + hit.normal.z > 0)
            {
                CreateBlock(hit.point);
            }
            else
            {
                CreateBlock(hit.point + hit.normal);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            DeleteBlock(hit.point);
        }
    }

    private void DeleteBlock(Vector3 pos)
    {
        var chunkMesh = chunk.GetComponent<MeshFilter>().mesh;
        var meshCollider = chunk.GetComponent<MeshCollider>();
        ChunkReMasher chunkReMasher = new();

        Vector3 localPos = chunk.transform.InverseTransformPoint(pos);
        chunkMesh = chunkReMasher.RemoveBlockFromTheMesh(chunkMesh, 
            (int)localPos.x, (int)localPos.y, (int)localPos.z, 
            Vector3Int.FloorToInt(pos), hit.triangleIndex);

        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();
        meshCollider.sharedMesh = chunkMesh;
    }

    private void CreateBlock(Vector3 pos)
    {
        var chunkMesh = chunk.GetComponent<MeshFilter>().mesh;
        var meshCollider = chunk.GetComponent<MeshCollider>();

        Vector3 localPos = chunk.transform.InverseTransformPoint(pos);
        chunkMesh = chunkReMasher.AddBlockToChunkMesh(chunkMesh, (int)localPos.x, (int)localPos.y, (int)localPos.z);

        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();
        meshCollider.sharedMesh = chunkMesh;
    }
}