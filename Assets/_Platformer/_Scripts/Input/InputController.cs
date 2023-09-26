using UnityEngine;

public class InputController : MonoBehaviour
{
    public float X { get; private set; }
    public float Y { get; private set; }
    
    public bool HasPressedAttackButton { get; private set; }
    public bool HasPressedJumpButton { get; private set; }

    private void Update()
    {
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
        
        HasPressedAttackButton = Input.GetButtonDown("Fire1");
        HasPressedJumpButton = Input.GetButtonDown("Jump");
    }
}

