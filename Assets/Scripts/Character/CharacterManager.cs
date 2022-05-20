using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_GodPrefab;

    private int m_SelectNum = 1;

    private PlayerMove m_Player = null;
    private GodMove m_God = null;

    private void Start()
    {
        m_Player = SetCharacter(m_PlayerPrefab, new Vector3(0, 0.6f, 1)).GetComponent<PlayerMove>();
        m_God = SetCharacter(m_GodPrefab, new Vector3(0, 0.6f, -1)).GetComponent<GodMove>();
    }

    private void FixedUpdate()
    {
        if (m_SelectNum == 0)
        {
            m_Player.CharacterMovement("Horizontal", "Vertical");
            m_God.CharacterMovement("Horizontal_2p", "Vertical_2p");
        }
        else
        {
            m_Player.CharacterMovement("Horizontal_2p", "Vertical_2p");
            m_God.CharacterMovement("Horizontal", "Vertical");
        }
    }

    private GameObject SetCharacter(GameObject gameObject, Vector3 pos)
    {
        GameObject game = Instantiate(gameObject);
        game.transform.position = pos;
        return game;
    }
}
