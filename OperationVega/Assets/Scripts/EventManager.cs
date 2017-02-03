
namespace Assets.Scripts
{
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// The event manager class. It is used to handle events and is a singleton
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        /// <summary>
        /// The event manager instance.
        /// </summary>
        private static EventManager eventManager;

        /// <summary>
        /// The event dictionary used for events.
        /// </summary>
        private Dictionary<string, UnityEvent> eventDictionary;

        /// <summary>
        /// Gets the instance of the event manager.
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                // Set eventManager to the value of eventManager if eventManager is NOT null; otherwise,
                // if eventManager = null, set eventManager to new EventManager().
                eventManager = eventManager ?? FindObjectOfType(typeof(EventManager)) as EventManager;

                eventManager.Init();

                return eventManager;
            }
        }

        /// <summary>
        /// The subscribe function. Subscribes to an event for later use
        /// </summary>
        /// <param name="eventName">
        /// The event name to subscribe to.
        /// </param>
        /// <param name="listener">
        /// The listener that will be the event function.
        /// </param>
        public static void Subscribe(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;

            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// The unsubscribe function. Unsubscribes to an event
        /// </summary>
        /// <param name="eventName">
        /// The event name to unsubscribe from.
        /// </param>
        /// <param name="listener">
        /// The listener that will be the event function.
        /// </param>
        public static void UnSubscribe(string eventName, UnityAction listener)
        {
            if (eventManager == null) return;

            UnityEvent thisEvent = null;

            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        /// <summary>
        /// The publish function. Calls the event to be used.
        /// </summary>
        /// <param name="eventName">
        /// The event name to be called and execute its function(s).
        /// </param>
        public static void Publish(string eventName)
        {
            UnityEvent thisEvent;

            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        /// <summary>
        /// The initialize function. Initializes the eventDictionary
        /// </summary>
        private void Init()
        {
            if (this.eventDictionary == null)
            {
                this.eventDictionary = new Dictionary<string, UnityEvent>();
            }
        }
    }
}
