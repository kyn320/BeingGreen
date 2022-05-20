using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMove : MonoBehaviour
{
    [SerializeField] protected float m_Speed = 2f;
    [SerializeField] protected float m_RotSpeed = 10f;

    protected Vector3 m_Direction;
    protected Vector3 m_PastPos = Vector3.zero;
    protected float x, z;

    public Vector3 Direction { get => m_Direction; set => m_Direction = value; }

    abstract protected void CharacterMovement();

    private void FixedUpdate()
    {
        CharacterMovement();
        Move();
    }

    private void Move()
    {
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
