using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public CollisionSensor collisionSensorFront,
        collisionSensorBack,
        collisionSensorRight,
        collisionSensorLeft;

    private bool isCanMove = true;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnInit() { }

    public void SetPosition(Vector3 pointToSet)
    {
        // Set position player by point
        transform.position = pointToSet;
    }

    public void MoveByDirect(Vector3 dir, Vector3 dirRotation)
    {
        // If forward dir
        if (dir == Vector3.forward)
        {
            // Check player can go straight
            isCanMove = collisionSensorFront.moveAble;
            Debug.Log("Check move forward " + isCanMove);
        }

        // If back dir
        if (dir == Vector3.back)
        {
            // Check player can go back
            isCanMove = collisionSensorBack.moveAble;
            Debug.Log("Check move back " + isCanMove);
        }

        // If right dir
        if (dir == Vector3.right)
        {
            // Check player can turn right
            isCanMove = collisionSensorRight.moveAble;
            Debug.Log("Check move right " + isCanMove);
        }

        // If left dir
        if (dir == Vector3.left)
        {
            // Check player can turn left
            isCanMove = collisionSensorLeft.moveAble;
            Debug.Log("Check move left " + isCanMove);
        }

        // If can move
        if (isCanMove)
        {
            Debug.Log("Dir to move " + dir);
            // Move player by dir
            transform.Translate(dir);
        }
    }
}
