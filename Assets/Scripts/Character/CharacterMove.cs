using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private GameObject m_characterRenderer;
    [SerializeField] private GameObject m_VFXAppearance;
    [SerializeField] private float m_Speed = 2f;
    [SerializeField] private float m_RotSpeed = 10f;

    private Animator m_Anim = null;

    private Vector3 m_MoveDirection;
    private Vector3 m_PastPos = Vector3.zero;

    public Vector3 m_LookDirection;

    public GameObject CharacterRenderer { get { return m_characterRenderer; } }
    public GameObject VFXAppearance { get => m_VFXAppearance; set => m_VFXAppearance = value; }

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (m_MoveDirection.sqrMagnitude > 0.1f)
        {
            m_Anim.SetBool("IsRunning", true);
            m_PastPos = this.transform.position;
            this.transform.position += m_MoveDirection * m_Speed * Time.deltaTime;
            m_LookDirection = Vector3.Normalize(this.transform.position - m_PastPos);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(m_LookDirection), Time.deltaTime * m_RotSpeed);
        }
        else
        {
            m_Anim.SetBool("IsRunning", false);
        }
    }

    public void Stop() {
        m_MoveDirection = Vector3.zero;
    }

    public void UpdateMovement(string Hori, string Verti)
    {
        m_MoveDirection.x = Input.GetAxis(Hori);
        m_MoveDirection.z = Input.GetAxis(Verti);
    }


}
