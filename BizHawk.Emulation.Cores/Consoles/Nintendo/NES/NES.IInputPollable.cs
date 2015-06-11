﻿using BizHawk.Emulation.Common;

namespace BizHawk.Emulation.Cores.Nintendo.NES
{
	public partial class NES : IInputPollable
	{
		public int LagCount
		{
			get { return _lagcount; }
		}

		public bool IsLagFrame
		{
			get { return islag; }
		}

		public IInputCallbackSystem InputCallbacks
		{
			get { return _inputCallbacks; }
		}

		private int _lagcount;
		private bool lagged = true;
		private bool islag = false;

		private readonly InputCallbackSystem _inputCallbacks = new InputCallbackSystem();
	}
}
