using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField]
    private GameObject brickBlock;

    public bool isHoldBrickBlock = true;

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
