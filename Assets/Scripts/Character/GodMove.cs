using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMove : CharacterMove
{
    protected override void CharacterMovement()
    {
        x = Input.GetAxis("Horizontal_2p") * Time.deltaTime * m_Speed;
        z = Input.GetAxis("Vertical_2p") * Time.deltaTime * m_Speed;
    }
}
