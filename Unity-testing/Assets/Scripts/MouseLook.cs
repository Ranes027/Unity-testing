using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    [Header("Mouse Sensitivity")]
    [SerializeField] private float _horizontalSensitivity = 100f;
    [SerializeField] private float _verticalSensitivity = 150f;
    [Header("Camera Settings")]
    [SerializeField] private float _minVerticalRotation = -90f;
    [SerializeField] private float _maxVerticalRotation = 90f;

    private float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _verticalSensitivity * Time.deltaTime;

        _playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minVerticalRotation, _maxVerticalRotation);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
