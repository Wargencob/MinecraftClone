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
        blockGenerator = new();
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
            CreateBlock(Vector3Int.FloorToInt(hit.point) + (Vector3Int)hit.normal);
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

    private void CreateBlock(Vector3Int pos)
    {
        //var obj = Instantiate(prefab, pos, Quaternion.identity);
        //obj.transform.localScale = Vector3.one * 50;
        var mesh = blockGenerator.AddBlockToChunkMesh(pos.x, pos.y, pos.z);
        foreach(var verticies in mesh.Item1)
            Debug.Log(verticies);
        //var chunkVerticies = chunk.GetComponent<MeshFilter>().mesh.vertices.ToList<Vector3>();
        //var chunkTriangles = chunk.GetComponent<MeshFilter>().mesh.triangles.ToList<int>();
        //foreach (var vert in mesh.Item1)
        //{
        //    chunkVerticies.Add(vert);
        //}
        //chunk.GetComponent<MeshFilter>().mesh.vertices = chunkVerticies.ToArray();
        //foreach(var triangles in mesh.Item2)
        //{
        //    chunkTriangles.Add(triangles);
        //}
        //chunk.GetComponent<MeshFilter>().mesh.triangles = mesh.Item2.ToArray();
    }
}