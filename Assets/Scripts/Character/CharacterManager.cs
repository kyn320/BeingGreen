using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private WorldController m_WorldController;

    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_GodPrefab;

    private int m_SelectNum = 1;

    private CharacterMove m_Player = null;
    private CharacterMove m_God = null;

    private Vector3 m_StartPlayerPos = Vector3.zero;
    private Vector3 m_StartGodPos = Vector3.zero;

    [Button("Spawn Character")]
    public void SpawnCharacter()
    {
        m_StartPlayerPos = (m_SelectNum == 0) ? m_WorldController.GetSpawnPoint(0) : m_WorldController.GetSpawnPoint(1);
        m_StartGodPos = (m_SelectNum == 0) ? m_WorldController.GetSpawnPoint(1) : m_WorldController.GetSpawnPoint(0);
        m_StartPlayerPos.y = 0.3f;
        m_StartGodPos.y = 0.3f;

        m_Player = SetCharacter(m_PlayerPrefab, m_StartPlayerPos, m_SelectNum == 1);
        m_God = SetCharacter(m_GodPrefab, m_StartGodPos, m_SelectNum == 0);
    }


    private void Update()
    {
        if (Time.timeScale < 0.1f) return;

        if (m_SelectNum == 0)
        {
            m_Player?.UpdateMovement("Horizontal", "Vertical");
            m_God?.UpdateMovement("Horizontal_2p", "Vertical_2p");
        }
        else
        {
            m_Player?.UpdateMovement("Horizontal_2p", "Vertical_2p");
            m_God?.UpdateMovement("Horizontal", "Vertical");
        }
    }

    private CharacterMove SetCharacter(GameObject gameObject, Vector3 pos, bool Is2P = false)
    {
        CharacterMove game = Instantiate(gameObject).GetComponent<CharacterMove>();
        game.transform.position = pos;
        if (Is2P) game.transform.rotation = Quaternion.Euler(0, 180f, 0);
        return game;
    }
}
