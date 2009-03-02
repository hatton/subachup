using System;

namespace subachup
{
	#region Grid stuff

	public class MRUAttribute : Attribute
	{
		public MRUAttribute()
		{
		}
	}
	public class ImageAttribute : Attribute
	{
		public ImageAttribute()
		{
		}
	}
	public class NormallyReadOnlyAttribute : Attribute
	{
		public NormallyReadOnlyAttribute()
		{
		}
	}
	public class DefaultVisibleAttribute : Attribute
	{
		public int _order=-1;
		public DefaultVisibleAttribute()
		{
		}
		public DefaultVisibleAttribute(int order)
		{
			_order = order;
		}
	}
	public class NeverVisibleAttribute : Attribute
	{
		public NeverVisibleAttribute()
		{
		}
	}
	public class DefaultColumnOrder : Attribute
	{

	}

	#endregion
 
	#region PersistenceStuff
		public class ForIndividualUserAttribute : Attribute
		{
			public ForIndividualUserAttribute()
			{
			}
		}
		public class DontSaveAttribute : Attribute
		{
			public DontSaveAttribute()
			{
			}
		}
	#endregion


}
