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

        [Test()]
        public void CheckForNulls() {
            foreach (string DeserializedElementDictionary_name in Creaturedb.TABLE_OBJECTS.Keys) {
                TypeCastDictionary<int, DeserializedElement> DeserializedElementDictionary_collection = Creaturedb.TABLE_OBJECTS[DeserializedElementDictionary_name];
                foreach (DeserializedElement element in DeserializedElementDictionary_collection.Values) {
                    Assert.IsNotNull(element);
                    Assert.IsNotNull(element.id);
                    Assert.IsNotNull(element.ToString());
                    Assert.AreEqual(element, DeserializedElementDictionary_collection[element.id]);
                }
            }
        }

	}
}