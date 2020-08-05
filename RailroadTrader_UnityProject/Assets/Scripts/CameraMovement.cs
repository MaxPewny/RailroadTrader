using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject CameraRig;

    private enum Direction { up, left, down, right };
    private Direction dir;

    private float[] borders = new float[4];

    [SerializeField]
    private float leftBorder = -52.0f;
    [SerializeField]
    private float rightBorder = -7.0f;
    [SerializeField]
    private float upperBorder = 3.0f;
    [SerializeField]
    private float lowerBorder = -5.0f;

    [SerializeField]
    float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        borders[0] = upperBorder;
        borders[1] = leftBorder;
        borders[2] = lowerBorder;
        borders[3] = rightBorder;
    }

    void Update()
    {
        if (Input.GetButton("Up") && CanMove(Direction.up))
        {
            MoveHorizontal(true);
            MoveVertical(true);
        }
        else if (Input.GetButton("Down") && CanMove(Direction.down))
        {
            MoveHorizontal(false);
            MoveVertical(false);
        }

        if (Input.GetButton("Left") && CanMove(Direction.left))
        {
            MoveHorizontal(false);
            MoveVertical(true);
        }
        else if (Input.GetButton("Right") && CanMove(Direction.right))
        {
            MoveHorizontal(true);
            MoveVertical(false);
        }
    }

    private bool CanMove(Direction dir)
    {
        if (GameManager.Instance.State != GameState.RUNNING)
            return false;

        switch (dir)
        {
            case Direction.up:
                if (CameraRig.transform.position.z < upperBorder &&
                    CameraRig.transform.position.x < rightBorder)
                    return true;
                return false;
            case Direction.left:
                if (CameraRig.transform.position.x > leftBorder &&
                    CameraRig.transform.position.z < upperBorder)
                    return true;
                return false;

            case Direction.down:
                if (CameraRig.transform.position.z > lowerBorder &&
                    CameraRig.transform.position.x > leftBorder)
                    return true;
                return false;
            case Direction.right:
                if (CameraRig.transform.position.x < rightBorder &&
                      CameraRig.transform.position.z > lowerBorder)
                    return true;
                return false;

            default:
                return false;
        }
    }

    private void MoveHorizontal(bool right)
    {
        float move = right ? speed : -speed;
        CameraRig.transform.Translate(move, 0, 0);
    }

    private void MoveVertical(bool up)
    {
        float move = up ? speed : -speed;
        CameraRig.transform.Translate(0, 0, move);
    }
}
