using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    internal MapLevelController mapLevelController;
    internal WinPosController winPosController;

    public void InitMap(int level)
    {
        Debug.Log("Init map " + level);
        clearMap();
        // Loading map from resource
        var mapRes = Resources.Load("Maps/Map_Level_" + level) as GameObject;
        // Instantiate map from resources
        GameObject mapInstance = Instantiate(mapRes, Vector3.zero, Quaternion.identity);
        // Set map to map plane
        mapInstance.transform.SetParent(transform);
        mapLevelController = mapInstance.GetComponent<MapLevelController>();
        winPosController = mapLevelController.GetComponentInChildren<WinPosController>();
        Debug.Log("Init map done.");
        // Debug.Log(
        //     "Map " + gameObject.GetComponentInChildren<MapLevelController>().name + " has loaded."
        // );
    }

    public Vector3 GetPointMap(string position)
    {
        Vector3 point = Vector3.zero;
        switch (position)
        {
            case "startPoint":
                point = gameObject.GetComponentInChildren<MapLevelController>().startPoint.position;
                break;
            case "endPoint":
                point = gameObject.GetComponentInChildren<MapLevelController>().endPoint.position;
                break;
        }

        return new Vector3(point.x, point.y + 2.96f, point.z);
    }

    public void clearMap()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void FinishLevel() { }
}
