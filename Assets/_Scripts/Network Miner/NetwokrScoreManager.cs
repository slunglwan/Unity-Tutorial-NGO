using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetwokrScoreManager : NetworkBehaviour
{
    public static NetwokrScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreUI;

    private NetworkVariable<int> score = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        score.OnValueChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int prevValue, int newValue)
    {
        scoreUI.text = newValue.ToString();
    }

    public void AddScore()
    {
        if(!IsServer)
            return;

        score.Value++;
    }
}
