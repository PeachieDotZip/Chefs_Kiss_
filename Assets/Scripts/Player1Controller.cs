using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
// Add in this using
public class Player1Controller : MonoBehaviour
{
    // reference to the input action you created
    GamplayActions controls;
    //variables to store data returned by the ReadValue function
    Vector2 move;
    Vector2 rotate;

    //Player1-specific variables
    private Transform holdPoint;
    private bool canGrab;
    public bool isHolding;
    public GameObject holdTarget;
    public bool grabAttempt;
    public bool dropAttempt;

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
        controls.Gameplay.Grab.performed += ctx => Grab();
        controls.Gameplay.Drop.performed += ctx => dropAttempt = true;
        controls.Gameplay.Drop.performed += ctx => Drop();
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
        //controls.Gameplay.Walk.performed += ctx => rotate =
        //ctx.ReadValue<Vector2>();
        //controls.Gameplay.Walk.canceled += ctx => rotate = Vector2.zero;

    }
    private void FixedUpdate()
    {
        // Here, we take the saved move value and manipulate it to show up in UNITY
        // We multiply it by Time.deltaTime to keep it independent of frame rate
        // Then we apply this manipulated value to the Translate function on
        // the transform of the object
        Vector2 moveVelocity = new Vector2(move.x, move.y) * 5f * Time.deltaTime;
        transform.Translate(moveVelocity, Space.Self);
        // Manipulate the saved value and apply it to the Rotate function of the
        // object
        Vector2 rotateVelocity = new Vector2(rotate.x, rotate.y) * 100f *
        Time.deltaTime;
        transform.Rotate(rotateVelocity, Space.World);

        if (isHolding)
        {
            holdTarget.transform.position = holdPoint.position;


        }
    }
    void Grow()
    {
        // Increasing the scale of the object
        transform.localScale *= 1.1f;

    }

    IEnumerator Grab()
    {
        if (canGrab == true)
        {
            isHolding = true;
        }
        yield return new WaitForSeconds(0.1f);
        grabAttempt = false;

    }
    IEnumerator Drop()
    {
        yield return new WaitForSeconds(0.1f);
        dropAttempt = false;
        isHolding = false;
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