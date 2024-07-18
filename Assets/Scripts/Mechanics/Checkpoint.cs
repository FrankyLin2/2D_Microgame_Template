using UnityEngine;
using Platformer.Model;
using Platformer.Core;
namespace Platformer.Mechanics
{
    public class Checkpoint : MonoBehaviour
    {
        public Sprite activeSprite;
        public void OnTriggerEnter2D(Collider2D collider)
        {
            PlatformerModel model = Simulation.GetModel<PlatformerModel>();
            var player = collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                model.checkpoint = transform;
            }
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if(activeSprite != null)
                    spriteRenderer.sprite = activeSprite;
                else
                    Debug.LogWarning("Checkpoint is missing active sprite");
            }
        }
    }
}