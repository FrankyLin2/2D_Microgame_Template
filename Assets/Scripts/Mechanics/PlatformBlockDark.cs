using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// PlatformBlockDark can switch between walkable and unwalkable,
    /// This block is walkable when there's no light
    /// </summary>
    public class PlatformBlockDark : MonoBehaviour
    {
        [SerializeField] private bool isCurrentlyWalkable = true;
        private BoxCollider2D nonTriggerCollider;
        private BoxCollider2D triggerCollider;
        private bool isLightControllerColliding = false;

        void Start()
        {
            // Assume the first BoxCollider2D is non-trigger and the second is trigger
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
            nonTriggerCollider = colliders[0];
            triggerCollider = colliders[1];
            
            nonTriggerCollider.isTrigger = false;
            triggerCollider.isTrigger = true;

            nonTriggerCollider.enabled = isCurrentlyWalkable;
        }

        /// <summary>
        /// Adjust the platform block's status to walkable or unwalkable.
        /// </summary>
        /// <param name="newStatus">The status we want to set to</param>
        public void SwitchStatus(bool newStatus)
        {
            //Debug.Log("PB.switchstatus is called");
            isCurrentlyWalkable = newStatus;
            nonTriggerCollider.enabled = isCurrentlyWalkable;
        }

        /// <summary>
        /// When a light enters the trigger zone, turn this platform into unwalkable
        /// </summary>
        /// <param name="other">colliding object</param>
        /// <returns></returns>
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("LightRing") && !isLightControllerColliding)
            {
                isLightControllerColliding = true;
                //Debug.Log("pb: OnTriggerEnter2D colliding with light");
                SwitchStatus(false);
            }
        }

        /// <summary>
        /// When a light exits the trigger zone, turn this platform into walkable
        /// </summary>
        /// <param name="other">colliding object</param>
        /// <returns></returns>
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("LightRing") && isLightControllerColliding)
            {
                isLightControllerColliding = false;
                //Debug.Log("pb: OnTriggerExit2D light exits");
                SwitchStatus(true);
            }
        }
        
    }
}