using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private int startRadius = 5;
    [SerializeField] private int dynamicRadius = 4;
    [SerializeField] private float chunkCheckInterval = 0.5f;
    [SerializeField] private float lookAheadDistance = 64f;

    private Vector3Int currentChunkPos = new Vector3Int(0, 0 , 0);
    private Vector3Int lastCheckedChunkPos;

    private List<Chunk> chunkList;
    private TerrainGenerator terrainGenerator;

    private float checkTimer;

    private Queue<Vector2Int> chunkQueue;
    private bool isGeneratingChunks = false;

    private void Start()
    {
        chunkQueue = new Queue<Vector2Int>();

        terrainGenerator = new TerrainGenerator();
        chunkList = new List<Chunk>();

        currentChunkPos = GetChunkCoord(transform.position);
        lastCheckedChunkPos = currentChunkPos;

        StartChunkPlaceInRadius(startRadius, currentChunkPos.x, currentChunkPos.z);
    }

    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= chunkCheckInterval)
        {
            checkTimer = 0f;
            DynamicChunkPlace();
        }
    }

    private void DynamicChunkPlace()
    {
        currentChunkPos = GetChunkCoord(transform.position);
        if (currentChunkPos != lastCheckedChunkPos)
        {
            ChunkPlaceInRadius(dynamicRadius, currentChunkPos.x, currentChunkPos.z);

            Vector3 forward = transform.forward.normalized;
            Vector3 lookAheadPos = transform.position + forward * lookAheadDistance;
            Vector3Int lookChunk = GetChunkCoord(lookAheadPos);

            ChunkPlaceInRadius(dynamicRadius, lookChunk.x, lookChunk.z);

            lastCheckedChunkPos = currentChunkPos;
        }
    }

    private Vector3Int GetChunkCoord(Vector3 position)
    {
        return new Vector3Int(
            Mathf.FloorToInt(position.x / (16 * Block.BlockScale)),
            0,
            Mathf.FloorToInt(position.z / (16 * Block.BlockScale))
        );
    }
    private void StartChunkPlaceInRadius(int radius, int centerX, int centerZ)
    {
        for (int x = -radius; x <= radius; x++)
        {
            for (int z = -radius; z <= radius; z++)
            {
                Vector3Int chunkCoord = new Vector3Int((centerX + x) * 16, 0, (centerZ + z) * 16);

                if (!chunkList.Any(chunk => chunk.ChunkPos == chunkCoord))
                {
                    ChunkPlace(x, z);
                }
            }
        }

        if (!isGeneratingChunks) StartCoroutine(ProcessChunkQueue());

    }
    private void ChunkPlaceInRadius(int radius, int centerX, int centerZ)
    {
        for (int x = -radius; x <= radius; x++)
        {
            for (int z = -radius; z <= radius; z++)
            {
                Vector3Int chunkCoord = new Vector3Int((centerX + x) * 16, 0, (centerZ + z) * 16);

                if (!chunkList.Any(chunk => chunk.ChunkPos == chunkCoord))
                {
                    chunkQueue.Enqueue(new Vector2Int(centerX + x, centerZ + z));
                }
            }
        }

        if (!isGeneratingChunks) StartCoroutine(ProcessChunkQueue());

    }

    private IEnumerator ProcessChunkQueue()
    {
        isGeneratingChunks = true;

        while (chunkQueue.Count > 0)
        {
            Vector2Int chunkCoord = chunkQueue.Dequeue();
            ChunkPlace(chunkCoord.x, chunkCoord.y);

            yield return new WaitForSeconds(0.05f);
        }

        isGeneratingChunks = false;
    }
    private void ChunkPlace(int x, int z)
    {
        BlockType[,,] blocks = terrainGenerator.GenerateTerrain(x, z);
        Vector3 position = new Vector3(x * 16 * Block.BlockScale, 0, z * 16 * Block.BlockScale);

        GameObject chunkObject = Instantiate(chunkPrefab, position, Quaternion.identity);
        Mesh chunkMesh = chunkObject.GetComponent<MeshFilter>().mesh;
        MeshCollider chunkCollider = chunkObject.GetComponent<MeshCollider>();

        Chunk chunk = new Chunk(new Vector3Int(x * 16, 0, z * 16), blocks);
        chunkList.Add(chunk);

        chunkObject.GetComponent<ChunkInfoContainer>().ChunkInfo = chunk;

        ChunkMeshGenerator meshGen = new ChunkMeshGenerator(blocks, chunkMesh);
        meshGen.GenerateMesh();
        meshGen.GenerateMeshCollider(chunkCollider);

        chunkMesh = meshGen.GetMesh();
        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();
    }
}