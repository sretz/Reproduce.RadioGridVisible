using System;
using System.Collections.Generic;
using System.Text;

namespace Reproduce.RadioGridVisible
{
	internal sealed class DayCellFull : DayCellBase
	{
		#region Fields
		private readonly Label focus;
		private readonly Label other;
		private Action<DayCellFull> selected;
		private Action<DayCellFull> unselect;
		#endregion //Fields

		#region Constructors
		public DayCellFull(
			RadioButton view, View container,
			Label focus, Label other,
			BoxView holidayOutset, BoxView holidayEnding)
			: base(view, container, holidayOutset, holidayEnding)
		{
			this.focus = focus;
			this.other = other;
		}
		#endregion //Constructors

		#region Properties
		public Label Focus => focus;
		public Label Other => other;
		#endregion //Properties

		#region Methods
		public void SetSelections(Action<DayCellFull> selected, Action<DayCellFull> unselect)
		{
			this.selected = selected;
			this.unselect = unselect;
		}

		public void Selected()
		{
			selected(this);
		}

		public void Unselect()
		{
			unselect(this);
		}
		#endregion //Methods
	}
}
