using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _other;
    [SerializeField] private Camera _portalView;

    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        _other._portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = _other._portalView.targetTexture;
    }

    private void Update()
    {
        Vector3 watcherPosition = _other.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        watcherPosition = new Vector3(-watcherPosition.x, watcherPosition.y, -watcherPosition.z);
        _portalView.transform.localPosition = watcherPosition;

        Quaternion difference = transform.rotation * Quaternion.Inverse(_other.transform.rotation * Quaternion.Euler(0, 180, 0));
        _portalView.transform.rotation = difference * Camera.main.transform.rotation;

        _portalView.nearClipPlane = watcherPosition.magnitude;
    }
}
