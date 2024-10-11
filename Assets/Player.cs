using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private Vector3 movementDirection;
    private Vector2 movementInput;

    private Plane plane;
    [SerializeField] private float speed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        plane = new Plane(Vector3.up,Vector3.zero);
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Rotation();
        Movement();
        
    }
    private void Movement()
    {
        Camera cam = Camera.main;
        Vector3 camright = cam.transform.right;
        Vector3 camfoward = cam.transform.forward;
        camright.y = 0f;
        camfoward.y = 0f;
        movementDirection = (camright * movementInput.x + camfoward * movementInput.y).normalized;
        characterController.Move(speed*Time.deltaTime*movementDirection);
    }
    private void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.SetNormalAndPosition(Vector3.up, transform.position);
        Vector3 vector = Vector3.zero;
        if(plane.Raycast(ray, out float distance))
        {
            vector = ray.GetPoint(distance);
        }
        Vector3 direction = (vector - transform.position);
        direction.y = 0;
        transform.rotation= Quaternion.LookRotation(direction);
    }
    private void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        animator.SetBool("Movement",movementInput!=Vector2.zero);
    }
}
