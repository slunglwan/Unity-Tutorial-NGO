using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreTextUI;

    private NetworkVariable<int> globalScore = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkDespawn()
    {
        globalScore.OnValueChanged -= OnScoreChanged;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        globalScore.OnValueChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int preValue, int newValue)
    {
        scoreTextUI.text = newValue.ToString();
    }

    public void AddScore()
    {
        if (!IsServer)
            return;

        globalScore.Value++;
    }
}
