using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationSmoothTime;

    [Header("Gravity & Jump Settings")]
    [SerializeField] private float gravity=-9.81f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask jumpablelayer;
    [SerializeField] private Transform playerFootTransform;
    [SerializeField] private float groundCheckDistance;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreTextObject;
    [SerializeField] private Image healthBarFGImage;
    [Header("Audio")]
    [SerializeField] private AudioClip coinSFX;
    //[SerializeField] private AudioClip jumpSFX;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    [Header("Keybinds")]
    public InputAction moveAction;
    public InputAction runAction;
    public InputAction jumpAction; 
    private AudioSource audioSource;

    private CharacterController controller;
    private Animator animator;
    private Camera mainCam;
    private int score;
    private float currentHealth;
    private float maxHealth=100;



    private Vector3 velocity;
    private bool isGrounded;

    private void OnEnable(){
        moveAction.Enable();
        runAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable(){
        moveAction.Disable();
        runAction.Disable();
        jumpAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        mainCam=Camera.main;
        score=0;
        currentHealth=maxHealth;
        healthBarFGImage.fillAmount = currentHealth / maxHealth;
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Groundcheck
        isGrounded = Physics.CheckSphere(playerFootTransform.position, groundCheckDistance, jumpablelayer);
        if(isGrounded && velocity.y < 0){
            velocity.y=-2;

        }




        //Input
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
        bool isRunning= runAction.IsPressed();
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        //turn to camera
        if(direction.magnitude >= 0.1f){
            float targetAngle=Mathf.Atan2 (direction.x, direction.z )* Mathf.Rad2Deg + mainCam.transform.eulerAngles.y;
            float angle=Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref rotationSpeed,rotationSmoothTime);

            transform.rotation=Quaternion.Euler(0,angle,0);
            Vector3 newDir =Quaternion.Euler(0,targetAngle,0)*Vector3.forward;
            controller.Move(newDir.normalized * currentSpeed * Time.deltaTime);
        }

        //ANIMATION
        float animationSpeed = direction.magnitude * currentSpeed;
        animator.SetFloat("Speed" , animationSpeed, 0.1f, Time.deltaTime);  

        //jumping
        if(jumpAction.WasPressedThisFrame() && isGrounded){
            velocity.y=Mathf.Sqrt(-2 * jumpHeight * gravity);
            animator.SetTrigger("Jump");    
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);
    }



        private void OnTriggerEnter(Collider other){
             if(other.gameObject.tag == "Coins"){
                score++;
                if(score >= gameManager.coinsToWin){
                    gameManager.WinGame();
                }
                audioSource.PlayOneShot(coinSFX);
            Debug.Log(score);
            scoreTextObject.text=score.ToString();
           
            Destroy(other.gameObject);
            }
            if(other.gameObject.tag == "Zombie"){
                
                gameManager.LoseGame();
             }
        }

         private void OnControllerColliderHit(ControllerColliderHit hit){
             if(hit.gameObject.tag=="Enemies"){
                currentHealth -= 20;
                healthBarFGImage.fillAmount = currentHealth / maxHealth;
                Destroy(hit.gameObject);
                Debug.Log("Health: " + currentHealth);
                if(currentHealth<= 0){
                    gameManager.LoseGame();
                }

             }
             
        }
      
}
