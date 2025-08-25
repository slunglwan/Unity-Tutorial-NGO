using UnityEngine;
using UnityEngine.Tilemaps;

public class DigEvent : MonoBehaviour
{
    private NetworkTilemap networkTilemap;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private Transform[] hitPoints;

    private void Awake()
    {
        networkTilemap = FindFirstObjectByType<NetworkTilemap>();
    }

    public void OnDig()
    {
        for (int i = 0; i < hitPoints.Length; i++)
        {
            Collider2D coll = Physics2D.OverlapCircle(hitPoints[i].position, 0.01f, tileLayer);

            if (coll != null)
            {
                networkTilemap.RemoveTile(hitPoints[i].position);
                
                break;
            }
        }
    }
}
