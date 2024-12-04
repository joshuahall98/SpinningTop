using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManager : MonoBehaviour
{

    PlayerManager instance;

    [SerializeField] GameObject playerInputObject;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(var user in InputUser.all)
        {
            var obj = Instantiate(playerInputObject);
            var inputController = obj.GetComponent<PlayerInputController>();
            inputController.AssignActions(user.actions);
        }
    }
}
