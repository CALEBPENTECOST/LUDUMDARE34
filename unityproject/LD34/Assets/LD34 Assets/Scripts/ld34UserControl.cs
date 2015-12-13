using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (ld34PlatformerCharacter2D))]
    public class ld34UserControl : MonoBehaviour
    {
        private ld34PlatformerCharacter2D m_Character;

        // We need to keep the current state of the input buttons
        private bool aHeld = false;
        private bool bHeld = false;
        
        /// <summary>
        /// When the control awakens, it needs to get the character it is associated with
        /// </summary>
        private void Awake()
        {
            m_Character = GetComponent<ld34PlatformerCharacter2D>();
        }

        /// <summary>
        /// On every update, we need to take the user input and determine if the user has pressed or released any buttons
        /// </summary>
        private void Update()
        {

            var buttonAPressed = CrossPlatformInputManager.GetButton("A");
            var buttonAReleased = CrossPlatformInputManager.GetButtonUp("A");

            var buttonBPressed = CrossPlatformInputManager.GetButton("B");
            var buttonBReleased = CrossPlatformInputManager.GetButtonUp("B");

            //if(buttonAPressed || buttonBPressed)
            //Debug.Log("ld34 Buttons pressed: " + (buttonAPressed?"A ":"") + (buttonBPressed?"B":""));

            // Attempt to perform an action on the B button
            PerformButtonAction(
                buttonBPressed, buttonBReleased, ref bHeld, StateInput.Brelease, StateInput.Bpress, StateInput.Bheld);

            // Attempt to perform an action on the A button
            PerformButtonAction(
                buttonAPressed, buttonAReleased, ref aHeld, StateInput.Arelease, StateInput.Apress, StateInput.Aheld);

            // We now need to see if there is anything special we need to do as far as the states go?


        }

        /// <summary>
        /// Attempts to perform an action (and mutate the state) depending on the buttons provided
        /// </summary>
        /// <param name="buttonPressed"></param>
        /// <param name="buttonReleased"></param>
        /// <param name="buttonCurrentlyHeld"></param>
        /// <param name="stateInputOnRelease"></param>
        /// <param name="stateInputOnPress"></param>
        /// <returns></returns>
        private void PerformButtonAction(
    bool buttonPressed, bool buttonReleased,
    ref bool buttonCurrentlyHeld,
    StateInput stateInputOnRelease, StateInput stateInputOnPress, StateInput stateInputOnHeld)
        {
            if (buttonCurrentlyHeld)
            {
                // Is the button up now?
                if (buttonReleased)
                {
                    // We should send a "released" event
                    m_Character.ReceiveUserInput(stateInputOnRelease);
                    //MoveNext(stateInputOnRelease);
                    buttonCurrentlyHeld = false;
                    return;
                }
                else
                {
                    // Was held, not released. Send a button held event?
                    m_Character.ReceiveUserInput(stateInputOnHeld);
                    return;
                }
            }
            else
            {
                // Is the button down now?
                if (buttonPressed)
                {
                    // We should send a "pressed" event
                    m_Character.ReceiveUserInput(stateInputOnPress);
                    //MoveNext(stateInputOnPress);
                    buttonCurrentlyHeld = true;
                    return;
                }
                else
                {
                    // Was not held, not pressed, no action required
                    return;
                }
            }
        }




        private void FixedUpdate()
        {
            //Debug.Log("ld34 FixedUpdate");
            /*
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
            //*/
        }
    }
}
