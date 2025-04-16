using System.Collections.Generic;
using UnityEngine;
public class Block
{
    public const float blockScale = 1f;

    public int id;

    public Vector3 blockPosition;

    public Vector3 blockCenter;

    public List<Vector2> uvs;

    public BlockMesh mesh;

    public Block()
    {
        mesh = new BlockMesh();

        uvs = new List<Vector2>();
    }
    public BlockType GetPosititon(BlockType[,,] blocks, Vector3 positon)
    {
        if (positon.x >= 0 && positon.x < Chunk.chunkWidth &&
                positon.y >= 0 && positon.y < Chunk.chunkDepth &&
                positon.z >= 0 && positon.z < Chunk.chunkHeight)
        {
            return blocks[(int)positon.x, (int)positon.y, (int)positon.z];
        }
        else
        {
            return BlockType.Air;
        }
    }
}

