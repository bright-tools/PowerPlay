# PowerPlay

Small Windows utility to run custom scripts when a laptop is plugged into or unplugged from the power supply.

The utility sits in the Task Tray (small lightning bolt icon) & watches for the power supply being connected/disconnected.

When it detects either, it runs all .exe and all .bat files from a pre-determined location (`My Documents\PowerPlay\OnBattery` or `My Documents\PowerPlay\OnMains`, depending on the power status)

# Why

I've found that there are a few apps which can had a severely detrimental effect on battery life (hello OneDrive) & it's useful to be able to "switch them off" when running off the battery.

There are a couple of example scripts in the `examples` folder which use PsSusped from [PsUtils](https://docs.microsoft.com/en-us/sysinternals/downloads/pstools) to suspend and un-suspend OneDrive

# How to Install

1. Install the latest release of PowerPlay
1. Go to your "My Documents" folder & create a new sub-directory called "PowerPlay"
1. Within the newly created "PowerPlay" directory, create 2 new directories called "OnBattery" and "OnMains"
1. (Optional) Download the example OneDriveResume.bat file and place it within "OnMains"
1. (Optional) Download the example OneDriveSuspend.bat file and place it within "OnBattery"