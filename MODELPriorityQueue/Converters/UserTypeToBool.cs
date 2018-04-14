using MODELPriorityQueue.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MODELPriorityQueue.Converters
{
    class UserTypeToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// If the User is of type Manager, return true.  Else return false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Pass 'i' to invert the return statement.</param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if ((string)parameter == "i")
                {
                    if (value.GetType() == typeof(Manager))
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else
                {
                    if (value.GetType() == typeof(Manager))
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
            }
            catch (Exception e)
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
