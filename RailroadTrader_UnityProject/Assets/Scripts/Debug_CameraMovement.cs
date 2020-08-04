using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_CameraMovement : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float scrollSpeed = 20f;
    private float mobileSpeed = 0.1f;

    public float ScreenBound_x = 420.0f;
    public float ScreenBound_y = 90.0f;
    public float ScreenBound_z = 420.0f;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android) MobileMovement();
        else PCMovement();
    }

    void MobileMovement()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPositon = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPositon.x * mobileSpeed, -touchDeltaPositon.y * mobileSpeed, 0);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -ScreenBound_x, ScreenBound_x),
                Mathf.Clamp(transform.position.y, ScreenBound_y, ScreenBound_y),
                Mathf.Clamp(transform.position.z, -20, ScreenBound_z)
                );
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            Camera.main.fieldOfView += deltaMagnitudeDiff * mobileSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 15f, 70f);

        }
    }

    void PCMovement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        }
        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -ScreenBound_x, ScreenBound_x),
                Mathf.Clamp(transform.position.y, ScreenBound_y, ScreenBound_y),
                Mathf.Clamp(transform.position.z, -20, ScreenBound_z)
                );
    }
}
