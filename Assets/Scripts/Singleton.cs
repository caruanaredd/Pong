using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    // This is a base class for all objects that need to persist in the game.
    // Singletons should only be used in small games (ideally).

    // <T> is a Generic Type in C# that will transform into any class we want. :D
    // In this case, we restricted functionlity to all MonoBehaviors.
    public abstract class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        /// <summary>
        /// The public reference to this class.
        /// </summary>
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            // There is no object of this type.
            if (Instance == null)
            {
                Instance = this as T;
                // Protects the script from being destroyed.
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // Destroys any duplicate object without checks.
                DestroyImmediate(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
