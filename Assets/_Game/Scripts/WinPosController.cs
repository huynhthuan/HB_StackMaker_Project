using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPosController : MonoBehaviour
{
    [SerializeField]
    public Transform openBoxPosition;

    [SerializeField]
    private GameObject boxGiftClose;

    [SerializeField]
    private GameObject boxGiftOpen;

    [SerializeField]
    public Transform finishPosition;

    [SerializeField]
    private ParticleSystem fireWorkPartical;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void openBox()
    {
        boxGiftClose.SetActive(false);
        boxGiftOpen.SetActive(true);
    }

    public void startFireWork()
    {
        fireWorkPartical.Play();
    }
}
