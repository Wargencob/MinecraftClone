using UnityEngine;
public class Block
{
    public const float blockScale = 1f;

    public BlockMesh mesh;

    public Mesh blockMesh;

    public Block()
    {
        mesh = new BlockMesh();
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

