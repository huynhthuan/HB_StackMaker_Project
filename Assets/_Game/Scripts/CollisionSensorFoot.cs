using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSensorFoot : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public RaycastHit hit;

    public bool hasStandEndLevel;
    public bool isHasPassFireWork;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
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
                if (hit.collider.gameObject.layer == 3)
                {
                    hasStandEndLevel = true;
                }
            }
            else
            {
                // Debug.Log("Did not Hit");
            }

            if (hit.collider.gameObject.tag == "OpenBox")
            {
                player.isHasStandOpenBoxPosition = true;
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

        // Draw debug raycast
        Debug.DrawRay(
            transform.position,
            transform.TransformDirection(Vector3.down) * hit.distance,
            Color.yellow
        );
    }
}
