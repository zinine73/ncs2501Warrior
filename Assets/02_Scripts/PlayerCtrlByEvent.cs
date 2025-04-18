using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrlByEvent : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction attackAction;
    private Animator anim;
    private Vector3 moveDir;
    private void Start()
    {
        anim = GetComponent<Animator>();
        
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
        .With("Up", "<keyboard>/w")
        .With("Down", "<keyboard>/s")
        .With("Left", "<keyboard>/a")
        .With("Right", "<keyboard>/d");
        moveAction.performed += ctx => {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0, dir.y);
            anim.SetFloat("Movement", dir.magnitude);
        };
        moveAction.canceled += ctx => {
            moveDir = Vector3.zero;
            anim.SetFloat("Movement", 0.0f);
        };
        moveAction.Enable();

        attackAction = new InputAction("Attack",
                                    InputActionType.Button,
                                    "<keyboard>/space");
        attackAction.performed += ctx => {
            anim.SetTrigger("Attack");
        };
        attackAction.Enable();
    }

    private void Update()
    {
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
    }
}
