using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace QuasarGames
{


    /// <summary>
    /// Class, which contains statics tools methods
    /// </summary>
    public class Tools
    {

        /// <summary>
        /// Right buttom camera edge
        /// </summary>
        public static Vector3 RBCamPos
        {
            get { return Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)); }
        }



        /// <summary>
        /// Return true if vectors are equal
        /// </summary>
        /// <param name="vec1">First vector</param>
        /// <param name="vec2">Second vector</param>
        public static bool IsVectors2DEqual(Vector2 vec1, Vector2 vec2)
        {
            if (Mathf.Approximately(vec1.x, vec2.x) && Mathf.Approximately(vec1.y, vec2.y))
                return true;
            
            return false;      
        }

        public static Transform SpawnObj(Transform spawnedPrefab, string name, Transform parent)
        {
            Transform spawnObj = GameObject.Instantiate(spawnedPrefab, spawnedPrefab.position, Quaternion.identity) as Transform;
            spawnObj.name = name;
            spawnObj.SetParent(parent);
            spawnObj.gameObject.SetActive(false);
            return spawnObj;
        }


        #region ChangeColor methods
        /// <summary>
        /// Change color in smooth animation
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        public static IEnumerator ChangeColor(SpriteRenderer target, Color color, float duration)
        {
            //Debug.Log("Color change");
            float timeElapsed = 0f;
            Color startColor = target.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }

        /// <summary>
        /// Change color in smooth animation
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        public static IEnumerator ChangeColor(Text target, Color color, float duration)
        {
            float timeElapsed = 0f;
            Color startColor = target.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }

        /// <summary>
        /// Change color in smooth animation
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        public static IEnumerator ChangeColor(Image target, Color color, float duration)
        {
            //Debug.Log("Color change");
            float timeElapsed = 0f;
            Color startColor = target.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }


        /// <summary>
        /// Change color in smooth animation
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        public static IEnumerator ChangeColor(MeshRenderer target, Color color, float duration)
        {
            //Debug.Log("Color change");
            float timeElapsed = 0f;
            Color startColor = target.material.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.material.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }

        /// <summary>
        /// Change color in smooth animation after wait
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        /// <param name="waitTime">Duration of wait time before animation</param> 
        public static IEnumerator WaitAndChangeColor(SpriteRenderer target, Color color, float duration, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            //Debug.Log("Color change");
            float timeElapsed = 0f;
            Color startColor = target.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }

        /// <summary>
        /// Change color in smooth animation after wait
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        /// <param name="waitTime">Duration of wait time before animation</param> 
        public static IEnumerator WaitAndChangeColor(Text target, Color color, float duration, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            float timeElapsed = 0f;
            Color startColor = target.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }

        /// <summary>
        /// Change color in smooth animation after wait
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        /// <param name="waitTime">Duration of wait time before animation</param> 
        public static IEnumerator WaitAndChangeColor(Image target, Color color, float duration, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            //Debug.Log("Color change");
            float timeElapsed = 0f;
            Color startColor = target.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }


        /// <summary>
        /// Change color in smooth animation after wait
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="color">End color of animation</param>
        /// <param name="duration">Duration of color animation</param> 
        /// <param name="waitTime">Duration of wait time before animation</param> 
        public static IEnumerator WaitAndChangeColor(MeshRenderer target, Color color, float duration, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            //Debug.Log("Color change");
            float timeElapsed = 0f;
            Color startColor = target.material.color;
            Color endColor = color;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                target.material.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                yield return null;
            }
        }

        #endregion

    }
}
