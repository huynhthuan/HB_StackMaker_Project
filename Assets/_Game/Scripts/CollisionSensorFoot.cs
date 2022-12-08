using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSensorFoot : MonoBehaviour
{
    [SerializeField]
    internal GameController gameController;
    public RaycastHit hit;

    public bool hasStandEndLevel;
    public bool isHasPassFireWork;

    // Start is called before the first frame update
    void Start() { }

    public void OnInit()
    {
        isHasPassFireWork = false;
        hasStandEndLevel = false;
    }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
        if (
            hasStandEndLevel
            && transform.GetComponentInParent<Player>().brickHolderController.countHolder == 1
        )
        {
            transform.GetComponentInParent<Player>().MoveByDirect(Direction.Forward);
        }

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
                // Debug.Log("Did Hit Brick or Pllar");
                if (hit.collider.gameObject.layer == 3 && hasStandEndLevel == false)
                {
                    Debug.Log("End level");
                    StartCoroutine(gameController.player.DecreaseAllPlayerTall());
                    hasStandEndLevel = true;
                    gameController.isLevelPlaying = false;
                }

                if (hit.collider.gameObject.layer == 6)
                {
                    if (isHasPassFireWork)
                    {
                        return;
                    }
                    isHasPassFireWork = true;
                    hit.collider.GetComponentInParent<WinPosController>().fireWorkPartical.Play();
                }
            }
            else
            {
                // Debug.Log("Did not Hit");
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
