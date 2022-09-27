using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSensor : MonoBehaviour
{
    public bool isCantMove = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        RaycastHit hit;
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
            if (hit.collider.gameObject.tag == "Brick" || hit.collider.gameObject.tag == "Pllar")
            {
                isCantMove = false;
                // Debug.Log("Did Hit Brick or Pllar");
            }
            else
            {
                isCantMove = true;

                // Debug.Log("Did not Hit");
            }
        }

        Debug.DrawRay(
            transform.position,
            transform.TransformDirection(Vector3.down) * hit.distance,
            Color.yellow
        );
    }

    public void ChangeSensorPosition(Vector3 dirMove)
    {
        if (Mathf.Abs(dirMove.x) > Mathf.Abs(dirMove.y))
        {
            if (dirMove.x > 0)
            {
                Debug.Log("Right");
                // transform.position = new Vector3(1f, 2f, 0f);
            }
            else
            {
                Debug.Log("Left");
                // transform.position = new Vector3(-1f, 2f, 0f);
            }
        }
        else
        {
            if (dirMove.y > 0)
            {
                Debug.Log("Up");
                // transform.position = new Vector3(0f, 2f, 1f);
            }
            else
            {
                Debug.Log("Down");
                // transform.position = new Vector3(0f, 2f, -1f);
            }
        }
    }
}
