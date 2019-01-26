using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QuasarGames.Utils
{

    [System.Serializable]
    public class ObjectToTweenRotation
    {
        // one second normalized curve
        public AnimationCurve curve;

        public float animationTime;
        public float delayTime;

        public float currentTime;

        public Transform transform;

        public Quaternion startRotation;
        public Quaternion goalRotation;

        public bool activateOnMovement;
        public bool deactivateAfterMovement;
    }

    public class CurveTweenRotationManager : MonoBehaviour
    {
        public static CurveTweenRotationManager Instance;

        [SerializeField]
        private bool isChanging = false;

        [SerializeField]
        private List<ObjectToTweenRotation> objectsToTweenRotation = new List<ObjectToTweenRotation>();

        public void Awake()
        {
            Instance = this;
        }

        public void AddObject(Transform transform, Quaternion goalRotation, float time = 1f, float delay = 0f, string curveName = "Linear", bool activateOnMovement = false, bool deactivateAfterMovement = false)
        {
            // Check if object is already tweening
            ObjectToTweenRotation foundObject = objectsToTweenRotation.Find(a => a.transform == transform);
            
            if (foundObject == null)
            {
                ObjectToTweenRotation newObject = new ObjectToTweenRotation();

                newObject.curve = CurveLibrary.Instance.GetCurve(curveName);

                newObject.animationTime = time;
                newObject.delayTime = delay;

                newObject.currentTime = 0f;

                newObject.transform = transform;

                newObject.startRotation = transform.localRotation;
                newObject.goalRotation = goalRotation;

                newObject.activateOnMovement = activateOnMovement;
                newObject.deactivateAfterMovement = deactivateAfterMovement;

                objectsToTweenRotation.Add(newObject);
            }
            else
            {
                // object found
                foundObject.curve = CurveLibrary.Instance.GetCurve(curveName);

                foundObject.animationTime = time;
                foundObject.delayTime = delay;

                foundObject.currentTime = 0f;

                foundObject.startRotation = transform.localRotation;
                foundObject.goalRotation = goalRotation;

                foundObject.activateOnMovement = activateOnMovement;
                foundObject.deactivateAfterMovement = deactivateAfterMovement;
            }

            isChanging = true;
        }

        public void Reset()
        {
            objectsToTweenRotation.Clear();

            isChanging = false;
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;

            if (isChanging)
            {
                if (objectsToTweenRotation.Count == 0)
                {
                    isChanging = false;
                    return;
                }

                List<ObjectToTweenRotation> objToDelete = new List<ObjectToTweenRotation>();

                // Tween
                foreach (ObjectToTweenRotation obj in objectsToTweenRotation)
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
                        obj.startRotation = obj.transform.localRotation;
                        continue;
                    }

                    if (obj.currentTime - obj.delayTime <= obj.animationTime)
                    {
                        // TODO ???
                        if (obj.activateOnMovement && !obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(true);

                        obj.transform.localRotation = Quaternion.SlerpUnclamped(obj.startRotation, obj.goalRotation, obj.curve.Evaluate((obj.currentTime - obj.delayTime) / obj.animationTime));
                    }
                    else
                    {
                        // TODO ???
                        if (obj.activateOnMovement && !obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(true);

                        if (obj.deactivateAfterMovement && obj.transform.gameObject.activeSelf)
                            obj.transform.gameObject.SetActive(false);

                        obj.transform.localRotation = obj.goalRotation;

                        objToDelete.Add(obj);
                    }

                }

                // Remove finished tweens
                int delCount = objToDelete.Count;
                if (delCount >= 1)
                {
                    for (int i = delCount; i > 0; i--)
                    {
                        objectsToTweenRotation.Remove(objToDelete[i-1]);
                    }
                }
            }

        }
    }
}
