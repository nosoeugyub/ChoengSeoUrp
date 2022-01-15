using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Cam
{
    public class LockZAxis : CinemachineExtension
    {
        public float m_ZPosition = 0;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage== CinemachineCore.Stage.Body)
            
            {
                var pos = state.RawPosition;
                pos.z = m_ZPosition;
                state.RawPosition = pos;
            }

        }

    }
}


