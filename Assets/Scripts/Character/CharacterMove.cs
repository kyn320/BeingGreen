using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float m_Speed = 2f;
    [SerializeField] private float m_RotSpeed = 10f;

    private Vector3 m_Direction;
    private Vector3 m_PastPos = Vector3.zero;

    public Vector3 Direction { get => m_Direction; set => m_Direction = value; }

    public void CharacterMovement(string Hori, string Verti)
    {
        float x = Input.GetAxis(Hori) * Time.deltaTime * m_Speed;
        float z = Input.GetAxis(Verti) * Time.deltaTime * m_Speed;

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
