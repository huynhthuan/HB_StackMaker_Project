using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private MapController mapController;

    protected int playerLevel;

    private Vector3 touchStartPoint,
        touchEndPoint;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        // Reset touch point
        resetTouchPoint();

        // Get current player level
        GetDataPLayer();

        //Loading map by player level
        mapController.InitMap(playerLevel);

        // Set player to start point of map
        player.SetPosition(mapController.GetPointMap("startPoint"));
    }

    private void OnDespawn() { }

    private bool IsTouchValid()
    {
        return Vector3.Distance(touchStartPoint, touchEndPoint) >= 10f
            && touchEndPoint != Vector3.zero
            && touchEndPoint != Vector3.zero;
    }

    public Vector3 GetVectorNormalized(Vector3 startPoint, Vector3 endPoint)
    {
        return (startPoint - endPoint).normalized;
    }

    private Vector3 GetDirectToMove()
    {
        Vector3 dir = Vector3.zero;
        // Normalized vector dir
        Vector3 dirVectorNormalized = GetVectorNormalized(touchEndPoint, touchStartPoint);

        if (Mathf.Abs(dirVectorNormalized.x) > Mathf.Abs(dirVectorNormalized.y))
        {
            dir = dirVectorNormalized.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            dir = dirVectorNormalized.y > 0 ? Vector3.forward : Vector3.back;
        }

        return dir;
    }

    public void GetDataPLayer()
    {
        // Get current player level from playerPrefs
        playerLevel = PlayerPrefs.GetInt("playerLevel", 2);
    }

    private void Update()
    {
        // If user touch down
        if (Input.GetMouseButtonDown(0))
        {
            // Get point touch down
            touchStartPoint = Input.mousePosition;
            // Debug.Log("touchStartPoint" + touchStartPoint);
        }

        // If user touch up
        if (Input.GetMouseButtonUp(0))
        {
            // Get point touch up
            touchEndPoint = Input.mousePosition;
            // Debug.Log("touchEndPoint" + touchEndPoint);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // if (!player.isCanMove)
        // {
        //     resetTouchPoint();
        // }

        if (!IsTouchValid())
        {
            return;
        }

        Debug.Log("Direct to move " + GetDirectToMove());

        player.MoveByDirect(GetDirectToMove(), GetVectorNormalized(touchEndPoint, touchStartPoint));
    }

    public void resetTouchPoint()
    {
        touchStartPoint = Vector3.zero;
        touchEndPoint = Vector3.zero;
    }
}
