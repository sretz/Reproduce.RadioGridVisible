using System;
using System.Collections.Generic;
using System.Text;

namespace Reproduce.RadioGridVisible
{
	internal abstract class DayCellBase
	{
		#region Fields
		protected DayInfo info;
		protected readonly RadioButton view;
		protected readonly View container;
		protected readonly BoxView holidayOutset;
		protected readonly BoxView holidayEnding;
		#endregion //Fields

		#region Constructors
		protected DayCellBase(
			RadioButton view, View container,
			BoxView holidayOutset, BoxView holidayEnding)
		{
			this.view = view;
			this.container = container;
			this.holidayOutset = holidayOutset;
			this.holidayEnding = holidayEnding;
		}
		#endregion //Constructors

		#region Properties
		public DayInfo Info { get => info; set => info = value; }
		public RadioButton View => view;
		public View Container => container;
		public BoxView HolidayOutset => holidayOutset;
		public BoxView HolidayEnding => holidayEnding;
		#endregion //Properties
	}
}
