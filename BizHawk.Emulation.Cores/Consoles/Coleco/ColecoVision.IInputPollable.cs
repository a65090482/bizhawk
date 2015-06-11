﻿using System;
using BizHawk.Emulation.Common;

namespace BizHawk.Emulation.Cores.ColecoVision
{
	public partial class ColecoVision : IInputPollable
	{
		public int LagCount
		{
			get { return _lagCount; }
		}

		public bool IsLagFrame
		{
			get { return _isLag; }
		}

		public IInputCallbackSystem InputCallbacks
		{
			[FeatureNotImplemented]
			get { throw new NotImplementedException(); }
		}

		private int _lagCount = 0;
		private bool _isLag = true;
	}
}
