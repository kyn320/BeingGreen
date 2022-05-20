using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CharacterMove
{
    protected override void CharacterMovement()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * m_Speed;
        z = Input.GetAxis("Vertical") * Time.deltaTime * m_Speed;
    }
}
