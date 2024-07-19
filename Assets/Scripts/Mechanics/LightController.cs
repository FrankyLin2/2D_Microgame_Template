using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    
    [RequireComponent(typeof(TrailRenderer))]
    public class LightController : MonoBehaviour
    {
        public float maxSpeed = 5f;  // Maximum speed the object can move
        private Vector3 targetPosition;
        private TrailRenderer mytrailRenderer;
        private Rigidbody2D rb;
        public float speedMultiplier = 2.0f;
        public float maxdistance = 8.0f;
        
        // for splatting
        private bool bReadyCached = false;
        private bool bReadySplatted = false;
        private bool bSplatted = false;
        private PlayerController player;
        void Start()
        {
            mytrailRenderer = GetComponent<TrailRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            if(!bSplatted && !bReadySplatted)
                FollowMouse();
        }

        /*void FollowMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 dir = mousePosition - this.transform.position;
            float distance = dir.magnitude;
            rb.velocity = speedMultiplier * Mathf.Min(distance, maxSpeed) * dir.normalized;
            

        }*/
        void FollowMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the z position is zero for 2D

            Vector2 dir = (mousePosition - transform.position).normalized;
            float distance = Vector2.Distance(mousePosition, transform.position);
            rb.velocity = speedMultiplier * Mathf.Min(distance, maxSpeed) * dir;

            // Calculate the screen boundaries in world space for 2D
            Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            // Convert screen boundaries to Vector2
            Vector2 screenBottomLeft2D = new Vector2(screenBottomLeft.x, screenBottomLeft.y);
            Vector2 screenTopRight2D = new Vector2(screenTopRight.x, screenTopRight.y);

            // Clamp the object's position to stay within the screen boundaries
            Vector2 newPosition = rb.position + rb.velocity * Time.fixedDeltaTime; // Use rb.position for 2D Rigidbody
            newPosition.x = Mathf.Clamp(newPosition.x, screenBottomLeft2D.x, screenTopRight2D.x);
            newPosition.y = Mathf.Clamp(newPosition.y, screenBottomLeft2D.y, screenTopRight2D.y);

            rb.MovePosition(newPosition); // Move the Rigidbody2D
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                bReadyCached = true;
            }
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                bReadyCached = false;
            }
        }


        public void Update()
        {
            if (bReadyCached)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    bReadySplatted = true;
                    bReadyCached = false;
                    rb.isKinematic = false;
                }
            }

            if (bReadySplatted)
            {
                var model = Simulation.GetModel<PlatformerModel>();
                player = model.player;
                if (Input.GetButtonUp("Fire1"))
                {
                    bSplatted = true;
                    bReadySplatted = false;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0f; // Ensure the z position is zero for 2D

                    Vector2 dir = (mousePosition - transform.position).normalized;
                    rb.velocity = maxSpeed * dir.normalized;
                }
                else
                {
                    this.transform.position = player.transform.GetChild(0).position;
                }
                
            }

            if (bSplatted)
            {
                if ((player.transform.position - transform.position).magnitude > maxdistance || rb.velocity.magnitude < 0.1f)
                {
                    bSplatted = false;
                    RigidTeleportLight(player.transform.position);

                }

            }


        }
        
        public void TeleportLight(Vector3 position)
        {
            var rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            this.gameObject.transform.position = position;
            rb.isKinematic = false;
        }
        
        public void RigidTeleportLight(Vector3 position)
        {
            var rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.MovePosition(position);
            rb.isKinematic = false;
        }
    }
}