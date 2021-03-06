﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GKit;
using GKit.Unity;
using GKit.Data;
using TaleKit.Datas.Editor;
using UAnchorPreset = GKit.AnchorPreset;
using UVector2 = UnityEngine.Vector2;
using UColor = UnityEngine.Color;

namespace TaleKit.Datas.UI {
	public class UiItem : IEditableModel {

		public event NodeItemInsertedDelegate<UiItem> ChildInserted;

		public event NodeItemDelegate<UiItem, UiItem> ChildRemoved;

		public readonly UiFile OwnerFile;
		public UiItem ParentItem {
			get; private set;
		}
		public List<UiItem> ChildItemList {
			get; private set;
		}

		//Datas
		[ValueEditorComponent_Header("Transform")]
		[ValueEditor_AnchorPreset("Anchor")]
		public UAnchorPreset anchorPreset = UAnchorPreset.StretchAll;

		[ValueEditor_Vector2("Size")]
		public UVector2 size;
		[ValueEditor_NumberBox("Rotation")]
		public float rotation;

		[ValueEditorComponent_ItemSeparator]
		[ValueEditorComponent_Header("Render")]
		[ValueEditor_ColorBox("Color")]
		public UColor color = Color.white;

		public readonly GameObject GameObject;
		public readonly RectTransform RectTransform;
		public readonly UiTransform UiTransform;
		public readonly CanvasRenderer Renderer;
		public float Alpha {
			get {
				return Renderer.GetAlpha();
			} set {
				Renderer.SetAlpha(value);
			}
		}

		public UiItem(UiFile ownerFile) {
			this.OwnerFile = ownerFile;

			ChildItemList = new List<UiItem>();

			//For unity only (제거할것)
			//GameObject = new GameObject();
			//RectTransform = GameObject.AddComponent<RectTransform>();
			//UiTransform = GameObject.AddComponent<UiTransform>();
			//Renderer = GameObject.AddComponent<CanvasRenderer>();
		}

		public void AddChildItem(UiItem item) {
			InsertChildItem(ChildItemList.Count, item);
		}
		public void InsertChildItem(int index, UiItem item) {
			ChildItemList.Insert(index, item);
			item.ParentItem = this;

			ChildInserted?.Invoke(index, item);
		}
		public void RemoveChildItem(UiItem item) {
			ChildItemList.Remove(item);
			item.ParentItem = null;

			ChildRemoved?.Invoke(item, this);
		}


	}
}
