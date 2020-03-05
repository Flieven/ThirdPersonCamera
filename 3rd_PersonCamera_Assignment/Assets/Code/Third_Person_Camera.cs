using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Camera : MonoBehaviour
{
    #region Variables

    private enum CameraTypes { LockedCamera, DetachedCamera, HybridCamera };

    #region Settings
    [Header("Settings")]

    [SerializeField] private GameObject ParentObject;

    [SerializeField] private float RotationSpeed = 50.0f;
    [SerializeField] private CameraTypes CameraType = CameraTypes.LockedCamera;
    [SerializeField] private bool InvertVertical = false;
    [SerializeField] private bool InvertHorizontal = false;
    #endregion

    #region Camera Settings
    [Header("Camera Settings")]

    [SerializeField] new Camera CameraObject;
    [SerializeField] private float MinimumRotationAngle = -80;
    [SerializeField] private float MaximumRotationAngle = 80;

    #endregion

    #region Boom Settings
    [Header("Boom Settings")]

    [SerializeField] private float MaximumBoomLength = 0.0f;
    [SerializeField] private float MinimumBoomLength = 0.0f;
    private float ZoomValue = 0.0f;
    private float MaximumZoomValue = 0.0f;
    [SerializeField] private float ZoomModifier = 5.0f;
    [SerializeField] private bool CanZoom = true;

    private Vector3 ActualBoomPos = Vector3.zero;

    #endregion

    #endregion

    #region Setup

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetupParent();
        SetupCamera();
    }

    private void SetupParent()
    {
        if(!ParentObject)
        {
            if(transform.parent) { ParentObject = transform.parent.gameObject; }
            else { Debug.LogError("No Parent Object to attach to!"); }
        }
    }

    private void SetupCamera()
    {
        if (!CameraObject)
        {
            gameObject.AddComponent(typeof(Camera));
            CameraObject = gameObject.GetComponent<Camera>();
        }

        ZoomValue = MaximumBoomLength;

    }

    #endregion

    private void Update()
    {
        LinetraceCameraBoom();
        CameraInputs();

        Vector3 RotAngle = ParentObject.transform.localEulerAngles;

        RotAngle.x = (RotAngle.x > 180) ? RotAngle.x-360 : RotAngle.x;
        RotAngle.x = Mathf.Clamp(RotAngle.x, MinimumRotationAngle, MaximumRotationAngle);

        ParentObject.transform.localRotation = Quaternion.Euler(RotAngle);
    }

    private void LinetraceCameraBoom()
    {
        Vector3 TargetPos = ParentObject.transform.position + (ParentObject.transform.forward * -1) * (MinimumBoomLength + ZoomValue);
        RaycastHit Hit;

        if(Physics.Linecast(ParentObject.transform.position, TargetPos, out Hit))
        {
            MaximumZoomValue = Hit.distance;
        }
        else
        {
            MaximumZoomValue = MaximumBoomLength;
        }

        ZoomValue = Mathf.Clamp(ZoomValue, MinimumBoomLength, MaximumZoomValue);

        ActualBoomPos = TargetPos;

        transform.position = ActualBoomPos;

        Debug.DrawLine(ParentObject.transform.position, ActualBoomPos);

    }

    #region Camera Inputs
    private void CameraInputs()
    {
        GetCameraRotationType();
        CameraZoom();
    }

    private void GetCameraRotationType()
    {
        switch(CameraType)
        {
            case CameraTypes.LockedCamera: RotateCamera(ParentObject.transform.parent.gameObject); break;

            case CameraTypes.DetachedCamera: RotateCamera(ParentObject); break;

            case CameraTypes.HybridCamera:
                if(Input.GetMouseButton(0)) { RotateCamera(ParentObject); }
                if(Input.GetMouseButtonUp(0)) { ParentObject.transform.localRotation = Quaternion.Euler(new Vector3 (0,0,0)); }
                else if (!Input.GetMouseButton(0)) { RotateCamera(ParentObject.transform.parent.gameObject); }
                break;
        }
    }

    private void CameraZoom()
    {
        ZoomValue -= (Input.GetAxis("Mouse ScrollWheel") * ZoomModifier);
    }

    private void RotateCamera(GameObject ParentToRotate)
    {
        if (Input.GetAxis("Mouse X") != 0) { ParentToRotate.transform.Rotate(0, (Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime) * (InverseMovement(InvertHorizontal) * -1), 0); }

        if (Input.GetAxis("Mouse Y") != 0) { ParentToRotate.transform.Rotate((Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime) * InverseMovement(InvertVertical), 0, 0); }

        float z = ParentToRotate.transform.eulerAngles.z;
        ParentToRotate.transform.Rotate(0, 0, -z);
    }

    private int InverseMovement(bool inverter)
    {
        return (inverter == true ? -1 : 1);
    }

    #endregion
}
