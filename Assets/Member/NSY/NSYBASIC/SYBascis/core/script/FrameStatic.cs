using UnityEngine;
using System.Collections;
namespace JMBasic
{
	public class FrameStatic : MonoBehaviour
	{

		// Use this for initialization
		void Awake()
		{
			Application.targetFrameRate = 60;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			//QualitySettings.resolutionScalingFixedDPIFactor = 0.5f;
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
