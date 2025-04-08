using BlockGen;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldEditor : MonoBehaviour
{
    public Transform CameraTransform;
    public GameObject prefab;
    private BlockGenerator blockGenerator;
    private Chunk chunk;

    private Ray ray;
    private RaycastHit hit;

    public float maxRayLength = 10f;
    public float returnSpeed = 1f;

    private float currentRayLength;

    void Start()
    {
        currentRayLength = maxRayLength;
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
            chunk = hit.collider.GetComponent<Chunk>();
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = (Vector3)Vector3Int.FloorToInt(hit.point);
            Debug.Log(pos);
            CreateBlock(pos);
        }
        if (Input.GetMouseButtonDown(1))
        {
            DeleteBlock();
        }
    }

    private void DeleteBlock()
    {
        throw new NotImplementedException();
    }

    private void CreateBlock(Vector3 pos)
    {
        var chunkMesh = chunk.GetComponent<MeshFilter>().mesh;
        var meshCollider = chunk.GetComponent<MeshCollider>();

        blockGenerator = new(chunkMesh.vertices.ToList<Vector3>(), chunkMesh.triangles.ToList<int>());

        var mesh = blockGenerator.AddBlockToChunkMesh((int)pos.x, (int)pos.y, (int)pos.z);

        chunkMesh.vertices = mesh.Item1.ToArray();
        chunkMesh.triangles = mesh.Item2.ToArray();

        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();
        meshCollider.sharedMesh = chunkMesh;
    }
}