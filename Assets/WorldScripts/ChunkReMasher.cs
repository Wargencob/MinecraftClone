using BlockGen;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class ChunkReMasher
{
    private Chunk chunk;
    public ChunkReMasher()
    {
        
    }

    public Mesh AddBlockToChunkMesh(Mesh chunkMesh, int x, int y, int z)
    {
        BlockGenerator blockGenerator = new();

        var block = blockGenerator.GenerateOneBlock(x, y, z);

        var verticesList = chunkMesh.vertices.ToList<Vector3>();
        var trianglesList = chunkMesh.triangles.ToList<int>();

        int vertexOffset = verticesList.Count;

        verticesList.AddRange(block.mesh.vertices);

        foreach (int index in block.mesh.triangles)
        {
            trianglesList.Add(index + vertexOffset);
        }

        chunkMesh.vertices = verticesList.ToArray();
        chunkMesh.triangles = trianglesList.ToArray();

        return chunkMesh;
    }

    public Mesh RemoveBlockFromTheMesh(Mesh chunkMesh, int x, int y, int z, Vector3 vetrex, int triagle)
    {
        var verticesList = chunkMesh.vertices.ToList<Vector3>();
        var trianglesList = chunkMesh.triangles.ToList<int>();
        
        var pos = new Vector3(x, y, z);
        

        chunkMesh.vertices = verticesList.ToArray();

        return chunkMesh;
    }

}

