using System;
using Microsoft.AspNetCore.Components;

namespace BlazorWheelPickerLib
{
	public partial class DateWheelSelect : ComponentBase
	{
        [Parameter]
        public string WheelColor { get; set; } = "#ffffff";

        [Parameter]
        public string TextColor { get; set; } = "#000000";

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public int WheelLevel { get; set; } = 2;

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public EventCallback<DateTime> ValueChanged { get; set; }

        [Parameter]
        public DateTime? MinYear { get; set; }

        [Parameter]
        public DateTime? MaxYear { get; set; }

        public WheelSelect<int> DayWheel { get; set; }

        [Parameter]
        public DateTime Value { get; set; } = DateTime.Now;

		public List<int> Days
		{
			get
			{
				return Enumerable.Range(1, DateTime.DaysInMonth(Value.Year, Value.Month)).ToList();
			}
		}

        public List<int> Months
        {
            get
            {
                return Enumerable.Range(1, 12).ToList();
            }
        }

        public List<int> Years
        {
            get
            {
                return Enumerable.Range(MinYear.HasValue ? MinYear.Value.Year : 1970, MaxYear.HasValue && MinYear.HasValue ? ((MaxYear.Value.Year - MinYear.Value.Year) + 1) : 100).ToList();
            }
        }

        public void OnMonthChanged(int value)
        {
            var day = Value.Day;
            if (Value.Day > DateTime.DaysInMonth(Value.Year, value))
            {
                day = DateTime.DaysInMonth(Value.Year, value);
            }

            Value = new DateTime(Value.Year, value, day);
            ValueChanged.InvokeAsync(Value);
        }

        public void OnYearChanged(int value)
        {
            var day = Value.Day;
            if (Value.Day > DateTime.DaysInMonth(value, Value.Month))
            {
                day = DateTime.DaysInMonth(value, Value.Month);
            }

            Value = new DateTime(value, Value.Month, day);
            ValueChanged.InvokeAsync(Value);
        }

        public void OnDayChanged(int value)
        {
            Value = new DateTime(Value.Year, Value.Month, value);
            ValueChanged.InvokeAsync(Value);
        }
    }
}

