/*****************************************************************************
// File Name :         Player1Controller.cs
// Author :            John H. Weber
// Creation Date :     Apr 5th, 2023
//
// Brief Description : Controls all variables and code for movement and inputs, as well as applying the correct rotation for its direction.
*****************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class Player1Controller : MonoBehaviour
{
    // reference to the input action you created
    GamplayActions controls;
    //variables to store data returned by the ReadValue function
    [SerializeField]
    Vector2 move;
    [SerializeField]
    Vector2 rotate;

    //Player1-specific variables
    private Transform holdPoint;
    private bool canGrab;
    public bool isHolding;
    public GameObject holdTarget;
    public bool grabAttempt;
    public bool dropAttempt;
    private Rigidbody2D rb;
    public GameObject sprite;

    /// <summary>
    /// Awake is called even before Start()
    /// Instantiates the input action reference
    /// Gets values from the system
    /// Create actions to perform when a button is triggered
    /// </summary>
    private void Awake()
    {
        holdTarget = null;
        holdPoint = GameObject.Find("HoldPoint").transform;
        isHolding = false;
        rb = GetComponent<Rigidbody2D>();

        //initializing the reference to player controls map
        controls = new GamplayActions();
        // In the following code line:
        // control = reference to player controls Input Actions
        // Gameplay = Action Map that you created
        // Grow = Action in the action map
        //
        // Three events can be associated with actions
        // - .started -> when the action is started
        // - .performed -> when the action is performed (actually happens)
        // - .cancelled -> when the action should stop
        //
        // ctx = data that is returned from UNITY (usually position value) when you
        //call the event
        //
        // So the following line means when we want the object to grow
        // add to the performed events (that's why we use +=),
        // then go to the Grow() function
        // In this case we have no need for position values so ctx is not saved
        controls.Gameplay.Grow.performed += ctx => Grow();
        controls.Gameplay.Grab.performed += ctx => grabAttempt = true;
        controls.Gameplay.Grab.performed += ctx => StartCoroutine(Grab());
        controls.Gameplay.Drop.performed += ctx => dropAttempt = true;
        controls.Gameplay.Drop.performed += ctx => StartCoroutine(Drop());
        // In the following pieces of code, we need to use the data that
        // UNITY returns to actually move/rotate the object
        //
        // Here we create a variable 'move' to store the data that ctx returns
        // To actually get the value, we use the ReadValue function and ask for the
        // data to be returned as a Vector2
        //
        // We then use this saved value in the FixedUpdate function to manipulate
        // the object
        controls.Gameplay.Walk.performed += ctx => move = ctx.ReadValue<Vector2>();
    // We don't want the object to move if we are not pressing the
    //button/thumbstick
        // So we reset the move value to zero and attach it to the .cancelled event
        controls.Gameplay.Walk.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;

    }
    /// <summary>
    /// Part of the script where the moveVelocity is recorded. Also applies correct rotation for directional movement.
    /// </summary>
    private void FixedUpdate()
    {
        // Here, we take the saved move value and manipulate it to show up in UNITY
        // We multiply it by Time.deltaTime to keep it independent of frame rate
        // Then we apply this manipulated value to the Translate function on
        // the transform of the object
        Vector2 moveVelocity = new Vector2(move.x, move.y) * 7.5f * Time.deltaTime;
        transform.Translate(moveVelocity, Space.Self);
        // Manipulate the saved value and apply it to the Rotate function of the
        // object

        //This part of the script changes the rotation of the main object to account for direction.
        if (rotate.x == -1)
        {
            sprite.transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        if (rotate.x == 1)
        {
            sprite.transform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        if (rotate.y < 0)
        {
            sprite.transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        if (rotate.y > 0.707 && rotate.y < 1.35)
        {
            sprite.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (rotate.x == 0 && rotate.y == 0)
        {
            sprite.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        //Everything that happens while holding an object.
        if (isHolding)
        {
            holdTarget.transform.position = holdPoint.position;
            //^^^ Keeps the grabbed object at the hold point.

        }

    }
    /// <summary>
    /// Debug function. Tests input. Simply grows object.
    /// </summary>
    void Grow()
    {
        // Increasing the scale of the object
        transform.localScale *= 1.1f;

    }
    /// <summary>
    /// Player has pressed the "grab" button. If near an object, the player will begin holding it.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Grab()
    {
        if (canGrab == true)
        {
            isHolding = true;
        }
        yield return new WaitForSeconds(0.1f);
        grabAttempt = false;

    }
    /// <summary>
    /// Player has pressed the "drop" button. If holding an object, the player will drop it.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Drop()
    {
        yield return new WaitForSeconds(0.1f);
        dropAttempt = false;
        isHolding = false;
        holdTarget = null;
    }
    /// <summary>
    /// Should always be included
    /// Used to activate the action map being used
    /// This function is called when you start gameplay
    /// </summary>
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    /// <summary>
    /// Should always be included
    /// Used to deactivate the action map being used
    /// This function is called when you exit gameplay
    /// </summary>
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
