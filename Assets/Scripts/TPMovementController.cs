using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.VR;

public class TPMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;    
    [SerializeField] private Transform targetCamera;
    [SerializeField] private bool isBlocked = false;
    [SerializeField] private float movWalkSpeed = 5f;
    [SerializeField] private float movSprintSpeed = 7f;
    [Header("Modifies all speeds (x*=modifier)")]
    [SerializeField] private float movSpeedModifier = 1f;
    [Header("How fast object's angle changes")]
    [SerializeField] private float turnSmoothTime = 0.1f;


    private float turnSmoothVelocity;
    private bool _hasCharacterController = false;
    private Vector3 movDirection = Vector3.zero;

    private void Awake()
    {
        if (characterController)
            _hasCharacterController = true;
        else
            _hasCharacterController = gameObject.TryGetComponent(out characterController);

        if (!targetCamera)
            targetCamera = Camera.main.transform;

        if (!_hasCharacterController || !targetCamera)
        {
            Debug.LogError("Warning! CharacterController/Camera is (are) not attached to GameObject"); 
            Debug.Break();
        }
    }

    private bool DoesMove => movDirection.magnitude >= 0.1f;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 movInputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        movDirection = movInputRaw.normalized;

        if (DoesMove && !isBlocked)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                MoveCharacter(movDirection, movWalkSpeed,movSpeedModifier);
            else
                MoveCharacter(movDirection, movSprintSpeed,movSpeedModifier);
        }

    }
    private void MoveCharacter(Vector3 direction, float speed, float speedModifier = 1)
    {
        //Получение угла направления(0:360) по его Tan: Tan d = (направление по x / по y) + угол камеры
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + targetCamera.eulerAngles.y;
        //Сглаживание угла направления
        float smoothedAngle = Mathf.SmoothDampAngle(characterController.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        characterController.transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
        
        //Перемещение относительно угла камеры (при умножении на V3(1,1,1) угол преобразуется в направление)
        Vector3 movCamDirection = Quaternion.Euler(0f, smoothedAngle, 0f) * Vector3.forward;
        characterController.Move(movCamDirection.normalized * speed * speedModifier * Time.deltaTime);
    }

}
