using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Ñlockwise,
    Ñounterclockwise
}

public class WeaponMovementController : MonoBehaviour
{
    [SerializeField] protected Transform ownerTransform;
    [SerializeField] protected Transform weaponModelTransform;
    [SerializeField] protected Transform rotationPoint;
    [SerializeField] protected Direction rotateDirection;
    protected Vector3 direction;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] float speed;
    private void Awake()
    {
        transform.SetParent(null);
    }
    private void Start()
    {
        direction = weaponModelTransform.up;
        if (rotateDirection == Direction.Ñounterclockwise)
            direction = -direction;
    }
    void FixedUpdate()
    {       
        weaponModelTransform.RotateAround(rotationPoint.position, direction, speed);
    }
    private void LateUpdate()
    {
        transform.position = ownerTransform.position;

        //if (Input.GetMouseButtonDown(1))
        //    SwapWeaponDirection();
    }
    public void SetRotationSpeed(float rotateSpeed)
    {
        rotationSpeed = rotateSpeed;
        speed = rotationSpeed * Time.fixedDeltaTime;
    }
    public void SwapWeaponDirection()
    {
        direction = -direction;
        weaponModelTransform.transform.Rotate(180, weaponModelTransform.transform.rotation.y, 0);
    }
}

