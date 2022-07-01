namespace Reproduce.RadioGridVisible;

public partial class MainPage : ContentPage
{
	private readonly DayCellFull[] queue = new DayCellFull[30];
	private DayCellFull? cell;

	private readonly DayInfo[] period = new DayInfo[30];

	public MainPage()
	{
		InitializeComponent();

		DayCellFull cell;
		RadioButton view;

		this.queue[00] = cell = UI.CreateLuniSolarMoonthNewMoon();
		view = cell.View;
		calendar.Add(view, 6, 0);
		var d = 0;
		for (var row = 2; row < 6; ++row)
		{
			for (var col = 0; col < 6; ++col)
			{
				this.queue[++d] = cell = UI.CreateLuniSolarMoonthWork(d);
				view = cell.View;
				calendar.Add(view, col, row);
			}
			this.queue[++d] = cell = UI.CreateLuniSolarMoonthSabbath(d);
			view = cell.View;
			calendar.Add(view, 6, row);
		}
		this.queue[29] = cell = UI.CreateLuniSolarMoonthTransition();
		view = cell.View;
		calendar.Add(view, 6, 6);

		var date = DateTime.Now.Date;
		for (d = 0; d < period.Length; ++d)
		{
			var info = new DayInfo() { Gregorian = date, };
			if (d >= 13 && d < 21)
			{
				info.HolidayOutset = true;
				info.HolidayEnding = true;
			}
			period[d] = info;
			date = date.AddTicks(TimeSpan.TicksPerDay);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		for (var d = 28; d >= 0; --d)
		{
			var cell = queue[d];
			var info = period[d];
			UI.UpdateLuniSolarMoonth(cell, info);
			if (d == 0)
			{
				UI.Selected(cell);
				this.cell = cell;
				cell.View.IsChecked = true;
			}
			else
			{
				UI.Unselect(cell);
			}
		}
		if (visable.IsChecked)
		{
			var cell = queue[29];
			var info = period[29];
			var view = cell.View;
			view.IsEnabled = true;
			view.IsVisible = true;
			UI.UpdateLuniSolarMoonth(cell, info);
			if (29 == 0)
			{
				UI.Selected(cell);
				this.cell = cell;
				cell.View.IsChecked = true;
			}
			else
			{
				UI.Unselect(cell);
			}
		}
		else
		{
			var cell = queue[29];
			var view = cell.View;
			view.IsEnabled = false;
			view.IsVisible = false;
		}
	}

	private void VisableCheckedChanged(object sender, CheckedChangedEventArgs args)
	{
		if (args.Value)
		{
			var cell = queue[29];
			var info = period[29];
			var view = cell.View;
			view.IsEnabled = true;
			view.IsVisible = true;
			UI.UpdateLuniSolarMoonth(cell, info);
			if (29 == 0)
			{
				UI.Selected(cell);
				this.cell = cell;
				cell.View.IsChecked = true;
			}
			else
			{
				UI.Unselect(cell);
			}
		}
		else
		{
			var cell = queue[29];
			var view = cell.View;
			view.IsEnabled = false;
			view.IsVisible = false;
		}
	}
}

