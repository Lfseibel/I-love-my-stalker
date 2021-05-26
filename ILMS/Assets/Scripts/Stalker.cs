using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : EntityController
{
    private void FixedUpdate()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        MovePosition(inputVector);
    }   
}
