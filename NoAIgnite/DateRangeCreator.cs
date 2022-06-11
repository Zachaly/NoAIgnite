using NoAIgnite.Exceptions;
using System;
using System.Globalization;
using System.Linq;

namespace NoAIgnite
{
    public class DateRangeCreator
    {
        private char[] _separationSigns = { '.', '/', '-', ','};
        private char _separationSign = '.';
        // Used because a date separator differs in different cultures
        private string _cultureSeparationSign = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _endDateFormat = "dd/MM/yyyy";
        private string _startDateFormat = "dd/MM/yyyy";

        public DateRangeCreator(string startDate, string endDate)
        {
            SetDates(startDate, endDate);
        }

        /// <summary>
        /// Creates a range between start date and end date;
        /// </summary>
        public string Range() => $"{FormatDate(_startDate, _startDateFormat)} - {FormatDate(_endDate, _endDateFormat)}";

        /// <summary>
        /// Checks and sets the dates given in constructor
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <exception cref="InvalidDataException"></exception>
        private void SetDates(string startDate, string endDate)
        {
            if (!IsDateValid(startDate) || !IsDateValid(endDate))
                throw new InvalidDataException();

            var start = startDate.Split(_separationSign).Select(number => Int32.Parse(number)).ToArray();
            var end = endDate.Split(_separationSign).Select(number => Int32.Parse(number)).ToArray();

            if (start.Max() == start[0])
            {
                _startDate = new DateTime(start[0], start[1], start[2]);
                _endDate = new DateTime(end[0], end[1], end[2]);

                _endDateFormat = "yyyy/MM/dd";
                _startDateFormat = _endDateFormat;
                SetEndDateFormat();
            }
            else if(IsUSCulture())
            {
                _startDate = new DateTime(start[2], start[0], start[1]);
                _endDate = new DateTime(end[2], end[0], end[1]);

                _endDateFormat = "MM/dd/yyyy";
                _startDateFormat = _endDateFormat;
                SetStartDateFormat();
            }
            else
            {
                _startDate = new DateTime(start[2], start[1], start[0]);
                _endDate = new DateTime(end[2], end[1], end[0]);
                SetStartDateFormat();
            }

            // start date shouldn't be bigger than end
            if (_startDate > _endDate)
                throw new InvalidDataException();
        }

        /// <summary>
        /// Checks of the string containing date has a correct structure
        /// </summary>
        private bool IsDateValid(string date)
        {
            char separationSign = (char)0;

            foreach (char sign in _separationSigns)
                if (date.Contains(sign))
                    separationSign = sign;

            if (separationSign == (char)0)
                return false;

            _separationSign = separationSign;

            if (date.Split(separationSign).Count() != 3)
                return false;

            return true;
        }

        /// <summary>
        /// Adjusts date to given format, replaces a default separation with a one given by user
        /// </summary>
        /// <returns></returns>
        private string FormatDate(DateTime date, string format) 
            => date.ToString(format).Replace(_cultureSeparationSign, _separationSign.ToString());

        /// <summary>
        /// Shortens the start date format
        /// </summary>
        private void SetStartDateFormat()
        {
            if (_startDate.Year != _endDate.Year)
                return;
            
            if (_startDate.Month == _endDate.Month && !IsUSCulture())
                _startDateFormat = "dd";
            else
                _startDateFormat = IsUSCulture() ? "MM/dd" : "dd/MM";
        }

        /// <summary>
        /// Shortens the end date format
        /// </summary>
        private void SetEndDateFormat()
        {
            if (_startDate.Year != _endDate.Year)
                return;

            if (_startDate.Month == _endDate.Month)
                _endDateFormat = "dd";
            else
                _endDateFormat = "MM/dd";
        }

        private bool IsUSCulture() => CultureInfo.CurrentCulture.Name == "en-US";
    }
}
