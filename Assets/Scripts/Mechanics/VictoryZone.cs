using Platformer.Gameplay;
using Platformer.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        public int nextScene = 0;
        public VideoClip videoClip;

        [SerializeField] private VideoPlayer vp;

        [SerializeField] private bool playedOnce = false;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //play video
                if (Camera.main != null && vp == null && !playedOnce)
                {
                    Camera.main.gameObject.AddComponent<VideoPlayer>();
                    var videoPlayer = Camera.main.GetComponent<VideoPlayer>();
                    videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
                    videoPlayer.clip = videoClip;
                    videoPlayer.loopPointReached += OnVideoFinished;
                    vp = videoPlayer;
                    videoPlayer.Play();
                }
            }
        }
        
        /*void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<LightController>();
            if (p != null)
            {
                //play video
                if (Camera.main != null)
                {
                    Camera.main.gameObject.AddComponent<VideoPlayer>();
                    var videoPlayer = Camera.main.GetComponent<VideoPlayer>();
                    videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
                    videoPlayer.clip = videoClip;
                    videoPlayer.loopPointReached += OnVideoFinished;
                    videoPlayer.Play();
                    
                }

                // after video is finished, load next scene, packaged as a event
                
                
                // var ev = Schedule<PlayerEnteredVictoryZone>();
                // ev.victoryZone = this;
                
            }
        }*/
        
        public void OnVideoFinished(VideoPlayer thisPlay)
        {
            // SceneManager.LoadScene(nextScene);
            /*var gameController = GameObject.Find("GameController").GetComponent<MetaGameController>();
            gameController.ToggleMainMenu(true);
            myPlayer.SetActive(true); 
            startButton.SetActive(false);*/
            playedOnce = true;
            vp.enabled = false;
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = false;
            this.enabled = false;

        }
        
    }
    
}