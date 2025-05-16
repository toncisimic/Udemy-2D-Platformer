using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchMehanic : MonoBehaviour
{
    public TorchScript torch1;
    public TorchScript torch2;
    public TorchScript torch3;

    public GameObject GameObjec;

    private bool isFinished;

    private Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GameObjec.GetComponent<Animator>();
        isFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (torch1.isRed && !torch2.isRed && torch3.isRed && !isFinished)
        {
            Debug.Log("Sve baklje su crvene!");
            ActivateSwitch();
            isFinished = true;
        }
    }

    private void ActivateSwitch()
    {

        doorAnimator.SetTrigger("openDoor");

        Debug.Log($"Tipka E pritisnuta u zoni – nešto se događa!");
    }
}
