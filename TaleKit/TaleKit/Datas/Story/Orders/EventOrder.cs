﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TaleKit.Datas.Editor;

namespace TaleKit.Datas.Story {
	public class EventOrder : OrderBase {
		[ValueEditor_TextBox("이벤트 키")]
		public string eventKey;

		public override OrderType OrderType => OrderType.Event;

		public EventOrder(StoryBlock ownerBlock) : base(ownerBlock) {
			
		}

		public override JObject ToJObject() {
			throw new NotImplementedException();
		}
	}
}
