package md568d398eee8b61b7503afd8cb0f1db3a3;


public class ShareClass
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("cinefilo.Droid.Implementations.ShareClass, cinefilo.Android", ShareClass.class, __md_methods);
	}


	public ShareClass ()
	{
		super ();
		if (getClass () == ShareClass.class)
			mono.android.TypeManager.Activate ("cinefilo.Droid.Implementations.ShareClass, cinefilo.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
