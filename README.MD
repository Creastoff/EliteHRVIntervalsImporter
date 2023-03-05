# EliteHRVIntervalsImporter

Export non-raw data from EliteHRV Pro and import into Intervals.icu.

Updates following wellness data in Intervals from EliteHRV readings.
- Resting HR
- HRV (rMSSD)
- HRV (SDNN)
- Readiness


## Usage
1) Get your Athlete Id & generate an API key in the Developer Settings section of [this page](https://intervals.icu/settings).
2) Replace the values in appsettings.json.
3) Replace the value of the filePath variable in Harness\Program.cs with the path to your exported value.
4) Build & Run - you can see the updates being applied in realtime by view the [Intervals Dashboard](https://intervals.icu) dashboard. NOTE: The console only displays error output.