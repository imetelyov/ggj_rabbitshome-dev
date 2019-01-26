using UnityEngine;
using System.Collections;
using TMPro;

namespace QuasarGames.Utils
{
    public class FPSCounter : MonoBehaviour
    {

        /* Public Variables */
        public TextMeshProUGUI fpsText;

        public float frequency = 0.5f;

        /* **********************************************************************
         * PROPERTIES
         * *********************************************************************/
        public int framesPerSec;

        private int minFramesPerSec;
        private int avgFramesPerSec;
        private int maxFramesPerSec;

        private int avgFirstFrame;
        private float avgStartTime;
        private bool isCounting;

        private void Start()
        {
            isCounting = false;
            avgFirstFrame = 0;
            avgStartTime = 1.0f;
            avgFramesPerSec = 0;
            minFramesPerSec = 1;
            maxFramesPerSec = 1;

            StartFPSCount();
        }


        public void StartFPSCount()
        {
            isCounting = true;

            avgFramesPerSec = 0;
            minFramesPerSec = 1000;
            maxFramesPerSec = 1;

            avgFirstFrame = Time.frameCount;
            avgStartTime = Time.realtimeSinceStartup;

            StartCoroutine(FPS());
        }

        public void StopFPSCount()
        {
            if (isCounting)
            {
                isCounting = false;
                StopCoroutine("FPS");

                float avgTime = Time.realtimeSinceStartup - avgStartTime;
                int avgframeCount = Time.frameCount - avgFirstFrame;

                avgFramesPerSec = Mathf.RoundToInt((float)avgframeCount / avgTime);

                //Debug.Log("min " + minFramesPerSec);
                //Debug.Log("avg " + avgFramesPerSec);
                //Debug.Log("max " + maxFramesPerSec);
            }

        }

        private IEnumerator FPS()
        {
            while (isCounting)
            {
                // Capture frame-per-second
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSeconds(frequency);


                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;
                // Display it
                framesPerSec = Mathf.RoundToInt((float)frameCount / timeSpan);

                if (framesPerSec > maxFramesPerSec)
                    maxFramesPerSec = framesPerSec;

                if (framesPerSec < minFramesPerSec)
                    minFramesPerSec = framesPerSec;

                float avgTime = Time.realtimeSinceStartup - avgStartTime;
                int avgframeCount = Time.frameCount - avgFirstFrame;

                avgFramesPerSec = Mathf.RoundToInt((float)avgframeCount / avgTime);


                var stringBuilder = new System.Text.StringBuilder();

                stringBuilder.Append("min:");
                stringBuilder.Append(minFramesPerSec.ToString());
                stringBuilder.Append("\navg:");
                stringBuilder.Append(avgFramesPerSec.ToString());
                stringBuilder.Append("\nmax:");
                stringBuilder.Append(maxFramesPerSec.ToString());
                stringBuilder.Append("\ncur:");
                stringBuilder.Append(framesPerSec.ToString());

                fpsText.text = stringBuilder.ToString();


            }


        }
    }
}
