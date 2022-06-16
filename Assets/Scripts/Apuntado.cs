using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuntado : MonoBehaviour
{
    [Header("InputCamera")]
    public Camera camera;

    Vector3 reticlesPosition;
    public Vector3 ReticlesPosition
    {
        get { return reticlesPosition; }
    }

    private Vector3 reticlesNormal;
    public Vector3 ReticlesNormal
    {
        get { return reticlesNormal; }
    }

    private float forwardInput;
    [SerializeField] float ForwardInput
    {
        get { return forwardInput; }
    }

    private float rotationInput;
    [SerializeField]
    float RotationInput
    {
        get { return rotationInput; }
    }

    [Header("Reticles Properties")]
    public Transform reticleTransform;

    [Header("Turret Properties")]
    public Transform turretTransform;
    public float turretLagSpeed = 0.5f;

    private Vector3 finalTurretLookDir;

    private void Update()
    {
        if (camera) HandleInputs();

        HandleReticle();
        HandleTurret();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(reticlesPosition, 0.5f);
    }

    protected virtual void HandleInputs()
    {
        Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(screenRay, out hit))
        {
            reticlesPosition = hit.point;
            reticlesNormal = hit.normal;
        }

        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
    }

    void HandleTurret()
    {
        if (turretTransform)
        {
            Vector3 turretLookDir = ReticlesPosition - turretTransform.position;
            turretLookDir.y = 0f;

            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Time.deltaTime * turretLagSpeed);
            turretTransform.rotation = Quaternion.LookRotation(turretLookDir);
        }
    }

    void HandleReticle()
    {
        if (reticleTransform)
        {
            reticleTransform.position = ReticlesPosition;
        }
    }
}
