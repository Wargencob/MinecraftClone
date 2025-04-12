using BlockGen;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChunkGen
{
    public class ChunkGenerator
    {
        private BlockGenerator blockGenerator;
        public Chunk GenerateChunk(BlockType[,,] blocks, Chunk chunk)
        {
            blockGenerator = new BlockGenerator(blocks);

            for (int x = 0 ; x < Chunk.chunkWidth; x++)
            {
                for (int y = 0; y < Chunk.chunkDepth; y++)
                {
                    for (int z = 0; z < Chunk.chunkHeight; z++)
                    {
                        Block block = blockGenerator.GenerateBlocksInChunk(x, y, z);
                        if (block != null)
                        {
                            chunk.mesh.meshBlocks.Add(block);
                        }
                    }
                }
            }
            
            AcceptMesh(chunk);
            return chunk;
        }
        public static Chunk AcceptMesh(Chunk chunk)
        {
            var Mesh = chunk.GetComponent<MeshFilter>().mesh;
            var Colider = chunk.GetComponent<MeshCollider>();

            var verticesList = Mesh.vertices.ToList<Vector3>();
            var trianglesList = Mesh.triangles.ToList<int>();

            foreach(var block in chunk.mesh.meshBlocks)
            {
                int vertexOffset = verticesList.Count;

                verticesList.AddRange(block.mesh.vertices);

                foreach (int index in block.mesh.triangles)
                {
                    trianglesList.Add(index + vertexOffset);
                }
            }

            Mesh.vertices = verticesList.ToArray();
            Mesh.triangles = trianglesList.ToArray();

            Colider.sharedMesh = Mesh;

            Mesh.RecalculateBounds();
            Mesh.RecalculateNormals();
            Mesh.Optimize();

            return chunk;
        }
    }
}
