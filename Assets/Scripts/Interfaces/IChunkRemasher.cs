using UnityEngine;

public interface IChunkRemasher : IMeshEditor
{
    void AddBlockToChunk(RaycastHit hit);
    void RemoveBlockFromChunk(RaycastHit hit);
}

