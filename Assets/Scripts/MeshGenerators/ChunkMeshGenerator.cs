using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ChunkMeshGenerator : IChunkMeshGenerator
{

    readonly BlockType[,,] blocks;

    internal Mesh ChunkMesh;

    BlockVerticesAndTrianglesGenerator blockVerticiesAndTrianglesGenerator;
    BlockUVGenerator blockUVGenerator;
    public ChunkMeshGenerator(BlockType[,,] blocks, Mesh ChunkMesh)
    {
        this.blocks = blocks;
        this.ChunkMesh = ChunkMesh;
    }
    public void GenerateMesh()
    {
        List<Vector3> verticesList = ChunkMesh.vertices.ToList<Vector3>();
        List<int> trinaglesList = ChunkMesh.triangles.ToList<int>();
        List<Vector2> uvList = ChunkMesh.uv.ToList<Vector2>();

        for (int x = 0; x < Chunk.ChunkWidth; x++)
        {
            for (int y = 0; y < Chunk.ChunkDepth; y++)
            {
                for (int z = 0; z < Chunk.ChunkHeight; z++)
                {
                    blockVerticiesAndTrianglesGenerator = new BlockVerticesAndTrianglesGenerator(blocks, new Vector3(x, y, z), verticesList, trinaglesList);
                    blockUVGenerator = new BlockUVGenerator(blocks, new Vector3(x, y, z), uvList, blocks[x, y, z]);

                    blockVerticiesAndTrianglesGenerator.GenerateVerticesAndTringles();
                    blockUVGenerator.AddUVToBlock();
                }
            }
        }

        ChunkMesh.vertices = verticesList.ToArray();
        ChunkMesh.uv = uvList.ToArray();
        ChunkMesh.triangles = trinaglesList.ToArray();
    }

    public void GenerateMeshCollider(MeshCollider collider)
    {
        collider.sharedMesh = ChunkMesh;
    }

    public Mesh GetMesh()
    {
        return ChunkMesh;
    }
}