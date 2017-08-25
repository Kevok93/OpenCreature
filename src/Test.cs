using System;
public static class Test {
	public delegate void TestMethod ();
	public static void tryToFail (TestMethod testMethod) {
		bool failedToFail = false;
		try{ 
			testMethod();
			failedToFail = true;
		} catch (Exception e) {e.ToString ();
		}
		if (failedToFail)
			throw new InvalidOperationException ("Method failed to throw an error in an invalid situation");
	}
}

