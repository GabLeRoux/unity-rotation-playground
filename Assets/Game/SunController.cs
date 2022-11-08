using UnityEngine;

namespace Game
{
    // source: https://stackoverflow.com/a/73438676/1092815
    // Andrew Łukasik
    public class SunController : MonoBehaviour
    {
        // 0.5 means noon
        [SerializeField] [Range(0, 1)] private float dayTime = 0.5f;
        [SerializeField] private float timeScale = 1;
        // make sure it is directional
        [SerializeField] private Light sun;
        // degrees
        [SerializeField] private Vector2 sunAtMidnight = new(45, 0);

        // degrees, axis relative to your location (so pointing straight up if you are on a N or S pole)
        [SerializeField] private Vector2 earthRotationAxis = new(-45, 0);

        private const float DegreesPerSecond = 360f / SecondsPerDay;
        private const float SecondsPerDay = 24 * 60 * 60; // seconds per day

        [SerializeField] private AnimationCurve sunIntensity = new(
            new Keyframe(0, 0),
            new Keyframe(0.25f, 0),
            new Keyframe(0.5f, 1),
            new Keyframe(0.75f, 0),
            new Keyframe(1, 0)
        );

        [SerializeField] private Gradient sunColor = new()
        {
            colorKeys = new[]
            {
                new GradientColorKey(new Color(1, 0.3f, 0, 1), 0),
                new GradientColorKey(Color.white, 0.5f),
                new GradientColorKey(new Color(1, 0.3f, 0, 1), 1)
            }
        };

        private void FixedUpdate()
        {
            // step simulation time:
            dayTime = (dayTime + (Time.fixedDeltaTime * timeScale) / SecondsPerDay) % 1f;

            // update directional light:
            var sunPositionNow = SunPositionAtTime(dayTime);
            sun.transform.rotation = Quaternion.LookRotation(-sunPositionNow);
            sun.intensity = sunIntensity.Evaluate(dayTime);
            sun.color = sunColor.Evaluate(dayTime);
            sun.enabled = sun.intensity > 0;
        }

        /// <param name="t">0-1 value range</param>
        private Vector3 SunPositionAtTime(float t)
        {
            var midnightDir = Quaternion.Euler(sunAtMidnight) * Vector3.forward;
            var earthAxisDir = Quaternion.Euler(earthRotationAxis) * Vector3.forward;
            return Quaternion.AngleAxis(t * SecondsPerDay * DegreesPerSecond, earthAxisDir) * midnightDir;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var position = transform.position;

            Gizmos.color = Color.cyan * 0.3f;
            Gizmos.DrawRay(position, Quaternion.Euler(earthRotationAxis) * Vector3.forward);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(position, Quaternion.Euler(sunAtMidnight) * Vector3.forward);

            if (Application.isPlaying)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawRay(position, SunPositionAtTime(dayTime));
            }

            const int numSteps = 100;
            for (var i = 0; i <= numSteps; i++)
            {
                var t = i / (float) numSteps;
                Gizmos.color = sunColor.Evaluate(t) * new Color(1, 1, 1, Mathf.Max(sunIntensity.Evaluate(t), 0.05f));
                Gizmos.DrawRay(position, SunPositionAtTime(t));
            }

            Gizmos.color = Color.yellow * 0.3f;
            Gizmos.DrawWireSphere(position, 1f);

            var dayTimeSeconds = (int) (dayTime * SecondsPerDay);
            var h = dayTimeSeconds / (60 * 60);
            var m = (dayTimeSeconds % (60 * 60)) / 60;
            var s = dayTimeSeconds % 60;
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.Label(position, $"time of day: {h:00.}:{m:00.}:{s:00.}");
        }
#endif
    }
}