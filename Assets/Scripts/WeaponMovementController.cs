using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    �lockwise,
    �ounterclockwise
}

public class WeaponMovementController : MonoBehaviour
{
    [SerializeField] protected Transform ownerTransform;
    [SerializeField] protected Transform weaponModelTransform;
    [SerializeField] protected Transform rotationPoint;
    [SerializeField] protected Direction rotateDirection;
    protected Vector3 directionVector;
    [SerializeField] protected float rotationSpeed;

    private void Awake()
    {
        transform.SetParent(null);
    }
    private void Start()
    {

    }

    public void SetAntiDirection(Direction playerDirection)
    {
        if (playerDirection == Direction.�lockwise)
        {
            rotateDirection = Direction.�ounterclockwise;
            return;
        }
        else
        {
            rotateDirection = Direction.�lockwise;
        }       
    }
    void FixedUpdate()
    {
        if (rotateDirection == Direction.�lockwise)
        {
            directionVector = Vector3.up;
        }
        else directionVector = -Vector3.up;

        weaponModelTransform.RotateAround(rotationPoint.position, 
            directionVector, 
            rotationSpeed * Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
        transform.position = ownerTransform.position;
    }
    public void SetRotationSpeed(float rotateSpeed)
    {
        this.rotationSpeed = rotateSpeed;
    }
    public void SwapWeaponDirection()
    {
        if (rotateDirection == Direction.�lockwise)
            rotateDirection = Direction.�ounterclockwise;
        else rotateDirection = Direction.�lockwise;
      
        weaponModelTransform.transform.Rotate(180, weaponModelTransform.transform.rotation.y, 0);
    }
    public Direction GetRotationDirection()
    { 
        return rotateDirection;
    }
}

