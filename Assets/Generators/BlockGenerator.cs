using UnityEngine;
using ChunkGen;
using System.Collections.Generic;

namespace BlockGen
{
    public class BlockGenerator
    {
        private BlockType[,,] blocks;
        public BlockGenerator(BlockType[,,] blocks)
        {
            this.blocks = blocks;
        }
        public BlockGenerator()
        {
            
        }

        public Block GenerateOneBlock(int x, int y, int z)
        {
            Block block = new Block();

            GenerateLeftSide(new Vector3(x, y, z), block);
            GenerateRightSide(new Vector3(x, y, z), block);
            GenerateTopSide(new Vector3(x, y, z), block);
            GenerateBottomSide(new Vector3(x, y, z), block);
            GenerateBackSide(new Vector3(x, y, z), block);
            GenerateFrontSide(new Vector3(x, y, z), block);

            return block;

        }
        public Block GenerateBlocksInChunk(int x, int y, int z)
        {
            Block block = new Block();

            if (block.GetPosititon(blocks, new Vector3(x, y, z)) == 0)
                return null;

            if (block.GetPosititon(blocks, new Vector3(x, y, z) + Vector3.left) == 0)
            {
                GenerateLeftSide(new Vector3(x, y, z), block);
            }

            if (block.GetPosititon(blocks, new Vector3(x, y, z) + Vector3.right) == 0)
            {
                GenerateRightSide(new Vector3(x, y, z), block);
            }

            if (block.GetPosititon(blocks, new Vector3(x, y, z) + Vector3.up) == 0)
            {
                GenerateTopSide(new Vector3(x, y, z), block);
            }

            if (block.GetPosititon(blocks, new Vector3(x, y, z) + Vector3.down) == 0)
            {
                GenerateBottomSide(new Vector3(x, y, z), block);
            }

            if (block.GetPosititon(blocks, new Vector3(x, y, z) + Vector3.back) == 0)
            {
                GenerateBackSide(new Vector3(x, y, z), block);
            }

            if (block.GetPosititon(blocks, new Vector3(x, y, z) + Vector3.forward) == 0)
            {
                GenerateFrontSide(new Vector3(x, y, z), block);
            }

            return block;
        }

        private void GenerateFrontSide(Vector3 pos, Block block)
        {
            block.mesh.vertices.Add((new Vector3(0, 0, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 0, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 1, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 1, 1) + pos) * Block.blockScale);

            AddVertices(block);
        }
        private void GenerateBackSide(Vector3 pos, Block block)
        {
            block.mesh.vertices.Add((new Vector3(0, 0, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 1, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 0, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 1, 0) + pos) * Block.blockScale);

            AddVertices(block);
        }
        private void GenerateBottomSide(Vector3 pos, Block block)
        {
            block.mesh.vertices.Add((new Vector3(0, 0, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 0, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 0, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 0, 1) + pos) * Block.blockScale);

            AddVertices(block);
        }
        private void GenerateTopSide(Vector3 pos, Block block)
        {
            block.mesh.vertices.Add((new Vector3(0, 1, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 1, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 1, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 1, 1) + pos) * Block.blockScale);

            AddVertices(block);
        }
        private void GenerateRightSide(Vector3 pos, Block block)
        {
            block.mesh.vertices.Add((new Vector3(1, 0, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 1, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 0, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(1, 1, 1) + pos) * Block.blockScale);

            AddVertices(block);
        }
        private void GenerateLeftSide(Vector3 pos, Block block)
        {
            block.mesh.vertices.Add((new Vector3(0, 0, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 0, 1) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 1, 0) + pos) * Block.blockScale);
            block.mesh.vertices.Add((new Vector3(0, 1, 1) + pos) * Block.blockScale);

            AddVertices(block);
        }
        private void AddVertices(Block block)
        {
            block.mesh.triangles.Add(block.mesh.vertices.Count - 4);
            block.mesh.triangles.Add(block.mesh.vertices.Count - 3);
            block.mesh.triangles.Add(block.mesh.vertices.Count - 2);

            block.mesh.triangles.Add(block.mesh.vertices.Count - 3);
            block.mesh.triangles.Add(block.mesh.vertices.Count - 1);
            block.mesh.triangles.Add(block.mesh.vertices.Count - 2);
        }
    }
}
