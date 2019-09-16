﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleKit.Datas.Story.Orders;

namespace TaleKit.Datas.Story {
	public class StoryBlock : StoryBlockBase {
		
		public string DisplayName {
			get; set;
		}
		public List<OrderBase> OrderList {
			get; private set;
		}
		public bool IsComplete {
			get {
				for (int i = 0; i < OrderList.Count; ++i) {
					if (!OrderList[i].IsComplete) {
						return false;
					}
				}
				return true;
			}
		}

		public StoryBlock(StoryFile ownerFile) : base(ownerFile) {
			OrderList = new List<OrderBase>();
		}

		public void OnEnter() {
			foreach (OrderBase job in OrderList) {
				job.OnStart();
			}
		}
		public void OnTick() {
			foreach (OrderBase job in OrderList) {
				job.OnTick();
			}
		}
		public void OnEnd() {
			foreach (OrderBase job in OrderList) {
				job.OnComplete();
			}
		}

		public void Skip() {
			for (int i = 0; i < OrderList.Count; ++i) {
				OrderList[i].Skip();
			}
		}

		public override JObject ToJObject() {
			JObject jBlock = new JObject();

			//Add components
			JArray jOrders = new JArray();
			jBlock.Add("Orders", jOrders);

			foreach(OrderBase order in OrderList) {
				jOrders.Add(order.ToJObject());
			}

			return jBlock;
		}
	}
}