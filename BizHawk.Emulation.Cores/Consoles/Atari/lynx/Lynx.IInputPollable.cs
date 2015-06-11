﻿using System;
using System.Collections.Generic;
using System.Linq;

using BizHawk.Emulation.Common;

namespace BizHawk.Emulation.Cores.Atari.Lynx
{
	public partial class Lynx : IInputPollable
	{
		public int LagCount { get; private set; }

		public bool IsLagFrame { get; private set; }

		// TODO
		public IInputCallbackSystem InputCallbacks
		{
			[FeatureNotImplemented]
			get { throw new NotImplementedException(); }
		}
	}
}
