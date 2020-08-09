/* 
*   NatSuite Integrations
*   Copyright (c) 2020 Yusuf Olokoba
*/

namespace NatSuite.Integrations {

    using UnityEngine;
    using UnityEngine.UI;
    using Recorders;

    public sealed class ARCam : MonoBehaviour {
        
        [Header(@"Video Recording")]
        [Range(0.2f, 1f)]
        public float videoScale = 0.5f;
    }
}