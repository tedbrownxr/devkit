// Source: https://answers.unity.com/questions/1539189/check-if-mouse-is-over-ui.html

using UnityEngine;
using UnityEngine.EventSystems;

namespace DvKit
{
	[RequireComponent(typeof(EventTrigger))]
	public class CanvasPointerCheck : MonoBehaviour
	{
		public static bool IsPointerOnCanvas => s_hoveredCanvasCount > 0;

		private static int s_hoveredCanvasCount;

		private void HandleHoverCanvas()
		{
			s_hoveredCanvasCount++;
		}
		private void HandleUnhoverCanvas()
		{
			s_hoveredCanvasCount = Mathf.Max(s_hoveredCanvasCount - 1, 0);
		}

		protected void Start()
		{
			EventTrigger eventTrigger = GetComponent<EventTrigger>();

			if (eventTrigger != null)
			{
				EventTrigger.Entry enterUIEntry = new EventTrigger.Entry();
				// Pointer Enter
				enterUIEntry.eventID = EventTriggerType.PointerEnter;
				enterUIEntry.callback.AddListener((eventData) => { HandleHoverCanvas(); });
				eventTrigger.triggers.Add(enterUIEntry);

				//Pointer Exit
				EventTrigger.Entry exitUIEntry = new EventTrigger.Entry();
				exitUIEntry.eventID = EventTriggerType.PointerExit;
				exitUIEntry.callback.AddListener((eventData) => { HandleUnhoverCanvas(); });
				eventTrigger.triggers.Add(exitUIEntry);
			}
		}
	}
}
