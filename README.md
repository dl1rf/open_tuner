# Open Tuner

This is a fork from Tom's (ZR6TG) great Open Tuner software

[Link to Tom's web site](https://www.zr6tg.co.za/open-tuner/)

Improvements and changes:

* Mute / Unmute with right mouse click in video window
* Start / Stop recording with "r" key + mouse left click in video window
* Start / Stop streaming with "u" key + mouse left click in video window
* Snapshot with "s" key + mouse left click in video window
* Toggle video window to full video area and back with mouse left double click in video window
* Restore video windows to equal sizes with mouse double click to separation line (taken from SV8ARJ)
* New streaming and recording indicators at the info line in video window
* DATV reporter removed as Tom's web site is no longer active
* New Auto Tune functions for all Tuners
* serveral bug fixes

Known Issues:

* From time to time a wrong call sign is shown in BATC Spectrum
* Crashes at startup of OpenTuner

The issues are under investigation

[Please read the rough description](https://github.com/dl1rf/open_tuner/blob/main/Doc/OpenTuner_DL1RF's_0.C.pdf)

Installation instructions:

Unzip the binary package into a new directory to preserve your original OpenTuner version.

To get your settings from a previous version to this version copy the setting files from your original version into the settings directory of this version.

If you plan to use more than one OpenTuner instance at a time please install OpenTuner into separate directories. One for each instance.
OpenTuner will try to save serval data at program close and each instance need its own settings directory for storage.
Otherwise one instance will override settings from another instance.

Please do not close OpenTuner by closing the Terminal Window.
In this case window layout etc. will not be stored. Please close OpenTuner by closing the Main window.

Only in cases OpenTuner will not stop and close (this can take up to 1 Minute in seldom cases) kill OpenTuner by closing the Terminal window.

If you whish to use the new AutoTune function please deactivate QuickTune checkbox in main settings.

If you find issues please post them here to me.

Have fun.

Martin, DL1RF

