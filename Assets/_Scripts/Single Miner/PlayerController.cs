using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject[] animObjs;

    private Rigidbody2D rb;
    private bool isAttack = false;
    private Vector3 moveInput;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpPower = 7f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isAttack)
            return;

        int index = moveInput.x == 0 ? 0 : 1;
        SetAnimObject(index);

        if (moveInput.x != 0)
        {
            int dirX = moveInput.x < 0 ? 1 : -1;
            transform.localScale = new Vector3(dirX, 1, 1);

            transform.position += moveInput * moveSpeed * Time.deltaTime;
        }
    }

    void OnMove(InputValue value)
    {
        var moveValue = value.Get<Vector2>();

        moveInput = new Vector3(moveValue.x, 0, 0);
    }

    void OnJump()
    {
        rb.AddForceY(jumpPower, ForceMode2D.Impulse);
    }

    void OnAttack()
    {
        if(!isAttack)
            StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttack = true;
        SetAnimObject(2);

        yield return new WaitForSeconds(1f);

        SetAnimObject(0);
        isAttack = false;
    }

    private void SetAnimObject(int index)
    {
        foreach (var animObj in animObjs)        
            animObj.SetActive(false);
        

        animObjs[index].SetActive(true);
    }
}