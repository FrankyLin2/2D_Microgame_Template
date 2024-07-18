using System;
using UnityEngine;
namespace Platformer.Mechanics
{
    public class ObstacleControllerForLight : MonoBehaviour
    {
        private ObstacleController.Mover obstacleMover;
        private ObstaclePath obstaclePath;
        public float chargeTime = 2.0f;
        private float startTime;
        public Sprite ActiveSprite;

        public void OnTriggerEnter2D(Collider2D other)
        {
            var lightController = other.gameObject.GetComponent<LightController>();
            if (lightController != null)
            {
                startTime = Time.time;
            }
        }
                
        public void OnTriggerStay2D(Collider2D other)
        {
            var lightController = other.gameObject.GetComponent<LightController>();
            if (lightController != null)
            {
                if (Time.time - startTime > chargeTime)
                {
                    obstacleMover = new ObstacleController.Mover(obstaclePath, true);
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = ActiveSprite;
                }
            }
        }
        

        
        public void Start()
        {
            obstaclePath = this.gameObject.transform.parent.gameObject.GetComponentInChildren<ObstaclePath>();
        }
        
        public void Update()
        {
            if(obstacleMover != null && obstacleMover.IsFinished())
            {
                obstacleMover = null;
            }
            if (obstacleMover != null)
            {
                obstaclePath.gameObject.transform.position = obstacleMover.GetPosition();
            }
            
        }
    }
}