using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            player.collider2d.enabled = true;
            player.controlEnabled = false;
            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);
            player.health.Increment();
            player.Teleport(model.checkpoint.transform.position);

            var lightGO = model.lightObject;
            var lc = lightGO.GetComponent<LightController>();
            if (lc != null)
            {
                lc.TeleportLight(model.checkpoint.transform.position);
            }
            player.jumpState = PlayerController.JumpState.Grounded;
            player.animator.SetBool("dead", false);
            model.virtualCamera.m_Follow = player.transform.GetChild(0);
            model.virtualCamera.m_LookAt = player.transform.GetChild(0);
            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}