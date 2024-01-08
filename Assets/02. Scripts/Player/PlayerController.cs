using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float rollSpeed;
    private Vector2 _moveInput;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask GroundLayerMask;

    [Header("Attack")]
    [SerializeField]
    private GameObject chickenPrefab;
    [SerializeField]
    private GameObject chickenSpaawnPos;
    [SerializeField]
    private float attackSpeed;

    [HideInInspector]
    public PlayerInputState inputState;


    private bool _isGrounded;
    private bool _isElevator;
    private bool _isAttack;
    private bool _isRun;
    private float lastAttackTime = float.MaxValue;
    private Vector3 boxSize = new Vector3(0.6f, 0.1f, 0.6f);


    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform _mainCameraTransform;

    private const float GroundedOffset = -0.17f;
    private const float boxCastDistance = 0.2f;
    private const float elevatorBoxCastDistanceModifier = 0.2f;
    private const float RandBoxCastDistanceModifier = 1f;
    private const float RollAnimationtime = 1.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _mainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
        Cursor.lockState = CursorLockMode.Locked;
        inputState = PlayerInputState.UnLocked;
    }

    private void Update()
    {
        //CheckElevator();

    }

    private void FixedUpdate()
    {
        Move();
        Attack();
        CheckElevator();
        CheckAirAndGroundAnimation();
    }

    private void Move()
    {
        Vector3 dir = GetMoveDir();
        
        if (dir == Vector3.zero) return;
        Rotate(dir);
        var movementSpeed = _isRun ? runSpeed : moveSpeed;
        dir = dir * movementSpeed;
        //dir.y = _rigidbody.velocity.y;
        //_rigidbody.velocity = dir;
        _rigidbody.AddForce(dir);

    }

    private IEnumerator Roll()
    {
        _animator.SetTrigger("Roll");

        var dir = transform.forward;
        dir.y = 0;
        dir.Normalize();

        float timer = 0f;
        while (timer < RollAnimationtime)
        {
            //_rigidbody.MovePosition(_rigidbody.position + dir * rollSpeed * Time.fixedDeltaTime);
            _rigidbody.AddForce(dir * rollSpeed, ForceMode.VelocityChange);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Rotate(dir);
    }

    private void Attack()
    {
        lastAttackTime += Time.deltaTime;
        if (_isAttack && (lastAttackTime >= (1 / attackSpeed)))
        {
            // АјАн
            _animator.SetTrigger("Attack");
            GameManager.Instance.AudioManager.SFXPlay(("Bird"), gameObject.transform.position, 0.1f);
            Rotate(GetCamDir());
            Instantiate(chickenPrefab, chickenSpaawnPos.transform.position, Quaternion.identity);

            lastAttackTime = 0f;
        }
    }


    private void Rotate(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 GetMoveDir()
    {
        if(_moveInput == Vector2.zero) return Vector3.zero;

        var fowardDir = _mainCameraTransform.forward;
        var rightDir = _mainCameraTransform.right;

        fowardDir.y = 0f;
        rightDir.y = 0f;

        fowardDir.Normalize();
        rightDir.Normalize();

        return fowardDir * _moveInput.y + rightDir * _moveInput.x;

    }

    private Vector3 GetCamDir()
    {
        var fowardDir = _mainCameraTransform.forward;
        fowardDir.y = 0f;
        fowardDir.Normalize();
        return fowardDir;
    }

    #region InputAction
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (inputState == PlayerInputState.Locked) return;
        if(context.phase == InputActionPhase.Performed)
        {
            _moveInput = context.ReadValue<Vector2>();
            _animator.SetBool("Move", true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _moveInput = Vector2.zero;
            _animator.SetBool("Move", false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (inputState == PlayerInputState.Locked) return;
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                _animator.SetTrigger("Jump");
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (inputState == PlayerInputState.Locked) return;
        if (context.phase == InputActionPhase.Started)
        {
            _isAttack = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _isAttack = false;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (inputState == PlayerInputState.Locked) return;
        if (context.phase == InputActionPhase.Started)
        {
            _isRun = true;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            _isRun = false;
        }
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (inputState == PlayerInputState.Locked) return;
        if (context.phase == InputActionPhase.Started)
        {
            if(IsGrounded())
            {
                StartCoroutine(Roll());
            }
        }
    }

    #endregion

    public bool IsGrounded()
    {
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        _isGrounded = Physics.BoxCast(boxPosition, boxSize / 2, Vector3.down, Quaternion.identity, boxCastDistance, GroundLayerMask,
            QueryTriggerInteraction.Ignore);

        return _isGrounded;
    }

    private void CheckElevator()
    {
        RaycastHit hit;
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset ,
            transform.position.z);
        _isElevator = Physics.BoxCast(boxPosition, boxSize / 2, Vector3.down, out hit, Quaternion.identity, 
            boxCastDistance + elevatorBoxCastDistanceModifier + 0.2f, LayerMask.GetMask("Elevator", "Swing"),
            QueryTriggerInteraction.Ignore);


        if (_isElevator)
        {
            //if(_rigidbody.velocity.y < 0f)
                //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -.5f, _rigidbody.velocity.z);
            transform.parent = hit.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void CheckAirAndGroundAnimation()
    {
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
           transform.position.z);
        bool isRand = Physics.BoxCast(boxPosition, boxSize / 2, Vector3.down, Quaternion.identity, 
            boxCastDistance + RandBoxCastDistanceModifier, GroundLayerMask,
            QueryTriggerInteraction.Ignore);

        _animator.SetBool("Air", !isRand);
        _animator.SetBool("Grounded", isRand);
    }

    public void InputActionLocked()
    {
        inputState = PlayerInputState.Locked;
        _moveInput = Vector2.zero;
        _animator.SetBool("Move", false);
    }

    public void InputActionUnLocked()
    {
        inputState = PlayerInputState.UnLocked;
    }

    // test
    private void OnDrawGizmos()
    {
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(boxPosition, Vector3.down * boxCastDistance);

        Gizmos.color = Color.red;

        Vector3 gizmoBoxSize = new Vector3(0.6f, 0.1f, 0.6f);
        Gizmos.DrawWireCube(boxPosition + Vector3.down * boxCastDistance, gizmoBoxSize);
    }

}
