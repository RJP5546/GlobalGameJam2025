using UnityEngine;
using Unity.Cinemachine;

[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class ApplyCorrections : CinemachineExtension
{
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            state.RawPosition = state.GetCorrectedPosition();
            state.PositionCorrection = Vector3.zero;
            state.RawOrientation = state.GetCorrectedOrientation();
            state.OrientationCorrection = Quaternion.identity;
        }
    }
}