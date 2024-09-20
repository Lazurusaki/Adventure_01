using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InputDetector
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const KeyCode JumpButton = KeyCode.Space;
    private const KeyCode RestartButton = KeyCode.R;

    public Vector2 AxisInput { get; private set; }
    public bool IsRestarting { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;

    public void Update()
    {    
        AxisInput = new Vector2(Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName)).normalized;

        if (Input.GetKeyDown(JumpButton))
            IsJumping = true;
        else
            IsJumping = false;

        if (Input.GetKeyDown(RestartButton))
            IsRestarting = true;
        else
            IsRestarting = false;       
    }

    public KeyCode GetRestartKey()
    {
        return RestartButton;
    }
}
