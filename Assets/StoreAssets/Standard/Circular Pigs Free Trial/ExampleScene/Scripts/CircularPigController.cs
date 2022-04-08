using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPigController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloorChecker floorChecker;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [Header("Move Properties")]
    public float moveSpeed;
    public float runSpeed;
    public float turnSpeed;
    public float jumpHeight;

    [Header("Key Binding")]
    [SerializeField] private KeyCode moveUpKey;
    [SerializeField] private KeyCode moveDownKey;
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode runKey;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode kickKey;
    [SerializeField] private KeyCode danceKey;
    [SerializeField] private KeyCode dieKey;
    [SerializeField] private KeyCode modeChangeKey;

    private const int runningState_Idle = 0;
    private const int runningState_Walk = 1;
    private const int runningState_Run = 2;
    private readonly Vector3 legModeColliderCenter = new Vector3(0f, -0.09f, 0f);
    private readonly Vector3 ballModeColliderCenter = new Vector3(0f, 0f, 0f);
    private const float legModeHeight = 0.48f;
    private const float ballModeHeight = 0f;

    private Vector3 moveVector;
    private bool dead = false;
    private bool actionLock = false;
    private bool legMode = true;
    private bool jumpKeyDown = false;

    //String Variables
    private const string animParam_jumpTrigger = "Jump Trigger";
    private const string animParam_legMode = "Leg Mode";
    private const string animParam_modeTransformTrigger = "Mode Transform Trigger";
    private const string animParam_dance = "Dance";
    private const string animParam_kickTrigger = "Kick Trigger";
    private const string animParam_dead = "Dead";
    private const string animParam_runningState = "Running State";


    WaitForSeconds actionLockDelay;

    private void Start()
    {
        actionLockDelay = new WaitForSeconds(0.01f);
    }

    void Update()
    {
        CheckDeadKeyDown();

        if (!dead && !actionLock)
        {
            CheckKickKeyDown();
            CheckDanceKeyDown();
            CheckJumpKeyDown();
            CheckModeChangeKeyDown();
            CheckMovementKeys();
            SetCharacterForward();
            SetRunningState();
        }
    }

    private void FixedUpdate()
    {
        if (!actionLock && !dead)
        {
            //Set Velocity
            moveVector *= Time.fixedDeltaTime;
            moveVector.y = rb.velocity.y;
            rb.velocity = moveVector;
        }
        if(jumpKeyDown)
        {
            //Add Jump Force
            rb.AddForce(Vector3.up * jumpHeight);
            animator.SetTrigger(animParam_jumpTrigger);
            jumpKeyDown = false;
        }
    }

    #region Private Methods

    private void CheckModeChangeKeyDown()
    {
        if (Input.GetKeyDown(modeChangeKey))
        {
            legMode = !legMode;
            animator.SetBool(animParam_legMode, legMode);
            animator.SetTrigger(animParam_modeTransformTrigger);
            if(legMode)
            {
                capsuleCollider.height = legModeHeight;
                capsuleCollider.center = legModeColliderCenter;
            }
            else
            {
                capsuleCollider.height = ballModeHeight;
                capsuleCollider.center = ballModeColliderCenter;
            }
        }
    }

    private void CheckDanceKeyDown()
    {
        if (Input.GetKeyDown(danceKey) && legMode)
        {
            animator.SetBool(animParam_dance, true);
        }
        else if (Input.anyKeyDown)
        {
            animator.SetBool(animParam_dance, false);
        }
    }

    private void CheckKickKeyDown()
    {
        if (Input.GetKeyDown(kickKey) && legMode)
        {
            animator.SetTrigger(animParam_kickTrigger);
            rb.velocity = Vector3.zero;
            actionLock = true;
            StartCoroutine(WaitForCurrentAnimationFinish());
        }
    }

    IEnumerator WaitForCurrentAnimationFinish()
    {
        yield return actionLockDelay;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
        actionLock = false;
    }

    private void CheckJumpKeyDown()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            if (floorChecker.FloorExists)
                jumpKeyDown = true;
        }
    }

    private void CheckDeadKeyDown()
    {
        if (Input.GetKeyDown(dieKey))
        {
            if (dead)
            {
                dead = false;
                animator.SetBool(animParam_dead, false);
            }
            else
            {
                dead = true;
                animator.SetBool(animParam_dead, true);
            }
        }
    }
    private void CheckMovementKeys()
    {
        moveVector = Vector3.zero;

        if (actionLock || dead) return;

        if (Input.GetKey(runKey))
        {
            if (Input.GetKey(moveUpKey))
            {
                moveVector += Vector3.forward * runSpeed;
            }
            if (Input.GetKey(moveDownKey))
            {
                moveVector += Vector3.back * runSpeed;
            }
            if (Input.GetKey(moveLeftKey))
            {
                moveVector += Vector3.left * runSpeed;
            }
            if (Input.GetKey(moveRightKey))
            {
                moveVector += Vector3.right * runSpeed;
            }
        }
        else
        {
            if (Input.GetKey(moveUpKey))
            {
                moveVector += Vector3.forward * moveSpeed;
            }
            if (Input.GetKey(moveDownKey))
            {
                moveVector += Vector3.back * moveSpeed;
            }
            if (Input.GetKey(moveLeftKey))
            {
                moveVector += Vector3.left * moveSpeed;
            }
            if (Input.GetKey(moveRightKey))
            {
                moveVector += Vector3.right * moveSpeed;
            }
        }
        
    }

    private void SetCharacterForward()
    {
        if (moveVector == Vector3.zero) return;

        if(Vector3.Dot(transform.forward, moveVector.normalized) < -0.9f)
        {
            transform.forward = Vector3.Lerp(transform.forward, moveVector.normalized, turnSpeed * 20f * Time.deltaTime);
        }
        else
        {
            transform.forward = Vector3.Lerp(transform.forward, moveVector.normalized, turnSpeed * Time.deltaTime);
        }
    }

    private void SetRunningState()
    {
        if (moveVector != Vector3.zero)
        {
            if (Input.GetKey(runKey))
                animator.SetInteger(animParam_runningState, runningState_Run);
            else
                animator.SetInteger(animParam_runningState, runningState_Walk);
        }
        else
        {
            animator.SetInteger(animParam_runningState, runningState_Idle);
        }
    }

    #endregion
}
