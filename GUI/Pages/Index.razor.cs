using Blazored.LocalStorage;
using Intervals.Model.EliteHRV;
using Intervals.Model.Intervals.ICU;
using Intervals.Service.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace GUI.Pages
{
    public partial class Index
    {
        [Inject]
        private IReadingMapper _readingMapper { get; set; }
        [Inject]
        private IHRVReadingProcessor _hrvReadingProcessor { get; set; }
        [Inject]
        private ILocalStorageService _localStorage { get; set; }

        private List<HRVReading> hrvReadings = new();
        private IntervalsAccess intervalsAccessCredentials { get; set; } = new();

        private const string localStorageKey = "INTERVALS_ACCESS_CREDENTIALS";

        private const string fileValidationDefault = "color: #fff; background-color: #007bff; border-color: #007bff";
        private const string fileValidationFail = "color: #fff; background-color: #8B0000; border-color: #007bff";
        private const string fileValidationSuccess = "color: #fff; background-color: #228B22; border-color: #007bff";
        private string currentFileValidationStyling = fileValidationDefault;
        private bool fileSubmissionDisabled = true;
        private string errorMessage = "";
        private string submissionStatus = "Submission Pending";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var intervalsAccessCredentialsFromStorage = await _localStorage.GetItemAsStringAsync(localStorageKey);

                if (intervalsAccessCredentialsFromStorage != null)
                {
                    intervalsAccessCredentials = JsonSerializer.Deserialize<IntervalsAccess>(intervalsAccessCredentialsFromStorage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SubmitFile()
        {
            fileSubmissionDisabled = true;
            submissionStatus = "Submitting...";

            bool successfullyUploadedData = false;

            try
            {
                await _hrvReadingProcessor.Process(hrvReadings, intervalsAccessCredentials.UserId, intervalsAccessCredentials.GetEncodedAccessToken());
                successfullyUploadedData = true;
                submissionStatus = "Submitted Successfully";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("403"))
                {
                    errorMessage = "Invalid UserId and/or Access Token";
                }
                else
                {
                    errorMessage = $"Unhandled Exception: {ex.Message}";
                }

                submissionStatus = "Submission Failed";
            }

            if (successfullyUploadedData)
            {
                try
                {
                    var copyOfIntervalsAccessCredentials = JsonSerializer.Deserialize<IntervalsAccess>(JsonSerializer.Serialize(intervalsAccessCredentials));

                    if (!copyOfIntervalsAccessCredentials.IsAllowedToStore)
                    {
                        copyOfIntervalsAccessCredentials.UserId = "";
                        copyOfIntervalsAccessCredentials.AccessToken = "";
                    }

                    await _localStorage.SetItemAsStringAsync(localStorageKey, JsonSerializer.Serialize(copyOfIntervalsAccessCredentials));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            fileSubmissionDisabled = false;
        }

        private long maxFileSize = 512000;

        private async Task LoadAndValidateHRVFile(InputFileChangeEventArgs e)
        {

            try
            {
                if (e.File.Size > maxFileSize)
                {
                    throw new Exception($"File exceeds maximum size");
                }

                var ms = new MemoryStream();

                await e.File.OpenReadStream(maxFileSize).CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(ms))
                {
                    hrvReadings = _readingMapper.MapStreamContentsToList(reader);

                    if (hrvReadings.Count == 0)
                    {
                        throw new Exception("No rows besides header found in file");
                    }

                    currentFileValidationStyling = fileValidationSuccess;
                    fileSubmissionDisabled = false;
                    errorMessage = "";

                }
            }
            catch (Exception ex)
            {
                hrvReadings = new();
                currentFileValidationStyling = fileValidationFail;
                errorMessage = ex.Message;
                fileSubmissionDisabled = true;
            }

            StateHasChanged();
        }
    }
}