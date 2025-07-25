﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ricochet;
using Ricochet.Kinematic;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Rendering.VirtualTexturing;
using Unity.VisualScripting;
using UnityEngine.Windows;
using Cinemachine;

namespace Ricochet.Kinematic
{
    public class Player : MonoBehaviour {
        public CharacterController Character;
        public CharacterCamera characterCamera;

        private PlayerInputHandler playerInputHandler;
        private Stamina m_Stamina;

        void Awake() {
            playerInputHandler = gameObject.GetComponent<PlayerInputHandler>();
            m_Stamina = GetComponent<Stamina>();
        }

        private void Start()
        {
            characterCamera = Camera.main.GetComponent<CharacterCamera>();
            ApplyCamera();
        }

        private void Update()
        {
            HandleCharacterInput(playerInputHandler.MoveInput, playerInputHandler.JumpInput,
                playerInputHandler.DodgeInput && m_Stamina.CanDodge, playerInputHandler.CrouchInput,
                playerInputHandler.FireInput, playerInputHandler.IsInputEnabled);
            RotateWithPlayer();
        }

        private void LateUpdate() {
            HandleCameraInput(playerInputHandler.LookInput, playerInputHandler.IsInputEnabled);
        }

        private void ApplyCamera() {

            Cursor.lockState = CursorLockMode.Locked;
            // Tell camera to follow transform
            characterCamera.SetFollowTransform(Character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            characterCamera.IgnoredColliders.Clear();
            characterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
            
        }

        private void RotateWithPlayer() {

            // Handle rotating the camera along with physics movers
            if(characterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null) {
                characterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * characterCamera.PlanarDirection;
                characterCamera.PlanarDirection = Vector3.ProjectOnPlane(characterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
            }
        }
        private void HandleCameraInput(Vector2 look, bool input) {
            // Create the look input vector for the camera
            float mouseLookAxisUp = look.y;
            float mouseLookAxisRight = look.x;
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            //float scrollInput = -Input.GetAxis(MouseScrollInput);
            float scrollInput = 0;


            // Apply inputs to the camera
            if(input) {
                characterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);
            }
        }

        private void HandleCharacterInput(Vector2 move, bool jump, bool dodge, bool crouch, bool fire, bool input) {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.CameraRotation = characterCamera ? characterCamera.Transform.rotation : Quaternion.identity;
            characterInputs.MoveAxisForward = move.y;
            characterInputs.MoveAxisRight = move.x;
            characterInputs.JumpDown = jump;
            characterInputs.DodgeDown = dodge;
            characterInputs.CrouchDown = crouch;
            characterInputs.CrouchUp = crouch;

            // Apply inputs to character
            if(input) {
                Character.SetInputs(ref characterInputs);
            }
            
        }
    }
}