using System.Collections.Generic;
using UnityEngine;

public class PhysGun : MonoBehaviour
{
    [Header("PhysGun Properties")]
    [SerializeField]
    private float maxGrabDistance = 40f;
    [SerializeField]
    private float minGrabDistance = 1f;
    [SerializeField]
    private LineRenderer pickLine;

    private Transform barrelPoint;

    private Rigidbody grabbedObject;
    private float pickDistance;
    private Vector3 pickOffset;
    private Quaternion rotationOffset;
    private Vector3 pickTargetPosition;
    private Vector3 pickForce;

    private Camera playerCamera;

    private float rotSpeed = 5f;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    private void Start()
    {
        if (!barrelPoint)
            barrelPoint = transform;

        if (!pickLine)
        {
            var obj = new GameObject("PhysGun Pick Line");
            pickLine = obj.AddComponent<LineRenderer>();
            pickLine.startWidth = 0.02f;
            pickLine.endWidth = 0.02f;
            pickLine.useWorldSpace = true;
            pickLine.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        MouseLook mouseLook = playerCamera.GetComponent<MouseLook>();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Grab();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            if(grabbedObject)
                Release();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            if (grabbedObject)
                Release(true);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate((Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime), (Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime), 0, Space.World);
            mouseLook.enabled = false;
        }
        //else
            //mouseLook.enabled = true;

        pickDistance = Mathf.Clamp(pickDistance + Input.mouseScrollDelta.y, minGrabDistance, maxGrabDistance);
    }

    private void LateUpdate()
    {
        if (grabbedObject)
        {
            var midpoint = playerCamera.transform.position + playerCamera.transform.forward * pickDistance * .5f;
            DrawQuadraticBezierCurve(pickLine, barrelPoint.position, midpoint, grabbedObject.position + grabbedObject.transform.TransformVector(pickOffset));
        }
    }

    private void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            var ray = playerCamera.ViewportPointToRay(Vector3.one * .5f);
            pickTargetPosition = (ray.origin + ray.direction * pickDistance) - grabbedObject.transform.TransformVector(pickOffset);
            var forceDir = pickTargetPosition - grabbedObject.position;
            pickForce = forceDir / Time.fixedDeltaTime * 0.3f / grabbedObject.mass;
            grabbedObject.velocity = pickForce;
            grabbedObject.transform.rotation = playerCamera.transform.rotation * rotationOffset;

        }
    }

    private void Grab()
    {
        var ray = playerCamera.ViewportPointToRay(Vector3.one * .5f);
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance, layerMask: 1 << 0)
            && hit.rigidbody != null
            && !hit.rigidbody.CompareTag("Player"))
        {
            rotationOffset = Quaternion.Inverse(playerCamera.transform.rotation) * hit.rigidbody.rotation;
            pickOffset = hit.transform.InverseTransformVector(hit.point - hit.transform.position);
            pickDistance = hit.distance;
            grabbedObject = hit.rigidbody;
            grabbedObject.collisionDetectionMode = CollisionDetectionMode.Continuous;
            grabbedObject.useGravity = false;
            grabbedObject.freezeRotation = true;
            grabbedObject.isKinematic = false;
            pickLine.gameObject.SetActive(true);
        }
    }

    private void Release(bool freeze = false)
    {
        grabbedObject.collisionDetectionMode = CollisionDetectionMode.Discrete;
        grabbedObject.useGravity = true;
        grabbedObject.freezeRotation = false;
        grabbedObject.isKinematic = false;
        pickLine.gameObject.SetActive(false);

        if (freeze)
        {
            Freeze(grabbedObject);
        }
        else
        {
            Unfreeze(grabbedObject);
        }

        grabbedObject = null;
    }

    private Dictionary<Rigidbody, Rigidbody> _jointSwaps = new Dictionary<Rigidbody, Rigidbody>();
    private void Freeze(Rigidbody rb)
    {
        if (rb.TryGetComponent(out CharacterJoint characterJoint))
        {
            var fixedJointObject = GameObject.Instantiate(rb.gameObject, rb.transform.parent);
            var fixedJoint = fixedJointObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = characterJoint.connectedBody;
            fixedJoint.connectedAnchor = characterJoint.connectedAnchor;
            fixedJoint.massScale = characterJoint.massScale;
            fixedJoint.connectedMassScale = characterJoint.connectedMassScale;
            fixedJoint.GetComponent<Rigidbody>().isKinematic = true;
            _jointSwaps.Add(fixedJoint.GetComponent<Rigidbody>(), rb);

            rb.gameObject.SetActive(false);
        }
        rb.isKinematic = true;
    }

    private void Unfreeze(Rigidbody rb)
    {
        if (_jointSwaps.ContainsKey(rb))
        {
            _jointSwaps[rb].gameObject.SetActive(true);
            _jointSwaps[rb].isKinematic = false;
            _jointSwaps[rb].transform.localPosition = rb.transform.localPosition;
            _jointSwaps[rb].transform.localScale = rb.transform.localScale;
            _jointSwaps[rb].transform.localRotation = rb.transform.localRotation;
            _jointSwaps[rb].GetComponent<CharacterJoint>().connectedAnchor = rb.GetComponent<FixedJoint>().connectedAnchor;
            _jointSwaps[rb].GetComponent<CharacterJoint>().anchor = rb.GetComponent<FixedJoint>().anchor;
            GameObject.Destroy(rb.gameObject);
            _jointSwaps.Remove(rb);
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    // https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/
    void DrawQuadraticBezierCurve(LineRenderer line, Vector3 point0, Vector3 point1, Vector3 point2)
    {
        line.positionCount = 20;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < line.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            line.SetPosition(i, B);
            t += (1 / (float)line.positionCount);
        }
    }

}