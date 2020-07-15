using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject CameraRig;

    private enum Direction { up, left, down, right};
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
            MoveVertical(true);
        }
        else if (Input.GetButton("Down") && CanMove(Direction.down))
        {
            MoveVertical(false);
        }

        if (Input.GetButton("Left") && CanMove(Direction.left))
        {
            MoveHorizontal(false);
        }
        else if (Input.GetButton("Right") && CanMove(Direction.right))
        {
            MoveHorizontal(true);
        }
    }

    private bool CanMove(Direction dir)
    {
        switch (dir)
        {
            case Direction.up:
                if (CameraRig.transform.position.z >= borders[(int)dir])
                {
                    return false;
                }
                return true;
            case Direction.left:
                if (CameraRig.transform.position.x <= borders[(int)dir])
                {
                    return false;
                }
                return true;
            case Direction.down:
                if (CameraRig.transform.position.z <= borders[(int)dir])
                {
                    return false;
                }
                return true;
            case Direction.right:
                if (CameraRig.transform.position.x >= borders[(int)dir])
                {
                    return false;
                }
                return true;

            default:
                return false;
        }
    }

    private void MoveHorizontal(bool pos)
    {
        float move = pos ? speed : -speed;
        CameraRig.transform.Translate(move, 0, 0);
    }

    private void MoveVertical(bool pos)
    {
        float move = pos ? speed : -speed;
        CameraRig.transform.Translate(0, 0, move);
    }
}
