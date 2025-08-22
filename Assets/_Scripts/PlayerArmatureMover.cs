using StarterAssets;
using TMPro;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerArmatureMover : NetworkBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs starterAsset;
    [SerializeField] private ThirdPersonController controller;
    [SerializeField] private Transform playerRoot;
    [SerializeField] private GameObject bombPrefab;

    private void Awake()
    {
        cc.enabled = false;
        playerInput.enabled = false;
        starterAsset.enabled = false;
        controller.enabled = false;
    }

    private void Update()
    {
        if(!IsOwner)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddScoreServerRPC();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ThrowBombServerRpc();
        }
    }

    [ServerRpc]
    private void AddScoreServerRPC()
    {
        ScoreManager.Instance.AddScore();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        if (IsOwner)
        {
            cc.enabled = true;
            playerInput.enabled = true;
            starterAsset.enabled = true;
            controller.enabled = true;

            var cineMachine = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineCamera>();
            cineMachine.Target.TrackingTarget = playerRoot;
        }
    }


    [ServerRpc]
    private void ThrowBombServerRpc()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
