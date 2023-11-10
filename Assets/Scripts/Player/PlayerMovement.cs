using UnityEngine;

#if ENABLE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
#endif


namespace AmazingAssets.CurvedWorld.Example
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Характеристики")]
        float movementSpeed;                            // How fast moves forward and back.
        public float m_TurnSpeed;                       // How fast turns in degrees per second.

        [Header("Ссылки")]
        private Rigidbody rb;             
        Animator animator;
        [SerializeField] PlayerController playerController;

        [Header("Управление")]
        float horizontalInput, verticalInput;
        bool isInputActive;
        Vector3 movementVector;

        private void Awake ()
        {
            rb = GetComponent<Rigidbody> ();
            animator = GetComponent<Animator> ();
            isInputActive = false;
        }
        private void Start()
        {
            movementSpeed = playerController.GetMovementSpeed();
        }
        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
         
        }
        private void OnEnable ()
        {
            rb.isKinematic = false;                       
        }   
        
        private void FixedUpdate ()
        {
            if (isInputActive)
            {
                Move();
                Turn();
            }
        }

        public void SetMovementSpeed(float speed)
        {
            movementSpeed = speed; 
        }
        private void Move ()
        {
            movementVector = new Vector3(horizontalInput, 0, verticalInput).normalized / 10f;
            rb.MovePosition(rb.position + movementVector * movementSpeed * Time.fixedDeltaTime);
            animator.SetFloat("speed", movementVector.magnitude);
        }

        private void Turn ()
        {                     
            if (movementVector != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementVector);
                Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, m_TurnSpeed * Time.deltaTime);
                rb.MoveRotation(newRotation);
            }
        }

        public void SwitchInput(bool state)
        {          
            isInputActive = state;
            if (!isInputActive)
            {
                rb.velocity = Vector3.zero;
                animator.SetFloat("speed", 0);
            }
        }

        private void OnDisable()
        {
            rb.isKinematic = true;
        }
    }
}