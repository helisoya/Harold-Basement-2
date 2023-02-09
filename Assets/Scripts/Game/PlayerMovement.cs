using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static GameObject body;


    public int speed;

    private int default_speed;

    public int rotationspeed;

    float yaw = 0;

    private Rigidbody rb;

    public float hover;

    private float Stamina = 1f;


    [SerializeField] private Image stamina_image;

    [SerializeField] private GameObject flashlight;

    void Awake(){
        body = gameObject;
    }

    void Start(){
        rb = GetComponent<Rigidbody>();
        default_speed = speed;
    }
   
    void Update()
    {

        if(!Manager.instance.inGame) return;

        if(Input.GetKeyDown(KeyCode.F) || ( Input.GetJoystickNames().Length != 0 &&  Input.GetKeyDown(KeyCode.Joystick1Button1))){
            flashlight.SetActive(!flashlight.activeInHierarchy);
        }

        // Run

        if(Input.GetKey(KeyCode.LeftShift) || ( Input.GetJoystickNames().Length != 0 &&  Input.GetKey(KeyCode.Joystick1Button0))){
            if(Stamina>0f && speed!=default_speed-2){
                Stamina-=1*Time.deltaTime;
                speed=default_speed+2;
            }else if(Stamina==0f){
                speed= default_speed-2;
            }else if(speed!=default_speed-2){
                speed = default_speed;
            }
        }else if(speed!=default_speed-2){
            speed = default_speed;
        }

        Stamina = Mathf.Clamp(Stamina+0.3f*Time.deltaTime,0f,1f);

        if (Stamina==1f && speed == default_speed-2){
            speed=default_speed;
        }

        stamina_image.fillAmount=Stamina;


        // Deplacement

        Vector3 movX = transform.right * Input.GetAxis("Horizontal");
        Vector3 movZ = transform.forward * Input.GetAxis("Vertical");

        Vector3 move = (movX+movZ).normalized;
        if(move != Vector3.zero){
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);

        }
        
        
        // Camera
        yaw = rotationspeed * Input.GetAxis("Mouse X");
        if(yaw == 0 && Input.GetJoystickNames().Length != 0 && (Input.GetAxis("JoyStick Cam") <= -0.1f || Input.GetAxis("JoyStick Cam") >= 0.1f)){
            yaw = rotationspeed * Input.GetAxis("JoyStick Cam");
        }

        rb.MoveRotation(rb.rotation* Quaternion.Euler(new Vector3 (0,yaw,0)));


        
        // Hover

        RaycastHit hit;
        if (Physics.Raycast (transform.position,-Vector3.up, out hit,hover))
        {
            rb.MovePosition(rb.position+new Vector3(0, (hover - hit.distance),0)*Time.deltaTime*speed);
        }else{
            rb.MovePosition(rb.position+new Vector3(0, -1,0)*Time.deltaTime*speed);
        }


    }
}
