using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField]
    private GameObject brickBlock;

    public bool isHoldBrickBlock = true;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void UnHoldBrickBlock()
    {
        isHoldBrickBlock = false;
        brickBlock.SetActive(false);
    }

    public void HoldBrickBlock()
    {
        isHoldBrickBlock = true;
        brickBlock.SetActive(true);
    }
}
