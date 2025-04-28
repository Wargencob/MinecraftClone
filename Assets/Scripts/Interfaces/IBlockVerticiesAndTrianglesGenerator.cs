using UnityEngine;
using System.Collections.Generic;

public interface IBlockVerticesAndTrianglesGenerator
{
    public void GenerateVerticesAndTringles(List<Vector3> vertices, List<int> triangles);
}