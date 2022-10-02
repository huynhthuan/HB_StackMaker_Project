using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSensor : MonoBehaviour
{
    internal bool moveAble = false;
    public RaycastHit hit;

    [SerializeField]
    private MapController mapController;

    private bool hasPassStartFireworkPoint;
    private bool hasOpenBox;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        hasPassStartFireworkPoint = false;
        hasOpenBox = false;
    }

    void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (
            Physics.Raycast(
                transform.position,
                transform.TransformDirection(Vector3.down),
                out hit,
                Mathf.Infinity
            )
        )
        {
            // If hit Brick or Pllar
            if (hit.collider.gameObject.tag == "Brick" || hit.collider.gameObject.tag == "Pllar")
            {
                // Set moveable
                moveAble = true;
                // Debug.Log("Did Hit Brick or Pllar");
            }
            else
            {
                // Set can't moveable
                moveAble = false;
                // Debug.Log("Did not Hit");
            }

            if (hit.collider.gameObject.tag == "StartFireWork")
            {
                if (!hasPassStartFireworkPoint)
                {
                    // Enable firework
                    mapController.GetComponentInChildren<WinPosController>().startFireWork();
                }
            }

            if (hit.collider.gameObject.tag == "OpenBox")
            {
                gameObject.GetComponentInParent<Player>().isHasStandOpenBoxPosition = true;

                if (!hasOpenBox)
                {
                    GameController gameController = gameObject
                        .GetComponentInParent<Player>()
                        .gameController;

                    hasOpenBox = true;
                    // Enable firework
                    gameController.SwitchSubCamera();
                    gameObject.GetComponentInParent<Player>().animator.SetInteger("renwu", 2);
                    mapController.GetComponentInChildren<WinPosController>().openBox();
                    gameController.OnEndLevel();
                }
            }
        }

        // Draw debug raycast
        Debug.DrawRay(
            transform.position,
            transform.TransformDirection(Vector3.down) * hit.distance,
            Color.yellow
        );
    }
}
