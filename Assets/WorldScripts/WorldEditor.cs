using System;
using UnityEngine;

public class WorldEditor : MonoBehaviour
{
    public Transform CameraTransform;
    public GameObject prefab;
    public float maxRayLength = 10f;
    public float returnSpeed = 1f;

    private float currentRayLength;

    void Start()
    {
        currentRayLength = maxRayLength;
    }

    void Update()
    {
        ProcessRay();
    }

    private void ProcessRay()
    {
        Ray ray = new Ray(CameraTransform.position, CameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayLength))
        {
            currentRayLength = hit.distance;

            if (Input.GetMouseButtonDown(0))
            {
                CreateBlock((Vector3)(Vector3Int.FloorToInt(hit.point) + Vector3Int.CeilToInt(hit.point)) / 2 + hit.normal*0.5f);
            }
            if (Input.GetMouseButtonDown(1))
            {
                DeleteBlock();
            }
        }
        else
        {
            currentRayLength = Mathf.Lerp(currentRayLength, maxRayLength, Time.deltaTime * returnSpeed);
        }

        Debug.DrawRay(ray.origin, ray.direction * currentRayLength, Color.red);
    }

    private void DeleteBlock()
    {
        throw new NotImplementedException();
    }

    private void CreateBlock(Vector3 pos)
    {
        Debug.Log("Блок установлен в точке: " + pos);
        var obj = Instantiate(prefab, pos, Quaternion.identity);
        obj.transform.localScale = Vector3.one * 50;
    }
}