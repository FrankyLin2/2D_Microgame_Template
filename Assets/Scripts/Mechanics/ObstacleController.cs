using System;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ObstacleController : MonoBehaviour
    {
        private Mover mover;
        private ButtonPath path;
        private Mover obstacleMover;
        private ObstaclePath obstaclePath;
        public class Mover
        {
            float p = 0;
            float duration;
            float startTime;
            bool bForward = true;
            Vector2 startPostion, endPosition;
            public Mover(PatrolPath patrolPath, bool bForward)
            {
                var position = patrolPath.gameObject.transform.position;
                Vector2 currentPos = new Vector2(position.x, position.y);
                this.bForward = bForward;
                if (this.bForward)
                {
                    this.startPostion = currentPos;
                    this.endPosition = patrolPath.pathPosition + patrolPath.endPosition;
                    this.duration = (startPostion - endPosition).magnitude / patrolPath.speed;
                }
                else
                {
                    this.startPostion = currentPos;
                    this.endPosition = patrolPath.pathPosition + patrolPath.startPosition;
                    this.duration = (startPostion - endPosition).magnitude / patrolPath.speed;
                }
                this.startTime = Time.time;
            }

            public Vector2 GetPosition()
            {
                var t = Mathf.Clamp(Time.time - startTime, 0, duration) / duration;
                return  Vector2.Lerp(startPostion, endPosition, t);
            }
            // check if the movement is finished
            public bool IsFinished()
            {
                return Time.time - startTime > duration;
            }




        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            // var lightController = other.gameObject.GetComponent<LightController>();
            var player = other.gameObject.GetComponent<PlayerController>();
            //get the obstacle from child game object
            
            if (player != null)
            {
                this.mover = new Mover(path, true);
                obstacleMover = new Mover(obstaclePath, true);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            // var lightController = other.gameObject.GetComponent<LightController>();
            var player = other.gameObject.GetComponent<PlayerController>();
            //get the obstacle from child game object
            
            if (player != null)
            {
                this.mover = new Mover(path, false);
                obstacleMover = new Mover(obstaclePath, false);
            }
        }

        public void Start()
        {
            obstaclePath = this.gameObject.transform.parent.gameObject.GetComponentInChildren<ObstaclePath>();
            path = this.gameObject.GetComponent<ButtonPath>();
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
            if (mover != null && mover.IsFinished())
            {
                mover = null;
            }
            if (mover != null)
            {
                path.gameObject.transform.position = mover.GetPosition();
            }
            
            
        }
    }
}