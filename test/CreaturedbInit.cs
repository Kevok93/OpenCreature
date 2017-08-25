using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

[TestFixture()]
public class CreaturedbInit {
	[Test()]
	[SetUp()]
	public void Initialization () {
		Creaturedb.initialize();
	}
}

