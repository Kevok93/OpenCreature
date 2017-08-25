using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests{
	[TestFixture()]
	public class CreaturedbInit {
		[Test()]
		[SetUp()]
		public void Initialization () {
			Creaturedb.initialize();
		}
	}
}