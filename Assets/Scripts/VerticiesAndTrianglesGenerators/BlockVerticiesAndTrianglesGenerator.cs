using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockVerticesAndTrianglesGenerator : IBlockVerticesAndTrianglesGenerator
{
    readonly BlockType[,,] blocks;
    Vector3 position;

    List<Vector3> vertices;
    List<int> triangles;

    int verticiesCount;

    Block block;
    public BlockVerticesAndTrianglesGenerator(BlockType[,,] blocks, Vector3 position)
    {
        this.blocks = blocks;
        this.position = position;
    }
    public BlockVerticesAndTrianglesGenerator(Vector3 position)
    {
        this.position = position;
    }

    public void GenerateVerticesAndTringles(List<Vector3> vertices, List<int> triangles)
    {
        this.vertices = vertices;
        this.triangles = triangles;

        if (blocks[(int)position.x, (int)position.y, (int)position.z] == 0)
            return;

        if (!IsGetTouch(blocks, position + Vector3.left))
        {
            GenerateLeftSide(position);
        }

        if (!IsGetTouch(blocks, position + Vector3.right))
        {
            GenerateRightSide(position);
        }

        if (!IsGetTouch(blocks, position + Vector3.up))
        {
            GenerateTopSide(position);
        }

        if (!IsGetTouch(blocks, position + Vector3.down))
        {
            GenerateBottomSide(position);
        }

        if (!IsGetTouch(blocks, position + Vector3.back))
        {
            GenerateBackSide(position);
        }

        if (!IsGetTouch(blocks, position + Vector3.forward))
        {
            GenerateFrontSide(position);
        }

        //block = new Block(blocks[(int)position.x, (int)position.y, (int)position.z], mesh);
    }

    public Block GetBlock()
    {
        return block;
    }

    private void GenerateFrontSide(Vector3 pos)
    {
        vertices.Add((new Vector3(0, 0, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 0, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 1, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 1, 1) + pos) * Block.BlockScale);

        verticiesCount = vertices.Count;

        AddTriangles();
    }
    private void GenerateBackSide(Vector3 pos)
    {
        vertices.Add((new Vector3(0, 0, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 1, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 0, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 1, 0) + pos) * Block.BlockScale);

        verticiesCount = vertices.Count;

        AddTriangles();
    }
    private void GenerateBottomSide(Vector3 pos)
    {
        vertices.Add((new Vector3(0, 0, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 0, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 0, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 0, 1) + pos) * Block.BlockScale);

        verticiesCount = vertices.Count;

        AddTriangles();
    }
    private void GenerateTopSide(Vector3 pos)
    {
        vertices.Add((new Vector3(0, 1, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 1, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 1, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 1, 1) + pos) * Block.BlockScale);

        verticiesCount = vertices.Count;

        AddTriangles();
    }
    private void GenerateRightSide(Vector3 pos)
    {
        vertices.Add((new Vector3(1, 0, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 1, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 0, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(1, 1, 1) + pos) * Block.BlockScale);

        verticiesCount = vertices.Count;

        AddTriangles();
    }
    private void GenerateLeftSide(Vector3 pos)
    {
        vertices.Add((new Vector3(0, 0, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 0, 1) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 1, 0) + pos) * Block.BlockScale);
        vertices.Add((new Vector3(0, 1, 1) + pos) * Block.BlockScale);

        verticiesCount = vertices.Count;

        AddTriangles();
    }
    private void AddTriangles()
    {

        triangles.Add(verticiesCount - 4);
        triangles.Add(verticiesCount - 3);
        triangles.Add(verticiesCount - 2);

        triangles.Add(verticiesCount - 3);
        triangles.Add(verticiesCount - 1);
        triangles.Add(verticiesCount - 2);

    }

    public bool IsGetTouch(BlockType[,,] blocks, Vector3 position)
    {
        if
        (
        position.x >= 0 && position.x < Chunk.ChunkWidth &&
        position.y >= 0 && position.y < Chunk.ChunkDepth &&
        position.z >= 0 && position.z < Chunk.ChunkHeight
        )
        {
            if (blocks[(int)position.x, (int)position.y, (int)position.z] != 0)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
}
