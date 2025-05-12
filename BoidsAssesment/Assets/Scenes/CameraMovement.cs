using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine;
using System.Threading;
using UnityEngine.WSA;

public class CameraMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float lookspeed = 3f;

    private float rotationX = 0f;

    private void Update()
    {
        // using the Mouse to look around
        float MouseX = Input.GetAxis("Mouse X") * lookspeed;
        float MouseY = Input.GetAxis("Mouse Y") * lookspeed;

        rotationX -= MouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90f);

        transform.Rotate(Vector3.up * MouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Movment With WASD SPACE AND Shift for Up and Down
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float upDown = 0f;


        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {

            if (moveSpeed! <= 61) {
                moveSpeed++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (moveSpeed !>= 3) {
                 moveSpeed--;
            }
        }
        if (Input.GetKey(KeyCode.Space)) upDown = moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftShift)) upDown = -moveSpeed * Time.deltaTime;

        transform.Translate(new Vector3(horizontal, upDown, vertical));
    }
}
