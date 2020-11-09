using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.VR;


public class TPMovementController : MonoBehaviour
{
    [Header("Essential Components")]
    [SerializeField] private CharacterController charController;    
    [SerializeField] private Transform charCamera;
    [Space]
    [SerializeField] private bool MovementBlocked = false;
    [SerializeField] private float movWalkSpeed = 5f;
    [SerializeField] private float movSprintSpeed = 7f;
    [Tooltip("Modifies all speeds")]
    [SerializeField] private float movSpeedModifier = 1f;
    [Tooltip("How fast object's angle changes")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    
    
    private float turnSmoothVelocity;
    private Vector3 movDirection;

    

    private Vector3 MovementInputRaw => new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    private bool DoesMove => !MovementBlocked && movDirection.magnitude >= 0.1f;

    private void Update()
    {
        ChangeCursorState(CursorLockMode.Locked, false);

        movDirection = MovementInputRaw.normalized;
        if (DoesMove)
        {
            if (!IsPressed(KeyCode.LeftShift))
                MoveCharacter(movDirection, movWalkSpeed, movSpeedModifier);
            else
                MoveCharacter(movDirection, movSprintSpeed, movSpeedModifier);
        }
    }

    private void MoveCharacter(Vector3 direction, float speed, float speedModifier = 1)
    {
        //Получение угла направления(0:360) по его Tan: Tan d = (направление по x / по y) + угол камеры
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + charCamera.eulerAngles.y;
        //Сглаживание угла направления
        float smoothedAngle = Mathf.SmoothDampAngle(charController.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        charController.transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
        
        //Перемещение относительно угла камеры (при умножении на V3(1,1,1) угол преобразуется в направление)
        Vector3 movCamDirection = Quaternion.Euler(0f, smoothedAngle, 0f) * Vector3.forward;
        charController.Move(movCamDirection.normalized * (speed * speedModifier * Time.deltaTime));       
    }
    public void ChangeCursorState(CursorLockMode lockMode, bool visibility)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = visibility;
    }
    public bool IsPressed(KeyCode key)
    {
        if (MovementBlocked)
            return false;
        return Input.GetKey(key);
    }
    public bool IsPressedDown(KeyCode key)
    {
        if (MovementBlocked)
            return false;
        return Input.GetKeyDown(key);
    }

}
