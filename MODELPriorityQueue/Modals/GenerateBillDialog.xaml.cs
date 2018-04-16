using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MODELPriorityQueue.Models;
using System.Text;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MODELPriorityQueue.Modals
{
    public sealed partial class GenerateBillDialog : ContentDialog
    {
        public GenerateBillDialog(Job currentJob)
        {
            this.InitializeComponent();
        }
        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            double total = 0;
            Technician t = new Technician();
            DateTimeOffset start = t.StartDate();
            DateTimeOffset end = DateTimeOffset.Now;
            int years(DateTimeOffset startTime, DateTimeOffset endTime)
            {
                return (end.Year - start.Year - 1) +
                    (((end.Month > start.Month) ||
                    ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            }

            double pay = 30 + (years(start, end) * 10);
            total = total + (double.Parse(HoursWorked.Text) * pay);

            StorageFile file = await openPicker.PickSingleFileAsync();
            String path = System.IO.Path.Combine(path + @"Documents\invoice.txt");
            if (!File.Exists(path)) Task.Run(() =>
            {
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine("MODEL Computing Services");
                    sw.WriteLine("Invoice");
                    sw.WriteLine("Total Hours Worked: " + HoursWorked.Text + " hours");
                    sw.WriteLine("Technician's Pay: $" + pay.ToString() + " per hour");
                    sw.WriteLine("Total Price is: $" + total.ToString());
                }
            });
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
