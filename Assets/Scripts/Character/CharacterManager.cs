using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private WorldController m_WorldController;

    [SerializeField] private GameObject m_PlayerMalePrefab;
    [SerializeField] private GameObject m_PlayerFemalePrefab;
    [SerializeField] private GameObject m_GodPrefab;
    [SerializeField] private GameObject m_uiplayerTimerHUDPrefab;

    private int m_SelectNum = 0;
    private bool m_IsFemale = false;

    private CharacterMove m_Player = null;
    private CharacterMove m_God = null;

    private Vector3 m_StartPlayerPos = Vector3.zero;
    private Vector3 m_StartGodPos = Vector3.zero;

    private bool isInput = false;

    [Button("Spawn Character")]
    public void SpawnCharacter()
    {
        m_StartPlayerPos = (m_SelectNum == 0) ? m_WorldController.GetSpawnPoint(0) : m_WorldController.GetSpawnPoint(1);
        m_StartGodPos = (m_SelectNum == 0) ? m_WorldController.GetSpawnPoint(1) : m_WorldController.GetSpawnPoint(0);
        m_StartPlayerPos.y = 0.3f;
        m_StartGodPos.y = 0.3f;

        m_Player = (m_IsFemale) ? SetCharacter(m_PlayerFemalePrefab, m_StartPlayerPos, m_SelectNum == 1) : SetCharacter(m_PlayerMalePrefab, m_StartPlayerPos, m_SelectNum == 1);
        m_God = SetCharacter(m_GodPrefab, m_StartGodPos, m_SelectNum == 0);
    }

    public void UpdateInputed(bool isInput)
    {
        this.isInput = isInput;

        if (this.isInput)
        {
            m_Player.VFXAppearance.SetActive(false);
            m_God.VFXAppearance.SetActive(false);
        }
    }

    [Button("Delete Character")]
    public void DeleteCharacter()
    {
        this.isInput = false;
        m_Player.VFXAppearance.SetActive(true);
        m_God.VFXAppearance.SetActive(true);
        Destroy(m_Player.gameObject, 3f);
        Destroy(m_God.gameObject, 3f);
    }

    private void Update()
    {
        if (Time.timeScale < 0.1f || !isInput) return;

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
        var playerGo = Instantiate(gameObject);
        CharacterMove characterMove = playerGo.GetComponent<CharacterMove>();

        var hud = Instantiate(m_uiplayerTimerHUDPrefab);
        ObjectCreatorByTime objectCreator = playerGo.GetComponent<ObjectCreatorByTime>();
        objectCreator.updateDeltaTimeEvent.AddListener(hud.GetComponent<UIPlayerTimer>().UpdateDeltaTime);
        hud.GetComponent<TargetFollower>().target = playerGo.transform;

        playerGo.transform.position = pos;
        if (Is2P) playerGo.transform.rotation = Quaternion.Euler(0, 180f, 0);
        playerGo.SetActive(true);
        return characterMove;
    }
}
