using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Teleporter _other;

    private void OnTriggerStay(Collider other)
    {
        float zPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPosition < 0)
            Teleport(other.transform);
    }

    private void Teleport(Transform target)
    {
        Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(target.position);
        localPosition = new Vector3(-localPosition.x, localPosition.y, -localPosition.z);
        target.position = _other.transform.localToWorldMatrix.MultiplyPoint3x4(localPosition);

        Quaternion difference = _other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        target.rotation = difference * target.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = 9;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 8;
    }
}
