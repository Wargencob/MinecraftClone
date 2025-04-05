using UnityEngine;
using ChunkGen;
using System.Collections.Generic;

namespace BlockGen
{
    public class BlockGenerator
    {
        const int ChunkWidth = 16;
        const int ChunkDepth = 128;
        const int ChunkHeight = 16;

        private const float blockScale = 1;

        public List<Vector3> verticies;
        public List<int> triangles;

        BlockType[,,] Blocks;

        public BlockGenerator(BlockType[,,] blockTypes)
        {
            Blocks = blockTypes;
           
            verticies = new();
            triangles = new();
        }
        public void GenerateBlock(int x, int y, int z)
        {
            if (GetPosititon(new Vector3(x, y, z)) == 0)
                return;

            if (GetPosititon(new Vector3(x, y, z) + Vector3.left) == 0)
            {
                GenerateLeftSide(new Vector3(x, y, z));
            }

            if (GetPosititon(new Vector3(x, y, z) + Vector3.right) == 0)
            {
                GenerateRightSide(new Vector3(x, y, z));
            }

            if (GetPosititon(new Vector3(x, y, z) + Vector3.up) == 0)
            {
                GenerateTopSide(new Vector3(x, y, z));
            }

            if (GetPosititon(new Vector3(x, y, z) + Vector3.down) == 0)
            {
                GenerateBottomSide(new Vector3(x, y, z));
            }

            if (GetPosititon(new Vector3(x, y, z) + Vector3.back) == 0)
            {
                GenerateBackSide(new Vector3(x, y, z));
            }

            if (GetPosititon(new Vector3(x, y, z) + Vector3.forward) == 0)
            {
                GenerateFrontSide(new Vector3(x, y, z));
            }
        }

        public BlockType GetPosititon(Vector3 positon)
        {
            if (positon.x >= 0 && positon.x < ChunkWidth &&
                 positon.y >= 0 && positon.y < ChunkDepth &&
                 positon.z >= 0 && positon.z < ChunkHeight)
            {
                return Blocks[(int)positon.x, (int)positon.y, (int)positon.z];
            }
            else
            {
                return BlockType.Air;
            }
        }

        private void GenerateFrontSide(Vector3 pos)
        {
            verticies.Add((new Vector3(0, 0, 1) + pos)*blockScale);
            verticies.Add((new Vector3(1, 0, 1) + pos)*blockScale);
            verticies.Add((new Vector3(0, 1, 1) + pos)*blockScale);
            verticies.Add((new Vector3(1, 1, 1) + pos)*blockScale);

            AddVerticies();
        }
        private void GenerateBackSide(Vector3 pos)
        {
            verticies.Add((new Vector3(0, 0, 0) + pos)*blockScale);
            verticies.Add((new Vector3(0, 1, 0) + pos)*blockScale);
            verticies.Add((new Vector3(1, 0, 0) + pos)*blockScale);
            verticies.Add((new Vector3(1, 1, 0) + pos)*blockScale);

            AddVerticies();
        }
        private void GenerateBottomSide(Vector3 pos)
        {
            verticies.Add((new Vector3(0, 0, 0) + pos)*blockScale);
            verticies.Add((new Vector3(1, 0, 0) + pos)*blockScale);
            verticies.Add((new Vector3(0, 0, 1) + pos)*blockScale);
            verticies.Add((new Vector3(1, 0, 1) + pos)*blockScale);

            AddVerticies();
        }
        private void GenerateTopSide(Vector3 pos)
        {
            verticies.Add((new Vector3(0, 1, 0) + pos)*blockScale);
            verticies.Add((new Vector3(0, 1, 1) + pos)*blockScale);
            verticies.Add((new Vector3(1, 1, 0) + pos)*blockScale);
            verticies.Add((new Vector3(1, 1, 1) + pos)*blockScale);

            AddVerticies();
        }
        private void GenerateRightSide(Vector3 pos)
        {
            verticies.Add((new Vector3(1, 0, 0) + pos)*blockScale);
            verticies.Add((new Vector3(1, 1, 0) + pos)*blockScale);
            verticies.Add((new Vector3(1, 0, 1) + pos)*blockScale);
            verticies.Add((new Vector3(1, 1, 1) + pos)*blockScale);

            AddVerticies();
        }
        private void GenerateLeftSide(Vector3 pos)
        {
            verticies.Add((new Vector3(0, 0, 0) + pos)*blockScale);
            verticies.Add((new Vector3(0, 0, 1) + pos)*blockScale);
            verticies.Add((new Vector3(0, 1, 0) + pos)*blockScale);
            verticies.Add((new Vector3(0, 1, 1) + pos)*blockScale);

            AddVerticies();
        }
        private void AddVerticies()
        {
            triangles.Add(verticies.Count - 4);
            triangles.Add(verticies.Count - 3);
            triangles.Add(verticies.Count - 2);

            triangles.Add(verticies.Count - 3);
            triangles.Add(verticies.Count - 1);
            triangles.Add(verticies.Count - 2);
        }
    }
}
