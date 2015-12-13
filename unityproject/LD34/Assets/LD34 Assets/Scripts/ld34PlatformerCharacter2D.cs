using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{


    public enum StateInput
    {
        // Things the user can do manually
        Apress,
        Arelease,
        Aheld,
        Bpress,
        Brelease,
        Bheld,
        HoldTimeout,

        // State input that the game is responsible for
        MenuEnd,
        FallOff,
        HitGround,
        JumpingFinished
    }

    // All states available for the player control
    public enum PlayerState
    {
        // Error state, should never be used (only returned in failed getstate calls)
        Error,

        // Movement related
        Grounded,
        Jumping,
        Airborn,
        Fallthrough,

        // Menu related
        AirborneMenu,
        GroundedMenu
    }



    public class PlayerStateTransition
    {
        readonly PlayerState CurrentState;
        readonly StateInput Input;

        public PlayerStateTransition(PlayerState state, StateInput input)
        {
            CurrentState = state;
            Input = input;
        }

        public override int GetHashCode()
        {
            // Magic numbers ahoy!
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Input.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PlayerStateTransition other = obj as PlayerStateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Input == other.Input;
        }

        public static bool isMenuState(PlayerState state)
        {
            if (state == PlayerState.AirborneMenu || state == PlayerState.GroundedMenu)
            {
                return true;
            }
            else return false;
        }
    }


    public class ld34PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField]
        private float m_MaxSpeedStart = 10f;               // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_MaxSpeedGain = 3f;               // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_MaxSpeedFull = 100f;               // The fastest the player can travel in the x axis.


        [SerializeField]
        private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField]
        private float m_JumpForceExtraAdd = 20f;              // Amount of force added when the player holds jump.
        
        private float m_JumpForceExtraCurrent = -1f;              // Amount of force currently added due to holding jump.
        [SerializeField]
        private float m_JumpForceExtraMax = 120f;          // Max amount of force added when the player holds jump.

        [SerializeField]
        private LayerMask m_WhatIsAllGround;                  // A mask determining what is ground to the character
        [SerializeField]
        private LayerMask m_WhatIsSolidGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;

        // State Machine Related
        Dictionary<PlayerStateTransition, PlayerState> transitions;
        public PlayerState CurrentState { get; private set; }
        private int AHeldCount = 0;
        private int BHeldCount = 0;

        private int AHeldMax = -1;
        private int BHeldMax = -1;

        // Menu related


        /// <summary>
        /// Constructor. Also creates/sets the initial state machine
        /// </summary>
        public ld34PlatformerCharacter2D()
        {
            // Players start airborn. Its the easiest way to do things
            CurrentState = PlayerState.Airborn;

            // Lets set up our transitions table, as well
            transitions = new Dictionary<PlayerStateTransition, PlayerState>
            {
                // Airborn
                { new PlayerStateTransition(PlayerState.Airborn, StateInput.HitGround), PlayerState.Grounded },
                { new PlayerStateTransition(PlayerState.Airborn, StateInput.Apress), PlayerState.AirborneMenu },
                { new PlayerStateTransition(PlayerState.Airborn, StateInput.Bpress), PlayerState.Fallthrough },

                // Grounded
                { new PlayerStateTransition(PlayerState.Grounded, StateInput.FallOff), PlayerState.Airborn },
                { new PlayerStateTransition(PlayerState.Grounded, StateInput.Apress), PlayerState.GroundedMenu },
                { new PlayerStateTransition(PlayerState.Grounded, StateInput.Bpress), PlayerState.Jumping },

                // Fallthrough
                { new PlayerStateTransition(PlayerState.Fallthrough, StateInput.Brelease), PlayerState.Airborn },
                { new PlayerStateTransition(PlayerState.Fallthrough, StateInput.Apress), PlayerState.AirborneMenu },
                { new PlayerStateTransition(PlayerState.Fallthrough, StateInput.HitGround), PlayerState.Grounded },

                // Jumping
                { new PlayerStateTransition(PlayerState.Jumping, StateInput.JumpingFinished), PlayerState.Fallthrough },
                { new PlayerStateTransition(PlayerState.Jumping, StateInput.Brelease), PlayerState.Airborn },
                { new PlayerStateTransition(PlayerState.Jumping, StateInput.Bheld), PlayerState.Jumping },
                { new PlayerStateTransition(PlayerState.Jumping, StateInput.Apress), PlayerState.AirborneMenu },

                // Ground Menu
                { new PlayerStateTransition(PlayerState.GroundedMenu, StateInput.FallOff), PlayerState.AirborneMenu },
                { new PlayerStateTransition(PlayerState.GroundedMenu, StateInput.MenuEnd), PlayerState.Grounded },

                // Airborn Menu
                { new PlayerStateTransition(PlayerState.AirborneMenu, StateInput.HitGround), PlayerState.GroundedMenu },
                { new PlayerStateTransition(PlayerState.AirborneMenu, StateInput.MenuEnd), PlayerState.Airborn },
            };
        }

        /// <summary>
        /// Returns the theoretical next state, given the provided input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private PlayerState GetNextState(StateInput input)
        {
            PlayerStateTransition transition = new PlayerStateTransition(CurrentState, input);
            PlayerState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
            {
                // Transition is not a possibility.
                return PlayerState.Error;
            }

            return nextState;
        }

        /// <summary>
        /// Mutates the state machine, based on the provided input
        /// </summary>
        /// <param name="input"></param>
        /// <returns> the current state, after the move</returns>
        public PlayerState MoveNext(StateInput input)
        {
            var nextState = GetNextState(input);

            // Is there an error?
            if (nextState == PlayerState.Error)
            {
                // Is the reason we are in error because we are in a menu?
                if (PlayerStateTransition.isMenuState(CurrentState))
                {
                    // This was a menu state. Perhaps we should pass the input into that function instead?
                    // TODO make the menu react to menu button presses

                    // Just return the state we get when we act upon the menu
                    // CurrentState = CurrentState;
                }
                else
                {
                    // A true error. Do not change the state!
                    // Dont perform any actions either!
                }
            }
            else
            {
                // We have a new, valid state! We should set our current state, and then act depending on our state
                CurrentState = nextState;
                reactToStateChange();
            }

            return CurrentState;
        }

        private void reactToStateChange()
        {
            // We should switch depending on our current state, and then act accordingly
            switch (CurrentState)
            {
                case PlayerState.Error:
                    // Should never reach this...
                    break;
                case PlayerState.Grounded:
                    // We are now grounded!
                    m_JumpForceExtraCurrent = -1;
                    break;
                case PlayerState.Jumping:
                    // We are jumping! Lets perfrom a jump
                    Jump();
                    break;
                case PlayerState.Airborn:
                    // We are now airborn!
                    m_JumpForceExtraCurrent = -1;
                    break;
                case PlayerState.Fallthrough:
                    // We are now trying to fall through things
                    m_JumpForceExtraCurrent = -1;
                    break;
                case PlayerState.AirborneMenu:
                    // We have entered (or continued into) the airborn menu
                    break;
                case PlayerState.GroundedMenu:
                    // We have entered (or continued into) the grounded menu
                    break;
                default:
                    // We should never reach this...
                    break;
            }
        }


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            bool touchingGround = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            var groundToUse = m_WhatIsAllGround;
            if (CurrentState == PlayerState.Fallthrough)
            {
                groundToUse = m_WhatIsSolidGround;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundToUse);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    touchingGround = true;
                }                   
            }

            // Are we not touching ground?
            if (touchingGround == false)
            {
                // We think we fell off of something
                MoveNext(StateInput.FallOff);
            }

            if (touchingGround == true)
            {
                // We think we hit something
                MoveNext(StateInput.HitGround);
            }

            // Set animation speeds?
            m_Anim.SetBool("Ground", touchingGround);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void ReceiveUserInput(StateInput input)
        {
            // We should try to forward the input to the character.
            MoveNext(input);
        }


        public void Jump()
        {
            // Have we added any bonus jump yet?
            if (m_JumpForceExtraCurrent == -1)
            {
                m_JumpForceExtraCurrent = 0;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }            
            // We were jumping previously. Continue adding force?
            else
            {
                // We want to jump but arent on the ground. Try to add a little more velocity
                if (m_JumpForceExtraCurrent <= m_JumpForceExtraMax)
                {
                    m_JumpForceExtraCurrent += m_JumpForceExtraAdd;
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForceExtraAdd));
                }

                // Otherwise, we should exit the jumping state (no reason to be in it)
                MoveNext(StateInput.JumpingFinished);
            }
        }


    }
}
