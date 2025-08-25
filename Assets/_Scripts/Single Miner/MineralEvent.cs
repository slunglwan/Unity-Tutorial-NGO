using Unity.Netcode;
using UnityEngine;

public class MineralEvent : NetworkBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && IsOwner)
        {
            AddScoreServerRPC();
        }
    }

    [ServerRpc]
    private void AddScoreServerRPC()
    {
        NetwokrScoreManager.Instance.AddScore();
        GetComponent<NetworkObject>().Despawn();
    }
}
