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
        
        void Start()
        {
            mytrailRenderer = GetComponent<TrailRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            FollowMouse();
        }

        void FollowMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 dir = mousePosition - this.transform.position;
            float distance = dir.magnitude;
            rb.velocity = speedMultiplier * Mathf.Min(distance, maxSpeed) * dir.normalized;
            

        }
        
        /*void OnCollisionEnter2D(Collision2D collision)
        {
            
            Debug.Log("LightController: OnCollisionEnter2D ");
            var other = collision.gameObject.GetComponent<PlatformBlock>();
            if (other != null)
            {
                Debug.Log("LightController:OnCollisionEnter2D colliding with pb");
                var ev = Schedule<LightSourceCollision>();
                ev.playerLight = this;
                ev.pb = other;
                ev.colType = LightSourceCollision.CollisionType.Enter;
            }
        }
        
        void OnCollisionExit2D(Collision2D collision)
        {
            
            Debug.Log("LightController: OnCollisionExit2D");
            var other = collision.gameObject.GetComponent<PlatformBlock>();
            if (other != null)
            {
                Debug.Log("LightController:OnCollisionExit2D colliding with pb ends");
                var ev = Schedule<LightSourceCollision>();
                ev.playerLight = this;
                ev.pb = other;
                ev.colType = LightSourceCollision.CollisionType.Exit;
            }
        }*/
    }
}