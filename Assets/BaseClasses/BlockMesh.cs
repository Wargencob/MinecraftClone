using System.Collections.Generic;
using UnityEngine;
public class BlockMesh
{
    public List<Vector3> vertices;
    public List<int> triangles;
    public BlockMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }
}
