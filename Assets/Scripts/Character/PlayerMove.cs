using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CharacterMove
{
    public override void CharacterMovement(string Hori, string Verti)
    {
        x = Input.GetAxis(Hori) * Time.deltaTime * m_Speed;
        z = Input.GetAxis(Verti) * Time.deltaTime * m_Speed;

        Vector3 dir = new Vector3(x, 0, z);

        if (!(x == 0 && z == 0))
        {
            m_PastPos = this.transform.position;
            this.transform.position += dir;
            Direction = Vector3.Normalize(this.transform.position - m_PastPos);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * m_RotSpeed);
        }
    }
}
