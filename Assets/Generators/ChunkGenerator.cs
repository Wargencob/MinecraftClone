using BlockGen;
using UnityEngine;

namespace ChunkGen
{
    public class ChunkGenerator
    {
        const int ChunkWidth = 16;
        const int ChunkDepth = 128;
        const int ChunkHeight = 16;

        private BlockGenerator blockGenerator;
        public void GenerateChunk(BlockType[,,] blocks)
        {
            blockGenerator = new(blocks);

            for (int x = 0 ; x < ChunkWidth; x++)
            {
                for (int y = 0; y < ChunkDepth; y++)
                {
                    for (int z = 0; z < ChunkHeight; z++)
                    {
                        blockGenerator.GenerateBlock(x, y, z);
                    }
                }
            }
        }
        public void AcceptMesh(GameObject chunk)
        {
            var chunkMesh = chunk.GetComponent<MeshFilter>().mesh;

            chunkMesh.vertices = blockGenerator.verticies.ToArray();
            chunkMesh.triangles = blockGenerator.triangles.ToArray();

            chunkMesh.RecalculateBounds();
            chunkMesh.RecalculateNormals();
            chunkMesh.Optimize();
                
            chunk.GetComponent<MeshFilter>().mesh = chunkMesh;
            chunk.GetComponent<MeshCollider>().sharedMesh = chunkMesh;
        }
    }
}
