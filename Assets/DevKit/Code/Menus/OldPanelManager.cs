using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Menu
{
	public class OldPanelManager : MonoBehaviour
	{
		[Space] [Header("Buttons")]
		public GameObject BackButtonPrefab;
		public GameObject CheckboxButton;
		public GameObject ComponentButtonPrefab;
		public GameObject DynamicButtonPrefab;
		public GameObject DynamicButtonWithLabel;
		public GameObject PanelButtonPrefab;

		[Space] [Header("Elements")]
		public GameObject ToStringElementPrefab;

		[Space] [Header("Fields")]
		public GameObject BooleanFieldPrefab;
		public GameObject EnumFieldPrefab;
		public GameObject IntegerFieldPrefab;
		public GameObject StringFieldPrefab;

		[Space] [Header("Settings")]
		public AnimationCurve PanelMotionCurve;

		protected Dictionary<string, OldPanel> _panels;
		protected GameObject _focusedObject;
		protected Stack<OldPanel> _panelStack;

		public void GoBack ()
		{
			if (_panelStack.Count > 1)
			{
				OldPanel oldPanel = _panelStack.Pop();
				oldPanel.Close();
				_panelStack.Peek().ShowAsExisting();
				History.GoBack();
			}
			else
			{
				Log.Error("PanelManager can't go back any further");
			}
		}

		// public void OpenListOfUniqueObjects (UniqueObjectType objectType)
		// {
		// 	ListPanel listPanel = CreatePanelObject($"{objectType.ToString()} List").AddComponent<ListPanel>();
		// 	listPanel.LoadObjectType(objectType);
		// 	AddPanel(listPanel);
		// }

		public void OpenMenu (string menuName)
		{
			// if (menuName == MenuName._Back)
			// {
			// 	GoBack();
			// 	return;
			// }

			if (_panels.TryGetValue(menuName, out OldPanel panel))
			{
				AddPanel(panel);
			}
			else
			{
				Log.Error($"PanelManager does not have an object for MenuName.{menuName}");
			}
		}

		// public void OpenAttributeQueryInList (AttributeQueryListField field, AttributeQuery query)
		// {
		// 	GameObject panelObject = CreatePanelObject("Board Selection");
		// 	var panel = panelObject.AddComponent<AttributeQueryPanel>();
		// 	panel.Load(field, query);
		// 	AddPanel(panel);
		// }

		// public void OpenBoardSelection (BoardSelection obj)
		// {
		// 	GameObject panelObject = CreatePanelObject("Board Selection");
		// 	var panel = panelObject.AddComponent<BoardSelectionPanel>();
		// 	panel.Load(obj);
		// 	AddPanel(panel);
		// }

		// public void OpenComponentPanel (ComponentButton componentButton)
		// {
		// 	GameObject panelObject = CreatePanelObject("Component - " + componentButton.ComponentName);
		// 	var panel = panelObject.AddComponent<ComponentPanel>();
		// 	panel.Load(componentButton);
		// 	AddPanel(panel);
		// }

		// public void OpenEntityQueryInList (EntityQueryListField field, EntityQuery query)
		// {
		// 	GameObject panelObject = CreatePanelObject("Entity Query");
		// 	var panel = panelObject.AddComponent<EntityQueryPanel>();
		// 	panel.Load(field, query);
		// 	AddPanel(panel);
		// }

		// public void OpenModifyEffectTemplateInList (ModifyEffectTemplateListField field, ModifyEffectTemplate template)
		// {
		// 	GameObject panelObject = CreatePanelObject("Modify Effect");
		// 	var panel = panelObject.AddComponent<ModifyEffectTemplatePanel>();
		// 	panel.Load(field, template);
		// 	AddPanel(panel);
		// }

		// public void OpenPositionRequirement (PositionRequirement positionRequirement)
		// {
		// 	GameObject panelObject = CreatePanelObject("Position Requirement");
		// 	var panel = panelObject.AddComponent<PositionRequirementPanel>();
		// 	panel.Load(positionRequirement);
		// 	AddPanel(panel);
		// }

		// public void OpenRemoveEffectTemplateInList (RemoveEffectTemplateListField field, RemoveEffectTemplate template)
		// {
		// 	GameObject panelObject = CreatePanelObject("Remove Effect");
		// 	var panel = panelObject.AddComponent<RemoveEffectTemplatePanel>();
		// 	panel.Load(field, template);
		// 	AddPanel(panel);
		// }

		// public void OpenScenarioEntityList (ScenarioPanel scenarioPanel)
		// {
		// 	GameObject panelObject = CreatePanelObject("Scenario Entity List");
		// 	var panel = panelObject.AddComponent<ScenarioEntityListPanel>();
		// 	panel.LoadFromPanel(scenarioPanel);
		// 	AddPanel(panel);
		// }

		// public void OpenSettings ()
		// {
		// 	GameObject panelObject = CreatePanelObject("Settings");
		// 	var panel = panelObject.AddComponent<SettingsPanel>();
		// 	panel.Load();
		// 	AddPanel(panel);			
		// }

		// public void OpenTemplateComponentList (TemplatePanel templatePanel)
		// {
		// 	GameObject panelObject = CreatePanelObject("Template Component List");
		// 	var panel = panelObject.AddComponent<TemplateComponentListPanel>();
		// 	panel.Load(templatePanel);
		// 	AddPanel(panel);
		// }

		// public void OpenVariableParameter (VariableParameter variableParameter, string fieldName = default(string))
		// {
		// 	if (string.IsNullOrEmpty(fieldName))
		// 	{
		// 		fieldName = "Variable Parameter";
		// 	}
		// 	GameObject panelObject = CreatePanelObject(fieldName);
		// 	var panel = panelObject.AddComponent<VariableParameterPanel>();
		// 	panel.Load(variableParameter, fieldName);
		// 	AddPanel(panel);
		// }

		// public void OpenVisualsBoardMenu ()
		// {
		// 	var panel = Instantiate(SelectTexturePanelPrefab, transform).GetComponent<SelectTexturePanel>();
		// 	RectTransform rectTransform = panel.GetComponent<RectTransform>();
		// 	rectTransform.localScale = Vector3.one;
		// 	// pivot top left
		// 	rectTransform.pivot = new Vector2(0, 1);
		// 	// anchor top left
		// 	rectTransform.anchorMin = new Vector2(0, 1);
		// 	rectTransform.anchorMax = new Vector2(0, 1);
		// 	// Just below history
		// 	panel.SetBasePosition(new Vector2(0, -30));
		// 	ScenarioVisualManager.Instance.ConfigurePanel(panel);
		// 	AddPanel(panel);
		// }

		// public void OpenVisualsMenu ()
		// {
		// 	GameObject panelObject = CreatePanelObject("Visuals");
		// 	var panel = panelObject.AddComponent<VisualsPanel>();
		// 	panel.Load();
		// 	AddPanel(panel);
		// }

		// public void OpenVisualsTemplateMenu (Template template)
		// {
		// 	var panel = Instantiate(SelectTexturePanelPrefab, transform).GetComponent<SelectTexturePanel>();
		// 	RectTransform rectTransform = panel.GetComponent<RectTransform>();
		// 	rectTransform.localScale = Vector3.one;
		// 	// pivot top left
		// 	rectTransform.pivot = new Vector2(0, 1);
		// 	// anchor top left
		// 	rectTransform.anchorMin = new Vector2(0, 1);
		// 	rectTransform.anchorMax = new Vector2(0, 1);
		// 	// Just below history
		// 	panel.SetBasePosition(new Vector2(0, -30));
		// 	TemplateVisualManager.Instance.ConfigurePanel(panel, template);
		// 	AddPanel(panel);
		// }

		// public void OpenUniqueObject (ListPanel listPanel, UniqueObject uniqueObject)
		// {
		// 	GameObject panelObject = CreatePanelObject(uniqueObject.Name);

		// 	ObjectPanel panel = null;

		// 	switch (uniqueObject.UniqueObjectType)
		// 	{
		// 		case UniqueObjectType.Ability:
		// 			panel = panelObject.AddComponent<AbilityPanel>();
		// 			break;
		// 		case UniqueObjectType.ActionTemplate:
		// 			panel = panelObject.AddComponent<ActionTemplatePanel>();
		// 			break;
		// 		case UniqueObjectType.Rule: 
		// 			panel = panelObject.AddComponent<RulePanel>();
		// 			break;
		// 		case UniqueObjectType.Scenario: 
		// 			panel = panelObject.AddComponent<ScenarioPanel>();
		// 			break;
		// 		case UniqueObjectType.ScenarioEntity: 
		// 			panel = panelObject.AddComponent<ScenarioEntityPanel>();
		// 			break;
		// 		case UniqueObjectType.Template: 
		// 			panel = panelObject.AddComponent<TemplatePanel>();
		// 			break;
		// 		case UniqueObjectType.Trigger:
		// 			panel = panelObject.AddComponent<TriggerPanel>();
		// 			break;
		// 		default:
		// 			Log.RuntimeError($"PanelManager.OpenUniqueObject: UniqueObjectType.{uniqueObject.UniqueObjectType} has not been implemented");
		// 			break;
		// 	}

		// 	if (panel == null)
		// 	{
		// 		Log.RuntimeError($"Could not load unique object {uniqueObject.Id}");
		// 		return;
		// 	}

		// 	panel.LoadFromPanel(listPanel, uniqueObject);
		// 	AddPanel(panel);
		// }

		// public void OpenWorkshopMenu ()
		// {
		// 	GameObject panelObject = CreatePanelObject("Workshop");
		// 	var panel = panelObject.AddComponent<WorkshopPanel>();
		// 	panel.Load();
		// 	AddPanel(panel);			
		// }

		protected void AddPanel (OldPanel panel)
		{
			if (_panelStack.Count > 0)
			{
				_panelStack.Peek().Hide();
			}
			_panelStack.Push(panel);
			panel.OpenAsNew();
			History.Add(panel.GetName());
		}

		protected GameObject CreatePanelObject (string name)
		{
			RectTransform panel = new GameObject(name, typeof(RectTransform)).transform as RectTransform;
			panel.parent = transform;
			panel.localScale = Vector3.one;
			// pivot top left
			panel.pivot = new Vector2(0, 1);
			// anchor top left
			panel.anchorMin = new Vector2(0, 1);
			panel.anchorMax = new Vector2(0, 1);
			// Just below history
			panel.anchoredPosition = new Vector2(0, -30);
			return panel.gameObject;
		}

		protected void Awake ()
		{
			_panelStack = new Stack<OldPanel>();
			_panels = new Dictionary<string, OldPanel>();

			// - Activate all child objects (they may have been disabled while editing the scene)
			// - Find all Panel objects
			// - Add them to the dictionary
			foreach (Transform t in transform)
			{
				bool wasActive = t.gameObject.activeSelf;
				t.gameObject.SetActive(true);
				OldPanel panel = t.GetComponentInChildren<OldPanel>();

				if (panel != null)
				{
					if (_panels.ContainsKey(panel.MenuName))
					{
						Log.Error($"PanelManager can't add duplicate {panel.MenuName} from {panel.gameObject}", panel.gameObject);
					}
					else
					{
						_panels.Add(panel.MenuName, panel);
					}
				}
				t.gameObject.SetActive(wasActive);
			}
		}

		protected void Start ()
		{
			foreach (OldPanel panel in _panels.Values)
			{
				panel.gameObject.SetActive(false);
			}
			Log.Error("First menu to open not specified!!!!!");
			//OpenMenu(MenuName.Home);
		}
	}
}
