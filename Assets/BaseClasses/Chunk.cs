using UnityEngine;

[RequireComponent (typeof(MeshFilter)), RequireComponent (typeof(MeshRenderer)), RequireComponent (typeof(MeshCollider))]
public class Chunk : MonoBehaviour
{
    public int id;
    public int x;
    public int z;
}
