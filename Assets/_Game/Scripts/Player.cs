using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public CollisionSensor[] listCollisionSensor;

    [SerializeField]
    private GameController gameController;

    internal bool isCanMove = true;
    internal bool isMoving = false;

    Vector3[] directionVector = new Vector3[]
    {
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left
    };

    private void OnInit() { }

    public void SetPosition(Vector3 pointToSet)
    {
        // Set position player by point
        transform.position = pointToSet;
    }

    private bool CanTurnLeftOrRight()
    {
        // Check can turn left or right
        return !listCollisionSensor[(int)GameController.Direction.Left].moveAble
            || !listCollisionSensor[(int)GameController.Direction.Right].moveAble;
    }

    private bool CanGoStraightOrBack()
    {
        // Check can go straight or back
        return !listCollisionSensor[(int)GameController.Direction.Forward].moveAble
            || !listCollisionSensor[(int)GameController.Direction.Back].moveAble;
    }

    public void MoveByDirect(GameController.Direction dir)
    {
        // Check if can move by dir
        isCanMove = listCollisionSensor[(int)dir].moveAble;
        Debug.Log("Check move forward " + dir);

        // If can move
        if (isCanMove)
        {
            isMoving = true;
            // Move player by dir
            transform.Translate(directionVector[(int)dir]);
        }

        // If player stop
        if (
            (
                !listCollisionSensor[(int)GameController.Direction.Forward].moveAble
                && CanTurnLeftOrRight()
            )
            || (
                !listCollisionSensor[(int)GameController.Direction.Back].moveAble
                && CanTurnLeftOrRight()
            )
            || (
                !listCollisionSensor[(int)GameController.Direction.Right].moveAble
                && CanGoStraightOrBack()
            )
            || (
                !listCollisionSensor[(int)GameController.Direction.Left].moveAble
                && CanGoStraightOrBack()
            )
        )
        {
            // Set moving false
            isMoving = false;
        }
    }
}
