using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] GameObject playerInputObject;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var user in InputUser.all)
        {
            var obj = Instantiate(playerInputObject);
            var inputController = obj.GetComponent<InputController>();
            inputController.AssignActions(user.actions);
        }
    }
}
