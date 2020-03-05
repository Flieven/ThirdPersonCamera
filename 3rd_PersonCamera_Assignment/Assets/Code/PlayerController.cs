using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float MovementSpeed = 10.0f;

    #endregion

    private void Awake()
    {
        
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float Strafe = 0.0f;
        float ForwardMovement = 0.0f;
        if (Input.GetAxis("Horizontal") != 0)
        {
            Strafe = Input.GetAxis("Horizontal") * MovementSpeed;
            Strafe *= Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            ForwardMovement = Input.GetAxis("Vertical") * MovementSpeed;
            ForwardMovement *= Time.deltaTime;
        }
        transform.Translate(Strafe, 0, ForwardMovement);
    }
}
