using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using opencreature;

namespace Tests{
	[TestFixture()]
	public class CreaturedbInit {
		[Test()]
		[SetUp()]
		public void Initialization () {
			Creaturedb.initialize();
			Assert.False(Creaturedb.initialize());
		}
	}
}