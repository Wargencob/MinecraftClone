using Unity.Burst.CompilerServices;
using UnityEngine;
class ChunkChecker : MonoBehaviour
{ 
    Chunk currentChunk;
    private void Start()
    {
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentChunk = hit.collider.GetComponent<Chunk>();
        }
    }
    private void Update()
    {
        ChunkCheck();
    }
    public int ChunkCheck()
    {
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.down * Mathf.Infinity);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
           
            if (chunk != currentChunk)
            {
                Debug.Log($"Id чанка: {chunk.id}, координаты чанка: {chunk.x},{chunk.z}");
                currentChunk = chunk;
                return chunk.id;
            }
            else
            {
                return currentChunk.id;
            }
        }
        else
        {
            return 0;
        }
    }
}