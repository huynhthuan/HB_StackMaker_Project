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
    public GameController gameController;

    [SerializeField]
    public BrickHolderController brickHolderController;

    [SerializeField]
    public Animator animator;

    internal bool isCanMove;
    internal bool isMoving;
    internal bool isHasStandFinishLevel;
    internal bool isHasStandOpenBoxPosition;

    public Direction direction;

    Vector3[] directionVector = new Vector3[]
    {
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left
    };

    public void OnInit(Vector3 initPosition)
    {
        animator.transform.localPosition = Vector3.zero;
        animator.SetInteger("renwu", 0);
        SetPosition(initPosition);
        isHasStandFinishLevel = false;
        isHasStandOpenBoxPosition = false;
        isCanMove = true;
        isMoving = false;
        collisionSensorFoot.OnInit();
        brickHolderController.OnInit();
        foreach (CollisionSensor child in listCollisionSensor)
        {
            child.OnInit();
        }
    }

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
        // Debug.Log("Check move forward " + dir + " - " + isCanMove);

        // If can move
        if (isCanMove)
        {
            isMoving = true;

            // Move player by dir
            if (collisionSensorFoot.hasStandEndLevel && brickHolderController.countHolder == 1)
            {
                transform.Translate(directionVector[(int)dir] * 0.05f);
            }
            else
            {
                transform.Translate(directionVector[(int)dir]);
            }

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

            if (currentRayCastHit.collider.gameObject.tag == "Remove Brick")
            {
                if (hitBrickController.isHoldBrickBlock)
                {
                    // Add brick to brick holder
                    brickHolderController.RemoveBrickBlock();
                    // Decrease player tall
                    DecreasePlayerTall();
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
    }

    private void IncreasePlayerTall()
    {
        transform.Translate(Vector3.up * 0.31f);
    }

    public void DecreasePlayerTall()
    {
        Debug.Log("Decrease player tall");
        animator.transform.Translate(Vector3.down * 0.31f);
    }

    public IEnumerator DecreaseAllPlayerTall()
    {
        Debug.Log("Decrease player all tall");

        WaitForSeconds wait = new WaitForSeconds(0.01f);

        int currentHolder = brickHolderController.countHolder;
        for (int i = 0; i < currentHolder; i++)
        {

            Debug.Log("Holder " + brickHolderController.countHolder);
            brickHolderController.RemoveBrickBlock();
            // Decrease player tall
            transform.Translate(Vector3.down * 0.31f);
            // animator.transform.Translate(Vector3.down * 0.31f);
            yield return wait;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Player))]
public class PlayerButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("DecreasePlayerTall"))
        {
            ((Player)target).DecreasePlayerTall();
        }
    }
}
#endif
