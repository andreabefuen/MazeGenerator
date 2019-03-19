using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Camera ownCamera;
    Vector3 movement;
    int floorLayerMask;

    private Rigidbody rigid;
    Animator anim;

    CreationMazeSizeUI uiElements;

    

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        floorLayerMask = LayerMask.GetMask("floor");
        uiElements = GameObject.Find("MazeGenerator").GetComponent<CreationMazeSizeUI>();

        

    }

    void Update()
    {
        Move();
        Turning();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            anim.SetTrigger("isWalking");
            anim.ResetTrigger("isIdle");
        }
        else
        {
            anim.ResetTrigger("isWalking");
            anim.SetTrigger("isIdle");
        }

        movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rigid.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = ownCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, 100, floorLayerMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 pointWithMouse = floorHit.point - transform.position;
            pointWithMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(pointWithMouse);
            rigid.MoveRotation(newRotation);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            Debug.Log("YOU FINISH");
            uiElements.WinCanvas();
            Time.timeScale = 0f;
        }
    }
}

