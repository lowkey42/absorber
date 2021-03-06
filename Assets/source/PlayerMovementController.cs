using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlayerMovement))]
public class PlayerMovementController : MonoBehaviour
{
	private PlayerMovement m_Character;
	private bool m_Jump;
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
    }

	public void CopyState(PlayerMovementController other) {
		m_Jump = other.m_Jump;
	}


    private void FixedUpdate()
    {
        // Read the inputs.
		float h = CrossPlatformInputManager.GetAxis("Horizontal_P"+m_PlayerId);
		float v = CrossPlatformInputManager.GetAxis("Vertical_P"+m_PlayerId);
        // Pass all parameters to the character control script.
		m_Character.Move(h, v, m_Jump);
        m_Jump = false;
    }
}
