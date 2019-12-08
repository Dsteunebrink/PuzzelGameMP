using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Contains the command the user wishes upon the character
struct Cmd {
    public float forwardMove;
    public float rightMove;
    public float upMove;
}

public class RigidbodyFirstPersonController : MonoBehaviour {
    public Transform playerView;     // Camera
    public float playerViewYOffset = 0.6f; // The height at which the camera is bound to
    public float xMouseSensitivity = 30.0f;
    public float yMouseSensitivity = 30.0f;

    private PushCubeBorder pushCubeBorder; // Check the direction for the push cube with an border
    private Vector3 RevertPushCubeBorderPosZ; // Revert the position of the block to this pos
    private Vector3 RevertPushCubeBorderPosX; // Revert the position of the block to this pos

    public float pushPower = 2.0F; // The power wich the player is pushing the blocks
    public float pullPower = 2.0F; // The power wich the player is pulling the blocks

    //
    /*Frame occuring factors*/
    public float gravity = 20.0f;

    private float m_StepCycle; // cycle of steps
    private float m_NextStep; // the next step

    [SerializeField] private bool m_IsWalking; // bool to check if player is walking
    [SerializeField] [Range (0f, 1f)] private float m_RunstepLenghten; // the length of a step
    [SerializeField] private float m_StepInterval; // the interval between steps

    public float friction = 6; //Ground friction

    [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from
    [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground
    [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground

    private AudioSource m_AudioSource;

    /* Movement stuff */
    [SerializeField] private float moveSpeed = 7.0f;                // Ground move speed
    [SerializeField] private float sprintSpeed = 13.0f;             // Ground sprint speed
    [SerializeField] private float runAcceleration = 14.0f;         // Ground accel
    [SerializeField] private float runDeacceleration = 10.0f;       // Deacceleration that occurs when running on the ground
    [SerializeField] private float airAcceleration = 2.0f;          // Air accel
    [SerializeField] private float airDecceleration = 2.0f;         // Deacceleration experienced when ooposite strafing
    [SerializeField] private float airControl = 0.3f;               // How precise air control is
    [SerializeField] private float sideStrafeAcceleration = 50.0f;  // How fast acceleration occurs to get up to sideStrafeSpeed when
    [SerializeField] private float sideStrafeSpeed = 1.0f;          // What the max speed to generate when side strafing
    [SerializeField] private float jumpSpeed = 8.0f;                // The speed at which the character's up axis gains when hitting jump
    [SerializeField] private bool holdJumpToBhop = false;           // When enabled allows player to just hold jump button to keep on bhopping perfectly. Beware: smells like casual.

    /*print() style */
    public GUIStyle style;

    /*FPS Stuff */
    public float fpsDisplayRate = 4.0f; // 4 updates per sec

    private int frameCount = 0;
    private float dt = 0.0f;
    private float fps = 0.0f;

    private CharacterController _controller;

    // Camera rotations, Stop camera
    private float rotX = 0.0f;
    private float rotY = 0.0f;
    public bool stopCamera = false;

    private Vector3 moveDirectionNorm = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private float playerTopVelocity = 0.0f;

    // Q3: players can queue the next jump just before he hits the ground
    private bool wishJump = false;

    // Used to display real time fricton values
    private float playerFriction = 0.0f;

    // Player commands, stores wish commands that the player asks for (Forward, back, jump, etc)
    private Cmd _cmd;

    private bool landed; // bool for when player has landed

    private void Start () {
        // Make sure when scene start the time is on the audio is on and the camera is moving
        Time.timeScale = 1;
        AudioListener.volume = 1;
        stopCamera = false;

        Physics.IgnoreLayerCollision (0, 9);

        RevertPushCubeBorderPosZ = new Vector3 (0,0,0.1f);
        RevertPushCubeBorderPosX = new Vector3 (0.1f, 0, 0);

        if (SceneManager.GetActiveScene().name != "Level_1") {
            // Find the object of the cube in the scene
            pushCubeBorder = GameObject.Find ("PushCubeBorders").GetComponent<PushCubeBorder> ();
        }

        // Set how the interval between steps
        m_StepInterval = 5f;

        // Get audio source on player
        m_AudioSource = GetComponent<AudioSource> ();

        // Set step cycle and the next step
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;

        // Hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerView == null) {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
                playerView = mainCamera.gameObject.transform;
        }

        // Put the camera inside the capsule collider
        playerView.position = new Vector3 (
            transform.position.x,
            transform.position.y + playerViewYOffset,
            transform.position.z);

        _controller = GetComponent<CharacterController> ();
    }

    private void Update () {
        // Do FPS calculation
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / fpsDisplayRate) {
            fps = Mathf.Round (frameCount / dt);
            frameCount = 0;
            dt -= 1.0f / fpsDisplayRate;
        }
        /* Ensure that the cursor is locked into the screen 
        if (Cursor.lockState != CursorLockMode.Locked) {
            if (Input.GetButtonDown ("Fire1"))
                Cursor.lockState = CursorLockMode.Locked;
        }*/

        /* Camera rotation, mouse controls */
        if (stopCamera == false) {
            rotX -= Input.GetAxisRaw ("Mouse Y") * xMouseSensitivity * 0.02f;
            rotY += Input.GetAxisRaw ("Mouse X") * yMouseSensitivity * 0.02f;
        }

        // Clamp the X rotation
        if (rotX < -90)
            rotX = -90;
        else if (rotX > 90)
            rotX = 90;

        this.transform.rotation = Quaternion.Euler (0, rotY, 0); // Rotates the collider
        playerView.rotation = Quaternion.Euler (rotX, rotY, 0); // Rotates the camera

        if (_controller.isGrounded) {
            PlayLandingSound (); 
        } else if (!_controller.isGrounded) {
            landed = true;
        }

        /* Movement, here's the important part */
        QueueJump ();
        if (_controller.isGrounded) {
            if (Input.GetKey (KeyCode.LeftShift)) {
                m_StepInterval = 2f;
                m_IsWalking = false;
            } else {
                m_StepInterval = 5f;
                m_IsWalking = true;
            }
            GroundMove ();
        } else if (!_controller.isGrounded) {
            m_IsWalking = false;
            AirMove ();
        }


        // Move the controller
        _controller.Move (playerVelocity * Time.deltaTime);

        /* Calculate top velocity */
        Vector3 udp = playerVelocity;
        udp.y = 0.0f;
        if (udp.magnitude > playerTopVelocity)
            playerTopVelocity = udp.magnitude;

        //Need to move the camera after the player has been moved because otherwise the camera will clip the player if going fast enough and will always be 1 frame behind.
        // Set the camera's position to the transform
        playerView.position = new Vector3 (
            transform.position.x,
            transform.position.y + playerViewYOffset,
            transform.position.z);
    }

    /*******************************************************************************************************\
   |* MOVEMENT
   \*******************************************************************************************************/

    /**
     * Sets the movement direction based on player input
     */
    private void SetMovementDir () {
        _cmd.forwardMove = Input.GetAxisRaw ("Vertical");
        _cmd.rightMove = Input.GetAxisRaw ("Horizontal");
    }

    /**
     * Queues the next jump just like in Q3
     */
    private void QueueJump () {
        if (holdJumpToBhop) {
            wishJump = Input.GetButton ("Jump");
            return;
        }
        
        if (Input.GetButtonDown ("Jump") && !wishJump) {
            wishJump = true;
            
        }
        if (Input.GetButtonUp ("Jump")) {
            wishJump = false;
        }
            
    }
    
    private void PlayLandingSound () {
        if (landed == true) {
            //m_AudioSource.clip = m_LandSound;
            m_AudioSource.PlayOneShot (m_LandSound);
            m_NextStep = m_StepCycle + .5f;
            landed = false;
        }
    }

    private void PlayFootStepAudio () {
        if (!_controller.isGrounded)
        {
            return;
        }else if (landed == true) {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range (1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot (m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }

    /**
     * Execs when the player is in the air
    */
    private void AirMove () {
        Vector3 wishdir;
        float wishvel = airAcceleration;
        float accel;

        if (Input.GetKey (KeyCode.S)) {

            //playerVelocity.y = 0;
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        } else {

            SetMovementDir ();

            wishdir = new Vector3 (_cmd.rightMove, 0, _cmd.forwardMove);

            wishdir = transform.TransformDirection (wishdir);

            float wishspeed = wishdir.magnitude;

            if (Input.GetKey (KeyCode.LeftShift)) {
                wishspeed *= sprintSpeed;
            } else {
                wishspeed *= moveSpeed;
            }

            wishdir.Normalize ();
            moveDirectionNorm = wishdir;

            // CPM: Aircontrol
            float wishspeed2 = wishspeed;
            if (Vector3.Dot (playerVelocity, wishdir) < 0)
                accel = airDecceleration;
            else
                accel = airAcceleration;
            // If the player is ONLY strafing left or right
            if (_cmd.forwardMove == 0 && _cmd.rightMove != 0) {
                if (wishspeed > sideStrafeSpeed)
                    wishspeed = sideStrafeSpeed;
                accel = sideStrafeAcceleration;
            }

            Accelerate (wishdir, wishspeed, accel);
            if (airControl > 0)
                AirControl (wishdir, wishspeed2);
            // !CPM: Aircontrol

            // Apply gravity
            playerVelocity.y -= gravity * Time.deltaTime;
        }
    }

    /**
     * Air control occurs when the player is in the air, it allows
     * players to move side to side much faster rather than being
     * 'sluggish' when it comes to cornering.
     */
    private void AirControl (Vector3 wishdir, float wishspeed) {
        float zspeed;
        float speed;
        float dot;
        float k;

        // Can't control movement if not moving forward or backward
        if (Mathf.Abs (_cmd.forwardMove) < 0.001 || Mathf.Abs (wishspeed) < 0.001)
            return;
        zspeed = playerVelocity.y;
        playerVelocity.y = 0;
        /* Next two lines are equivalent to idTech's VectorNormalize() */
        speed = playerVelocity.magnitude;
        playerVelocity.Normalize ();

        dot = Vector3.Dot (playerVelocity, wishdir);
        k = 32;
        k *= airControl * dot * dot * Time.deltaTime;

        // Change direction while slowing down
        if (dot > 0) {
            playerVelocity.x = playerVelocity.x * speed + wishdir.x * k;
            playerVelocity.y = playerVelocity.y * speed + wishdir.y * k;
            playerVelocity.z = playerVelocity.z * speed + wishdir.z * k;

            playerVelocity.Normalize ();
            moveDirectionNorm = playerVelocity;
        }

        playerVelocity.x *= speed;
        playerVelocity.y = zspeed; // Note this line
        playerVelocity.z *= speed;
    }

    /**
     * Called every frame when the engine detects that the player is on the ground
     */
    private void GroundMove () {
        Vector3 wishdir;

        // Do not apply friction if the player is queueing up the next jump
        if (!wishJump)
            ApplyFriction (1.0f);
        else
            ApplyFriction (0);

        SetMovementDir ();

        wishdir = new Vector3 (_cmd.rightMove, 0, _cmd.forwardMove);
        wishdir = transform.TransformDirection (wishdir);
        wishdir.Normalize ();
        moveDirectionNorm = wishdir;

        var wishspeed = wishdir.magnitude;

        if (Input.GetKey (KeyCode.LeftShift)) {
            wishspeed *= sprintSpeed;
        } else {
            wishspeed *= moveSpeed;
        }

        if (Input.GetKey (KeyCode.W)) {
            ProgressStepCycle (wishspeed);
        } else if (Input.GetKey (KeyCode.A)) {
            ProgressStepCycle (wishspeed);
        } else if (Input.GetKey (KeyCode.S)) {
            ProgressStepCycle (wishspeed);
        } else if (Input.GetKey (KeyCode.D)) {
            ProgressStepCycle (wishspeed);
        }

        Accelerate (wishdir, wishspeed, runAcceleration);

        // Reset the gravity velocity
        playerVelocity.y = -gravity * Time.deltaTime;

        if (wishJump) {
            playerVelocity.y = jumpSpeed;
            wishJump = false;
        }
    }

    /**
     * Applies friction to the player, called in both the air and on the ground
     */
    private void ApplyFriction (float t) {
        Vector3 vec = playerVelocity; // Equivalent to: VectorCopy();
        float speed;
        float newspeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;

        /* Only if the player is on the ground then apply friction */
        if (_controller.isGrounded) {
            control = speed < runDeacceleration ? runDeacceleration : speed;
            drop = control * friction * Time.deltaTime * t;
        }

        newspeed = speed - drop;
        playerFriction = newspeed;
        if (newspeed < 0)
            newspeed = 0;
        if (speed > 0)
            newspeed /= speed;

        playerVelocity.x *= newspeed;
        playerVelocity.z *= newspeed;
    }

    private void Accelerate (Vector3 wishdir, float wishspeed, float accel) {
        float addspeed;
        float accelspeed;
        float currentspeed;

        currentspeed = Vector3.Dot (playerVelocity, wishdir);
        addspeed = wishspeed - currentspeed;
        if (addspeed <= 0)
            return;
        accelspeed = accel * Time.deltaTime * wishspeed;
        if (accelspeed > addspeed)
            accelspeed = addspeed;

        playerVelocity.x += accelspeed * wishdir.x;
        playerVelocity.z += accelspeed * wishdir.z;
    }

    private void ProgressStepCycle (float speed) {
        if (_controller.velocity.sqrMagnitude > 0 && (_cmd.rightMove != 0 || _cmd.forwardMove != 0)) {
            m_StepCycle += (_controller.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep)) {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio ();
    }

    void OnControllerColliderHit (ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        if (hit.gameObject.CompareTag("PushCube") || hit.gameObject.CompareTag ("Schans")) {

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);

            Debug.Log (pushDir);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * pushPower;
        }else if (hit.gameObject.CompareTag("PushCubeBorder")) {
            if (pushCubeBorder.moveX == true) {
                // Calculate push direction from move direction,
                // we only push objects to the sides never up and down
                Vector3 pushDir = new Vector3 (hit.moveDirection.x, 0, 0);

                Debug.Log (pushDir);

                // If you know how fast your character is trying to move,
                // then you can also multiply the push velocity by that.
               
                // Apply the push
                body.velocity = pushDir * pushPower;
            } else if (pushCubeBorder.moveZ == true) {
                // Calculate push direction from move direction,
                // we only push objects to the sides never up and down
                Vector3 pushDir = new Vector3 (0, 0, hit.moveDirection.z);

                Debug.Log (pushDir);

                // If you know how fast your character is trying to move,
                // then you can also multiply the push velocity by that.
                
                // Apply the push
                body.velocity = pushDir * pushPower;
            }
        }
    }

    private void OnGUI () {
        GUI.Label (new Rect (0, 0, 400, 100), "FPS: " + fps, style);
        var ups = _controller.velocity;
        ups.y = 0;
        GUI.Label (new Rect (0, 15, 400, 100), "Speed: " + Mathf.Round (ups.magnitude * 100) / 100 + "ups", style);
        GUI.Label (new Rect (0, 30, 400, 100), "Top Speed: " + Mathf.Round (playerTopVelocity * 100) / 100 + "ups", style);
    }
}
