using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private WorldController m_WorldController;

    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_GodPrefab;

    private int m_SelectNum = 0;

    private CharacterMove m_Player = null;
    private CharacterMove m_God = null;

    private Vector3 m_StartPlayerPos = Vector3.zero;
    private Vector3 m_StartGodPos = Vector3.zero;

    private void Start()
    {

    }

    [Button("Start")]
    public void StartGame()
    {
        GameRuleController.Instance.StartGame();

        m_StartPlayerPos = (m_SelectNum == 0) ? m_WorldController.tileControllers[0].transform.position : m_WorldController.tileControllers[m_WorldController.tileControllers.Length - 1].transform.position;
        m_StartGodPos = (m_SelectNum == 0) ? m_WorldController.tileControllers[m_WorldController.tileControllers.Length - 1].transform.position : m_WorldController.tileControllers[0].transform.position;
        m_StartPlayerPos.y = 0.6f;
        m_StartGodPos.y = 0.6f;

        m_Player = SetCharacter(m_PlayerPrefab, m_StartPlayerPos);
        m_God = SetCharacter(m_GodPrefab, m_StartGodPos, true);
    }


    private void FixedUpdate()
    {
        if (Time.timeScale < 0.1f) return;

        if (m_SelectNum == 0)
        {
            m_Player?.CharacterMovement("Horizontal", "Vertical");
            m_God?.CharacterMovement("Horizontal_2p", "Vertical_2p");
        }
        else
        {
            m_Player?.CharacterMovement("Horizontal_2p", "Vertical_2p");
            m_God?.CharacterMovement("Horizontal", "Vertical");
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
