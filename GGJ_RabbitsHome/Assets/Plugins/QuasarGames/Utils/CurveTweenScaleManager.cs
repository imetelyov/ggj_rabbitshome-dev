using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QuasarGames.Utils
{

    [System.Serializable]
    public class ObjectToTweenScale
    {
        // one second normalized curve
        public AnimationCurve curve;

        public float animationTime;
        public float delayTime;

        public float currentTime;

        public Transform transform;

        public Vector3 startScale;
        public Vector3 goalScale;

        public bool activateOnMovement;
        public bool deactivateAfterMovement;
    }

    public class CurveTweenScaleManager : MonoBehaviour
    {
        public static CurveTweenScaleManager Instance;

        [SerializeField]
        private bool isChanging = false;

        [SerializeField]
        private List<ObjectToTweenScale> objectsToTweenScale = new List<ObjectToTweenScale>();

        public void Awake()
        {
            Instance = this;
        }

        public void AddObject(Transform transform, Vector3 goalScale, float time = 1f, float delay = 0f, string curveName = "Linear", bool activateOnMovement = false, bool deactivateAfterMovement = false)
        {
            ObjectToTweenScale newObject = new ObjectToTweenScale();

            newObject.curve = CurveLibrary.Instance.GetCurve(curveName);

            newObject.animationTime = time;
            newObject.delayTime = delay;

            newObject.currentTime = 0f;

            newObject.transform = transform;

            newObject.startScale = transform.localScale;
            newObject.goalScale = goalScale;

            newObject.activateOnMovement = activateOnMovement;
            newObject.deactivateAfterMovement = deactivateAfterMovement;

            objectsToTweenScale.Add(newObject);

            isChanging = true;
        }

        public void Reset()
        {
            objectsToTweenScale.Clear();

            isChanging = false;
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;

            if (isChanging)
            {
                if (objectsToTweenScale.Count == 0)
                {
                    isChanging = false;
                    return;
                }

                List<ObjectToTweenScale> objToDelete = new List<ObjectToTweenScale>();

                // Tween
                foreach (ObjectToTweenScale obj in objectsToTweenScale)
                {
                    if (obj.transform == null)
                    {
                        objToDelete.Add(obj);
                        continue;
                    }


                    obj.currentTime += deltaTime;

                    if (obj.currentTime <= obj.delayTime)
                    {
                        // delay fix
                        obj.startScale = obj.transform.localScale;

                        continue;
                    }

                    if (obj.currentTime - obj.delayTime <= obj.animationTime)
                    {
                        // TODO ???
                        if (obj.activateOnMovement && !obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(true);
                        
                        obj.transform.localScale = Vector3.LerpUnclamped(obj.startScale, obj.goalScale, obj.curve.Evaluate((obj.currentTime - obj.delayTime) / obj.animationTime));
                    }
                    else
                    {
                        // TODO ???
                        if (obj.activateOnMovement && !obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(true);

                        if (obj.deactivateAfterMovement && obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(false);

                        obj.transform.localScale = obj.goalScale;

                        objToDelete.Add(obj);
                    }

                }

                // Remove finished tweens
                int delCount = objToDelete.Count;
                if (delCount >= 1)
                {
                    for (int i = delCount; i > 0; i--)
                    {
                        objectsToTweenScale.Remove(objToDelete[i-1]);
                    }
                }
            }

        }
    }
}
