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
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MODELPriorityQueue.Modals
{
    public sealed partial class GenerateBillDialog : ContentDialog
    {
        private Job job { get; set; }
        private Technician technician { get; set; }
        private Customer customer { get; set; }

        public GenerateBillDialog(Job job)
        {
            this.job = job;
            this.InitializeComponent();
        }
        
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            double total = 0;
            //get info for technician
            Technician t = await Technician.Get(job.Technician);
            DateTimeOffset start = t.StartDate;
            DateTimeOffset end = DateTimeOffset.Now;
            int years = (end.Year - start.Year - 1) +
                    (((end.Month > start.Month) ||
                    ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);

            double pay = 30 + (years * 10);
            total = total + (double.Parse(HoursWorked.Text) * pay);

            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                var file = await folder.CreateFileAsync("Invoice.txt", CreationCollisionOption.ReplaceExisting);
                List<string> lines = new List<string>()
                {
                    "MODEL Computing Services",
                    "Invoice",
                    "Total Hours Worked: " + HoursWorked.Text + " hours",
                    "Technician's Pay: $" + pay.ToString() + " per hour",
                    "Total Price is: $" + total.ToString()
                };
                await FileIO.WriteLinesAsync(file, lines);
            }
            else
            {
                await new MessageDialog("Operation Cancelled").ShowAsync();
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
