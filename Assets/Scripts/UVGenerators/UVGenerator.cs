using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockUVGenerator
{
    BlockType[,,] blocks;
    BlockType block;
    Vector3 blockPosition;
    List<Vector2> UV;


    private bool isUp = false;
    private bool isDown = false;
    private bool isBottom = false;
    private bool isLeft = false;
    public BlockUVGenerator(BlockType[,,] blocks, Vector3 blockPosition, List<Vector2> UV, BlockType block)
    {
        this.block = block;
        this.UV = UV;
        this.blocks = blocks;
        this.blockPosition = blockPosition;
        this.block = block;
    }
    public void AddUVToBlock()
    {
        if (blocks[(int)blockPosition.x, (int)blockPosition.y, (int)blockPosition.z] == 0)
            return;

        if (!BlockVerticesAndTrianglesGenerator.IsGetTouch(blocks, blockPosition + Vector3.left))
        {
            isLeft = true;
            AddUV(block, isBottom);
        }

        if (!BlockVerticesAndTrianglesGenerator.IsGetTouch(blocks, blockPosition + Vector3.right))
        {
            isBottom = true;
            AddUV(block, isBottom);
        }

        if (!BlockVerticesAndTrianglesGenerator.IsGetTouch(blocks, blockPosition + Vector3.up))
        {
            isUp = true;
            AddUV(block, isUp);
        }

        if (!BlockVerticesAndTrianglesGenerator.IsGetTouch(blocks, blockPosition + Vector3.down))
        {
            isDown = true;
            AddUV(block, isDown);
        }

        if (!BlockVerticesAndTrianglesGenerator.IsGetTouch(blocks, blockPosition + Vector3.back))
        {
            isBottom = true;
            AddUV(block, isBottom);
        }

        if (!BlockVerticesAndTrianglesGenerator.IsGetTouch(blocks, blockPosition + Vector3.forward))
        {
            isBottom = true;
            AddUV(block, isBottom);
        }
    }

    public void AddUV(BlockType block, bool side)
    {
        float atlasLenght = 1280f;
        float textureLenght = 160f;
        Vector2 offsetVector = new Vector2(textureLenght/atlasLenght, 0);

        switch (block)
        {
            case BlockType.Dirt:
                {
                    UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Dirt - 1));
                    UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Dirt - 1));
                    UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Dirt));
                    UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Dirt));

                    break;
                }
            case BlockType.Stone:
                {
                    UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Stone - 1));
                    UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Stone - 1));
                    UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Stone));
                    UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Stone));

                    break;
                }
            case BlockType.Grass:
                {
                    if(isUp)
                    {
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Grass - 1));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Grass - 1));
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Grass));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Grass));

                        isUp = false;

                        break;
                    }
                    if(isLeft)
                    {
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Grass + 1));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Grass + 1));

                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Grass));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Grass));
                        
                 
                        isLeft = false;

                        break;
                    }
                    if (isDown)
                    {
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Dirt - 1));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Dirt - 1));
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Dirt));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Dirt));

                        isDown = false;

                        break;
                    }
                    if( isBottom)
                    {
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Grass));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Grass));
                        UV.Add(new Vector2(0, 0) + offsetVector * ((int)BlockType.Grass + 1));
                        UV.Add(new Vector2(0, 1) + offsetVector * ((int)BlockType.Grass + 1));

                        isBottom = false;

                        break;
                    }

                    break;
                }
        }


       
    }
}