using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlayerMovement))]
public class PlayerMovementController : MonoBehaviour
{
	private PlayerMovement m_Character;
	private bool m_Jump;
	private bool m_Do;
	[SerializeField] private int m_PlayerId = 1;


    private void Awake()
    {
		m_Character = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
			m_Jump = CrossPlatformInputManager.GetButtonDown("Vertical_P"+m_PlayerId) || CrossPlatformInputManager.GetButtonDown("Jump_P"+m_PlayerId);
		}

		if (!m_Do)
		{
			m_Do = CrossPlatformInputManager.GetButtonDown("Do_P"+m_PlayerId);
		}
    }


    private void FixedUpdate()
    {
        // Read the inputs.
		float h = CrossPlatformInputManager.GetAxis("Horizontal_P"+m_PlayerId);
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);

		// TODO: process m_Do

        m_Jump = false;
    }
}
