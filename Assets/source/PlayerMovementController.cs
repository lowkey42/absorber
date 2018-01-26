using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlayerMovement))]
public class PlayerMovementController : MonoBehaviour
{
	private PlayerMovement m_Character;
	private IAction m_Action;
	private bool m_Jump;
	private bool m_Do;
	private bool m_DoReleased = true;
	[SerializeField] private int m_PlayerId = 1;


    private void Awake()
    {
		m_Character = GetComponent<PlayerMovement>();
		m_Action = GetComponent<IAction>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
			m_Jump = CrossPlatformInputManager.GetButtonDown("Vertical_P"+m_PlayerId) || CrossPlatformInputManager.GetButtonDown("Jump_P"+m_PlayerId);
		}

		m_Do = CrossPlatformInputManager.GetButtonDown ("Do_P" + m_PlayerId);
    }

	public void CopyState(PlayerMovementController other) {
		m_Jump = other.m_Jump;
		m_Do = other.m_Do;
		m_DoReleased = other.m_DoReleased;
	}


    private void FixedUpdate()
    {
        // Read the inputs.
		float h = CrossPlatformInputManager.GetAxis("Horizontal_P"+m_PlayerId);
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);

		if (m_Do && m_DoReleased) {
			if (m_Action!=null) {
				m_Action.doAction ();
			}
		}

		m_DoReleased = !m_Do;

        m_Jump = false;
    }
}
