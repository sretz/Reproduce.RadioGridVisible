using System;
using System.Collections.Generic;
using System.Text;

namespace Reproduce.RadioGridVisible
{
	internal sealed class DayInfo
	{
		#region Fields
		private DateTime gregorian;

		private bool holidayOutset;
		private bool holidayEnding;
		#endregion //Fields

		#region Constructors
		public DayInfo()
		{
		}
		#endregion //Constructors

		#region Properties
		public DateTime Gregorian { get => gregorian; set => gregorian = value; }

		public bool HolidayOutset { get => holidayOutset; set => holidayOutset = value; }
		public bool HolidayEnding { get => holidayEnding; set => holidayEnding = value; }
		#endregion //Properties
	}
}
