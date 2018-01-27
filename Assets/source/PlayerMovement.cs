using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
	[SerializeField] private float m_InertiaFactor = 0;
	[SerializeField] private bool m_Fly = false;
	[SerializeField] private bool m_ForceFly = false;

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
	private GameplaySystem m_GameplaySys;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private float m_FlyAngle = 90*Mathf.Deg2Rad;
	private float m_FlyTargetAngle = 90*Mathf.Deg2Rad;

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_GameplaySys = GameObject.FindObjectOfType<GameplaySystem> ();
	}

	public void CopyState(PlayerMovement other) {
/*		m_FacingRight = other.m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= Mathf.Sign(other.transform.localScale.x);
		transform.localScale = theScale;
*/	}

	public void SetFly(bool b) {
		m_Fly = b;

		var vel = m_Rigidbody2D.velocity;
		if (vel.magnitude > 0.0001f) {
			vel.Normalize ();
			m_FlyAngle = m_FlyTargetAngle = Mathf.Atan2 (-vel.y, vel.x);
		}
	}

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

		if (m_Anim) {
			m_Anim.SetBool ("Ground", m_Grounded);

			// Set the vertical animation
			m_Anim.SetFloat ("vSpeed", m_Rigidbody2D.velocity.y/m_MaxSpeed);
		}
    }


	public void Move(float move_x, float move_y, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
		{
			if (m_Anim) {
				// The Speed animator parameter is set to the absolute value of the horizontal input.
				m_Anim.SetFloat ("Speed", Mathf.Abs (move_x));
			}

            // Move the character
			if (m_Fly) {
				var dir = new Vector2 (move_x, move_y);
				if (dir.magnitude > 0.01f) {
					dir.Normalize ();
					m_FlyTargetAngle = Mathf.Atan2 (-dir.y, dir.x);
				}

				if (dir.magnitude > 0.01f || m_ForceFly) {
					if (Mathf.Abs (m_FlyTargetAngle - m_FlyAngle) > 0.00001f) {
						m_FlyAngle += Mathf.DeltaAngle (m_FlyAngle * Mathf.Rad2Deg, m_FlyTargetAngle * Mathf.Rad2Deg) * Mathf.Deg2Rad * Mathf.Min (1f, Time.deltaTime * 4);
					}

					m_Rigidbody2D.velocity = new Vector2 (Mathf.Cos (m_FlyAngle), Mathf.Sin (m_FlyAngle)) * m_MaxSpeed;
				}

			} else {
				var vel_x = move_x * m_MaxSpeed;

				if (m_InertiaFactor > 0) {
					vel_x = Mathf.Lerp (m_Rigidbody2D.velocity.x, vel_x, Time.deltaTime * 10*(1 - m_InertiaFactor));
				}

				m_Rigidbody2D.velocity = new Vector2 (vel_x, m_Rigidbody2D.velocity.y);
			}

            // If the input is moving the player right and the player is facing left...
			if (move_x > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
			else if (move_x < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
		if (m_Grounded && jump && !m_Fly)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
			if (m_Anim) {
				m_Anim.SetBool ("Ground", false);
			}
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			if(m_GameplaySys!=null)
				m_GameplaySys.PlayJumpSoundEffect();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
