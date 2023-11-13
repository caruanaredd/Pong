using UnityEngine;

namespace Pong
{
    public static class CameraBounds
    {
        /// <summary>
        /// The left edge of the screen.
        /// </summary>
        public static float Left => GetCoordinates().x;
        
        /// <summary>
        /// The right edge of the screen.
        /// </summary>
        public static float Right => GetCoordinates().z;
        
        /// <summary>
        /// The top edge of the screen.
        /// </summary>
        public static float Top => GetCoordinates().y;
        
        /// <summary>
        /// The bottom edge of the screen.
        /// </summary>
        public static float Bottom => GetCoordinates().w;
        
        private static Vector4 GetCoordinates()
        {
            // Check if the game has a main Camera
            Camera mainCamera = Camera.main;

            if (mainCamera == null)
            {
                // if null, return zero
                // stop the code
                return Vector4.zero;
            }

            // Find the top/right/bottom/left edges
            Vector3 topLeft = mainCamera.ViewportToWorldPoint(Vector3.zero);
            Vector3 bottomRight = mainCamera.ViewportToWorldPoint(Vector3.one);
            
            // put them in a variable
            return new Vector4(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }
    }
}