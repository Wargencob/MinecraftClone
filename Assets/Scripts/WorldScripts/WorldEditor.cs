using Unity.VisualScripting;
using UnityEngine;

public class WorldEditor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private float maxRayLength = 3f;
    private float returnSpeed = 1f;

    RaycastHit hit;

    Mesh chunkMesh;

    private float currentRayLength;
    private void Start()
    {
        currentRayLength = maxRayLength;
    }
    private void Update()
    {
        RayProcess();
    }
    
    internal void RayProcess()
    {
        var ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayLength))
        {
            this.hit = hit;
           
            currentRayLength = hit.distance;
            Debug.DrawRay(ray.origin, ray.direction * currentRayLength, Color.red);

            chunkMesh = hit.collider.GetComponent<MeshFilter>().mesh;

            InputLogic();
        }
        else
        {
            currentRayLength = Mathf.Lerp(currentRayLength, maxRayLength, Time.deltaTime * returnSpeed);
            Debug.DrawRay(ray.origin, ray.direction * currentRayLength, Color.gray);
        }
    }
    internal void InputLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BlockPlace();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            BlockDelete();
        }
    }
    internal void BlockDelete()
    {
        ChunkRemasher chunkRemasher = new ChunkRemasher(chunkMesh);

        chunkRemasher.RemoveBlockFromChunk(hit);

        chunkMesh = chunkRemasher.GetMesh();
    }
    internal void BlockPlace()
    {
        ChunkRemasher chunkRemasher = new ChunkRemasher(chunkMesh);

        chunkRemasher.AddBlockToChunk(hit);

        chunkMesh = chunkRemasher.GetMesh();
    }
}