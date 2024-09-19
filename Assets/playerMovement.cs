using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float Movespeed = 10;
    [SerializeField] float Jump = 5;
    [SerializeField] float groundCheckDistance = 1;
    [SerializeField] bool onGround;
    [SerializeField] Transform cam;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    private void Update()
    {
        onGround = (Physics.Raycast(transform.position, Vector3.up * -1, groundCheckDistance));

        if (Input.GetButtonDown("Jump") && onGround)
        {
           
        }
        if (onGround)
        {
            anim.SetBool("grounded", true);
        }
        if (!onGround)
        {
            anim.SetBool("grounded", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal") * Movespeed;
        float verticalinput = Input.GetAxisRaw("Vertical") * Movespeed;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;


        Vector3 ForwardRelative = verticalinput * camForward;
        Vector3 RightRelative = horizontalInput * camRight;

        Vector3 moveDir = ForwardRelative + RightRelative;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
       
        var movementVector = new Vector3(horizontalInput, 0, verticalinput);
        anim.SetFloat("Speed", movementVector.magnitude);
        if (moveDir.sqrMagnitude == 0) return;

    }
    public void OnMove()
    {

        transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);

    }
    public void OnJump()
    {
        if (onGround)
        {
          rb.velocity = new Vector3(rb.velocity.x, Jump, rb.velocity.z);
          anim.SetTrigger("Jump");
        }
        
    }
}
