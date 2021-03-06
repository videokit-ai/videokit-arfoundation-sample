/* 
*   NatCorder ARFoundation Integration
*   Copyright (c) 2021 Yusuf Olokoba
*/

namespace NatSuite.Examples {

    using UnityEngine;
    using UnityEngine.UI;
    using NatSuite.Recorders;
    using NatSuite.Recorders.Clocks;
    using NatSuite.Recorders.Inputs;
    using NatSuite.Sharing;

    public sealed class ARCam : MonoBehaviour {
        
        [Header(@"Recording")]
        public Camera videoCamera;
        public int videoWidth = 720;

        private MP4Recorder recorder;
        private CameraInput cameraInput;
        private string lastVideoPath;


        #region --Recording--

        public void StartRecording () {
            // Compute the video width dynamically to match the screen's aspect ratio
            var videoHeight = (int)(videoWidth / videoCamera.aspect);
            videoHeight = videoHeight >> 1 << 1; // Ensure divisible by 2
            // Create recorder and camera input
            var clock = new RealtimeClock();
            recorder = new MP4Recorder(videoWidth, videoHeight, 30);
            cameraInput = new CameraInput(recorder, clock, videoCamera);
            // Attach an optimized frame input to the camera input for better performance
            if (Application.platform == RuntimePlatform.Android)
                cameraInput.frameInput = new GLESRenderTextureInput(recorder, multithreading: true);
            else if (Application.platform ==  RuntimePlatform.IPhonePlayer)
                cameraInput.frameInput = new MTLRenderTextureInput(recorder, multithreading: true);
        }

        public async void StopRecording () {
            // Stop camera input and recorder
            cameraInput.Dispose();
            lastVideoPath = await recorder.FinishWriting();
        }
        #endregion


        #region --Sharing--
        
        public void ShareRecording () {
            // Create a share payload with video path
            var payload = new SharePayload();
            payload.AddMedia(lastVideoPath);
            // Present native share dialog
            payload.Commit();
        }
        #endregion
    }
}