using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuasarGames;

public class PlayerController : MonoBehaviour
{

    //---Mouse look
    public float RotationSpeed = 10f;
    //---Movement
    public float Speed = 1;
    public float Gravity = 20f;

    CharacterController characterController;
    Vector3 direction; //Move diretion
    Camera camera;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;

    }

    void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
        {


            #region Movement
            var _v = Input.GetAxis("Vertical"); //Move Forward/Back
            var _h = Input.GetAxis("Horizontal");//Strafe
            direction = new Vector3(_h, 0, _v);
            var magnitude = Mathf.Clamp(direction.magnitude, 0, 1);
            direction.Normalize(); //normalize vector to prevent diagonal speed up
            direction = direction * magnitude;
            #endregion

            //Fire currently equipped gun (no need to override the base class)
            if (Input.GetButtonDown("Fire1"))
            {
                //base.Attack(transform.position, transform.forward, CurrentGun.WeaponRange, CurrentGun.DamageOut);
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
        {
            //---Apply Gravity
            direction.y = direction.y - (Gravity * Time.deltaTime);
            //---Move
            characterController.Move(direction * Speed * Time.deltaTime);
        }
    }

    //---Prevent gimble lock
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
    /*//Removed mouse look
    //Vector3 _position = Camera.main.WorldToScreenPoint(transform.position);
    //Vector3 _direction = Input.mousePosition - _position;
    //float _angle = Mathf.Atan2(_direction.y,_direction.x)*Mathf.Rad2Deg;
    //transform.rotation = Quaternion.AngleAxis(_angle, Vector3.up);*/
}
