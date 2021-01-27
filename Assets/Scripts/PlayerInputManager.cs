using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerInputManager : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    void Start()
    {

    }
    public bool GetJumpDown()
    {
        return Input.GetButtonDown(GameConstants.k_ButtonNameJump);
    }

    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f, Input.GetAxisRaw(GameConstants.k_AxisNameVertical));
        move = Vector3.ClampMagnitude(move, 1);
        return move;
    }

    public bool GetFireDown()
    {
        return Input.GetButtonDown(GameConstants.k_ButtonNameFire);
    }

    public float GetLookInputsHorizontal()
    {
        return GetMouseOrStickLookAxis(GameConstants.k_HorizontalLookMouse, GameConstants.k_HorizontalLookStick);
    }

    public float GetLookInputsVertical()
    {
        return GetMouseOrStickLookAxis(GameConstants.k_VerticalLookMouse, GameConstants.k_VerticalLookStick);
    }

    float GetMouseOrStickLookAxis(string mouseInputName, string stickInputName)
    {
        // Check if this look input is coming from the mouse
        bool isGamepad = Input.GetAxis(stickInputName) != 0f;
        float i = isGamepad ? Input.GetAxis(stickInputName) : Input.GetAxisRaw(mouseInputName);

        // apply sensitivity multiplier
        i *= mouseSensitivity;

        if (isGamepad)
        {
            // since mouse input is already deltaTime-dependant, only scale input with frame time if it's coming from sticks
            i *= Time.deltaTime;
        }
        else
        {
            // reduce mouse input amount to be equivalent to stick movement
            i *= 0.01f;
#if UNITY_WEBGL
                // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                i *= webglLookSensitivityMultiplier;
#endif
        }

        return i;
    }

}