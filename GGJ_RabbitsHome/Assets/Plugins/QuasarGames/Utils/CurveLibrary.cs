using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace QuasarGames.Utils
{

    [System.Serializable]
    public class CurveStyle
    {
        public string name;

        public AnimationCurve curve;
    }

    public class CurveLibrary : MonoBehaviour
    {
        public static CurveLibrary Instance;

        public List<CurveStyle> curves;

        private AnimationCurve linearCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private void Awake()
        {
            Instance = this;
        }


        //GET

        public AnimationCurve GetCurve(string curveName)
        {
            AnimationCurve curve;
            CurveStyle curveStyle = curves.Find(a => a.name == curveName);

            if (curveStyle != null)
            {
                curve = curveStyle.curve;
            }
            else
            {
                curve = linearCurve;
            }

            return curve;
        }

        /// <summary>
        /// Curve value by time (Slow)
        /// </summary>
        /// <param name="curveName"></param>
        /// <param name="curveTime"></param>
        /// <returns></returns>
        public float GetCurveValue(string curveName, float curveTime)
        {
            return GetCurve(curveName).Evaluate(Mathf.Clamp01(curveTime));
        }
    }
}