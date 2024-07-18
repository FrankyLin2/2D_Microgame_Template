using UnityEngine;

namespace Platformer.Mechanics
{
    public class ButtonPath : PatrolPath
    { 
        void Start()
        {
            var position = this.gameObject.transform.position;
            pathPosition = new Vector2(position.x, position.y);
        }
    }
}