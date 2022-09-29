using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Direction
{
    Forward = 0,
    Right = 1,
    Back = 2,
    Left = 3
}

public class Player : MonoBehaviour
{
    [SerializeField]
    public CollisionSensor[] listCollisionSensor;

    [SerializeField]
    public CollisionSensorFoot collisionSensorFoot;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private BrickHolderController brickHolderController;

    [SerializeField]
    private WinPosController winPosController;

    internal bool isCanMove = true;
    internal bool isMoving = false;

    public Direction direction;

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
        return !listCollisionSensor[(int)Direction.Left].moveAble
            || !listCollisionSensor[(int)Direction.Right].moveAble;
    }

    private bool CanGoStraightOrBack()
    {
        // Check can go straight or back
        return !listCollisionSensor[(int)Direction.Forward].moveAble
            || !listCollisionSensor[(int)Direction.Back].moveAble;
    }

    public void MoveByDirect(Direction dir)
    {
        // Check if can move by dir
        isCanMove = listCollisionSensor[(int)dir].moveAble;
        // Debug.Log("Check move forward " + dir);

        // If can move
        if (isCanMove)
        {
            isMoving = true;
            // Move player by dir
            transform.Translate(directionVector[(int)dir]);

            RaycastHit currentRayCastHit = collisionSensorFoot.hit;
            BrickController hitBrickController =
                currentRayCastHit.collider.GetComponentInParent<BrickController>();

            if (currentRayCastHit.collider.gameObject.tag == "Brick")
            {
                if (hitBrickController.isHoldBrickBlock)
                {
                    hitBrickController.UnHoldBrickBlock();
                    // Add brick to brick holder
                    brickHolderController.AddBrickBlock();
                    // Increase player tall
                    IncreasePlayerTall();
                }
            }
        }

        // If player stop
        if (
            (!listCollisionSensor[(int)Direction.Forward].moveAble && CanTurnLeftOrRight())
            || (!listCollisionSensor[(int)Direction.Back].moveAble && CanTurnLeftOrRight())
            || (!listCollisionSensor[(int)Direction.Right].moveAble && CanGoStraightOrBack())
            || (!listCollisionSensor[(int)Direction.Left].moveAble && CanGoStraightOrBack())
        )
        {
            // Set moving false
            isMoving = false;
        }

        if (collisionSensorFoot.hasStandEndLevel && brickHolderController.countHolderForRemove > 0)
        {
            DecreasePlayerTall();
            brickHolderController.ClearBrickBlock();
        }
    }

    private void IncreasePlayerTall()
    {
        transform.Translate(Vector3.up * 0.31f);
    }

    public void DecreasePlayerTall()
    {
        Debug.Log("Decrease player tall");
        transform.Translate(Vector3.down * 0.31f);
    }

    public void runToBox()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            winPosController.finishPosition.position,
            Vector3.Distance(transform.position, winPosController.finishPosition.position)
        );
    }

    // public void debugDev(){
    //     Debug.Log("123");
    // }
}

// #if UNITY_EDITOR
// [CustomEditor(typeof(Player))]
// public class PlayerButton : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         if (GUILayout.Button("Update walkable point"))
//         {
//             ((Player)target).debugDev();
//         }
//     }
// }
// #endif
