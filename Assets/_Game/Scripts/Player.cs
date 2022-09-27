using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public StopSensor stopSensor;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnInit() { }

    public void SetPosition(Vector3 pointToSet)
    {
        Debug.Log("Set position player.");
        transform.position = pointToSet;
    }

    public void RotateByDir(Vector3 dirRotation)
    {
        // Debug.Log("Direct vector rotation " + dirRotation);

        if (Mathf.Abs(dirRotation.x) > Mathf.Abs(dirRotation.y))
        {
            transform.rotation =
                dirRotation.x > 0
                    ? Quaternion.Euler(Vector3.up * 90)
                    : Quaternion.Euler(Vector3.up * 270);
        }
        else
        {
            transform.rotation =
                dirRotation.y > 0
                    ? Quaternion.Euler(Vector3.up * 0)
                    : Quaternion.Euler(Vector3.up * 180);
        }
    }

    public void MoveByDirect(Vector3 dir)
    {
        if (!stopSensor.isCantMove)
        {
            Debug.Log("Run");
            transform.Translate(dir);
        }
    }
}
