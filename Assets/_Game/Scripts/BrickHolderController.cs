using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolderController : MonoBehaviour
{
    [SerializeField]
    private GameObject brickBlockPrefab;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    public int countHolder = 1;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update() { }

    public void OnInit()
    {
        countHolder = 1;
    }

    public void AddBrickBlock()
    {
        // Add brick block to brick holder
        var brickPrefab = Instantiate(
            brickBlockPrefab,
            gameObject.transform.position,
            Quaternion.identity
        );

        // brickPrefab.transform.position = Vector3.zero;
        brickPrefab.transform.SetParent(gameObject.transform);
        brickPrefab.transform.eulerAngles = new Vector3(-90f, 0, -180f);
        brickPrefab.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y - (0.31f * countHolder),
            gameObject.transform.position.z
        );

        countHolder++;
    }

    public void ClearAllBrickBlock()
    {
        countHolder = 0;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
