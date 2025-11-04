using opentuner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FTD2XX_NET.FTDI;
using System.Drawing;
using opentuner.MediaSources.Minitiouner;
using FlyleafLib.MediaFramework.MediaFrame;
using LibVLCSharp.Shared;
using Vortice.MediaFoundation;
using Serilog;
using System.Globalization;
using opentuner.ExtraFeatures.BATCSpectrum;

namespace opentuner.MediaSources.Longmynd
{
    public enum LongmyndPropertyCommands
    {
        SETFREQUENCY,
        SETPRESET,
        SETTSLOCAL,
        SETRFINPUTA,
        SETRFINPUTB,
        LNBA_OFF,
        LNBA_VERTICAL,
        LNBA_HORIZONTAL,
        LNBB_OFF,
        LNBB_VERTICAL,
        LNBB_HORIZONTAL,
        SETOFFSET,
        SETSYMBOLRATE
    }

    public partial class LongmyndSource
    {
        // properties management
        private Control _parent = null;

        private DynamicPropertyGroup _tuner1_properties = null;
        private DynamicPropertyGroup _source_properties = null;

        // context menu strip
        ContextMenuStrip _genericContextStrip;

        private List<StoredFrequency> _frequency_presets = null;

        private bool muted = true;
        private int preMute = 50;

        private bool BuildSourceProperties(bool mute_at_startup)
        {
            if (_parent == null)
            {
                Log.Information("Fatal Error: No Properties Panel");
                return false;
            }

            _genericContextStrip = new ContextMenuStrip();
            _genericContextStrip.Opening += _genericContextStrip_Opening;

            _tuner1_properties = new DynamicPropertyGroup("Tuner 0", _parent);
            _tuner1_properties.OnSlidersChanged += DynamicPropertyGroup_OnSliderChanged;
            _tuner1_properties.OnMediaButtonPressed += LongmyndSource_OnMediaButtonPressed;

            _tuner1_properties.AddItem("demodstate", "Demod State", Color.PaleVioletRed);
            _tuner1_properties.AddItem("mer", "Mer");
            //_tuner1_properties.AddItem("db_margin", "db Margin");
            //_tuner1_properties.AddItem("rf_input_level", "RF Input Level");
            _tuner1_properties.AddItem("rf_input", "RF Input");
            _tuner1_properties.AddItem("requested_freq", "Requested Freq"); //, _genericContextStrip);
            _tuner1_properties.AddItem("symbol_rate", "Symbol Rate");
            _tuner1_properties.AddItem("modcod", "Modcod");
            //_tuner1_properties.AddItem("lna_gain", "LNA Gain");
            _tuner1_properties.AddItem("ber", "Ber");
            //_tuner1_properties.AddItem("freq_carrier_offset", "Freq Carrier Offset");
            //_tuner1_properties.AddItem("stream_format", "Stream Format");
            _tuner1_properties.AddItem("service_name", "Service Name");
            _tuner1_properties.AddItem("service_name_provider", "Service Name Provider");
            _tuner1_properties.AddItem("null_packets", "Null Packets");
            _tuner1_properties.AddItem("video_codec", "Video Codec");
            _tuner1_properties.AddItem("video_resolution", "Video Resolution");
            _tuner1_properties.AddItem("audio_codec", "Audio Codec");
            _tuner1_properties.AddItem("audio_rate", "Audio Rate");
            _tuner1_properties.AddSlider("volume_slider_0", "Volume", 0, 200);
            _tuner1_properties.AddMediaControls("media_controls_0", "Media Controls");

            if (mute_at_startup)
            {
                _settings.DefaultMuted = true;
            }

            muted = _settings.DefaultMuted;
            preMute = (int)_settings.DefaultVolume;

            if (!_settings.DefaultMuted)
            {
                _tuner1_properties.UpdateValue("volume_slider_0", _settings.DefaultVolume.ToString());
                _settings.DefaultVolume = (uint)preMute;        // restore as changed by update volume_slider function
                _tuner1_properties.UpdateMuteButtonColor("media_controls_0", Color.Transparent);
            }
            else
            {
                _tuner1_properties.UpdateValue("volume_slider_0", "0");
                _settings.DefaultVolume = (uint)preMute;        // restore as changed by update volume_slider function
                _tuner1_properties.UpdateMuteButtonColor("media_controls_0", Color.PaleVioletRed);
            }

            if (_settings.DefaultUDPStreaming)
            {
                _tuner1_properties.UpdateStreamButtonColor("media_controls_0", Color.PaleTurquoise);
            }
            else
            {
                _tuner1_properties.UpdateStreamButtonColor("media_controls_0", Color.Transparent);
            }

            // source properties
            _source_properties = new DynamicPropertyGroup("Longmynd Properties", _parent);
            _source_properties.AddItem("source_ip", "IP Address");
            _source_properties.AddItem("source_ts_ip", "TS IP", _genericContextStrip);
            _source_properties.UpdateColor("source_ts_ip", Color.PaleVioletRed);

            _tuner_forms = new List<TunerControlForm>();

            // tuner for each device
            var tunerControl = new TunerControlForm(0, 0, 0, (int)current_offset_0, this);
            tunerControl.OnTunerChange += TunerControl_OnTunerChange;
            _tuner_forms.Add(tunerControl);

            return true;
        }

        private void TunerControl_OnTunerChange(int id, uint freq)
        {
            Log.Information("set frequency : " + id.ToString() + "," + freq.ToString());
            SetFrequency(id, freq, (uint)current_sr_0, false);
        }

        private void LongmyndSource_OnMediaButtonPressed(string key, int function)
        {
            switch (function)
            {
                case 0: // mute
                    ToggleMute(0);
                    break;
                case 1: // snapshot
                    Log.Information("Snapshot");
                    if (playing)
                        _media_player.SnapShot(_mediaPath + CommonFunctions.GenerateTimestampFilename() + ".png");
                    break;
                case 2: // record
                    Log.Information("Record");

                    if (_recorder.record)
                    {
                        _recorder.record = false;    // stop recording
                        _tuner1_properties.UpdateRecordButtonColor("media_controls_0", Color.Transparent);
                    }
                    else
                    {
                        // are we locked onto a signal ?
                        if (demodState >= 3)
                        {
                            _recorder.record = true;     // start recording
                            _tuner1_properties.UpdateRecordButtonColor("media_controls_0", Color.PaleVioletRed);
                        }
                        else
                        {
                            Log.Information("Can't record, not locked to a signal");
                        }
                    }
                    break;

                case 3: // stream
                    Log.Information("UDP Stream");

                    
                    if ( _streamer.stream)
                    {
                        _settings.DefaultUDPStreaming = _streamer.stream = false;   
                        _tuner1_properties.UpdateStreamButtonColor("media_controls_0", Color.Transparent);
                    }
                    else
                    {
                        _settings.DefaultUDPStreaming = _streamer.stream = true;
                        _tuner1_properties.UpdateStreamButtonColor("media_controls_0", Color.PaleTurquoise);
                    }
                    break;

            }
        }

        private void DynamicPropertyGroup_OnSliderChanged(string key, int value)
        {
            switch (key)
            {
                case "volume_slider_0":
                    _settings.DefaultMuted = muted = false;
                    _media_player?.SetVolume(value);
                    _settings.DefaultVolume = (byte)value;
                    _tuner1_properties.UpdateMuteButtonColor("media_controls_0", Color.Transparent);
                    break;
            }
        }

        private void UpdateMediaProperties(int player, MediaStatus media_status)
        {
            int tuner = player + 1;

            DynamicPropertyGroup _tuner = _tuner1_properties;

            _tuner.UpdateTitle("Tuner " + tuner.ToString() + " - " + _media_player.GetName());

            string video_res = media_status.VideoWidth.ToString() + " x " + media_status.VideoHeight.ToString();
            string audio_rate = media_status.AudioRate.ToString() + " Hz, " + media_status.AudioChannels.ToString() + " channels";

            _tuner.UpdateValue("video_codec", media_status.VideoCodec);
            _tuner.UpdateValue("video_resolution", video_res);
            _tuner.UpdateValue("audio_codec", media_status.AudioCodec);
            _tuner.UpdateValue("audio_rate", audio_rate);
        }

        private void UpdateInfo(monitorMessage mm)
        {
            try
            {
                // still setting up
                if (!_Ready)
                    return;

                if (_tuner1_properties == null)
                    return;

                //Log.Information("REady for info");
                if (demodState != mm.packet.rx.demod_state)
                {
                    if (mm.packet.rx.demod_state < 3)
                    {
                        Log.Information("Stopping");
                        VideoChangeCB?.Invoke(0, false);
                        playing = false;
                    }
                    else
                    {
                        Log.Information("Playing");
                        VideoChangeCB?.Invoke(0, true);
                        playing = true;
                    }

                    demodState = mm.packet.rx.demod_state;
                }

                current_frequency_0 = (uint)mm.packet.rx.frequency;
                current_sr_0 = (uint)mm.packet.rx.symbolrate;

                double mer = Convert.ToDouble(mm.packet.rx.mer) / 10;
                double db_margin = 0;
                string modcod_text = "";

                _tuner1_properties.UpdateValue("requested_freq", GetFrequency(0, true).ToString("N0") + "  (" + GetFrequency(0, false).ToString("N0") + ")");
                _tuner1_properties.UpdateValue("symbol_rate", (mm.packet.rx.symbolrate / 1000).ToString());
                _tuner1_properties.UpdateValue("demodstate", demod_state_lookup[mm.packet.rx.demod_state]);
                _tuner1_properties.UpdateValue("ber", mm.packet.rx.ber.ToString());

                if (mm.packet.rx.demod_state < 3)
                    _tuner1_properties.UpdateColor("demodstate", Color.PaleVioletRed);
                else
                    _tuner1_properties.UpdateColor("demodstate", Color.PaleGreen);

                try
                {
                    switch (mm.packet.rx.demod_state)
                    {
                        case 3:
                            modcod_text = modcod_lookup_dvbs[mm.packet.rx.modcod];
                            db_margin = (mer - modcod_lookup_dvbs_threshold[mm.packet.rx.modcod]);
                            break;
                        case 4:
                            modcod_text = modcod_lookup_dvbs2[mm.packet.rx.modcod];
                            db_margin = (mer - modcod_lookup_dvbs2_threshold[mm.packet.rx.modcod]);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception Ex)
                {
                    debug("Unknown ModCod : " + Ex.Message);
                    debug(mm.packet.rx.modcod.ToString());
                }

                last_mer_0 = mer.ToString();
                last_dbm_0 = "D" + db_margin.ToString("N1").ToString();
                _tuner1_properties.UpdateBigLabel("D" + db_margin.ToString("N1"));
                //_tuner1_properties.UpdateValue("db_margin", "D" + db_margin.ToString("N1"));
                _tuner1_properties.UpdateValue("modcod", modcod_text);
                _tuner1_properties.UpdateValue("mer", mer.ToString() + " dB");

                _tuner1_properties.UpdateValue("rf_input", (mm.packet.rx.rfport == 0 ? "A" : "B"));

                _tuner1_properties.UpdateValue("service_name_provider", mm.packet.ts.service_provider_name);
                _tuner1_properties.UpdateValue("service_name", mm.packet.ts.service_name);

                last_service_name_0 = mm.packet.ts.service_name;
                last_service_provider_0 = mm.packet.ts.service_provider_name;

                _tuner1_properties.UpdateValue("null_packets", mm.packet.ts.null_ratio + "%");
                _source_properties.UpdateValue("source_ts_ip", mm.packet.rx.ts_ip_addr + ":" + mm.packet.rx.ts_ip_port.ToString());

                if (_LocalIp + ":" + _settings.TS_Port != mm.packet.rx.ts_ip_addr + ":" + mm.packet.rx.ts_ip_port.ToString())
                {
                    _source_properties.UpdateColor("source_ts_ip", Color.PaleVioletRed);
                }
                else
                {
                    _source_properties.UpdateColor("source_ts_ip", Color.Bisque);
                }

                //_source_properties.UpdateValue("source_ip", _settings.LongmyndWSHost);

                // lost lock
                if (mm.packet.rx.demod_state < 3)
                {
                    _tuner1_properties.UpdateValue("service_name_provider", "");
                    _tuner1_properties.UpdateValue("service_name", "");
                    _tuner1_properties.UpdateValue("stream_format", "");

                    _tuner1_properties.UpdateValue("video_codec", "");
                    _tuner1_properties.UpdateValue("video_resolution", "");
                    _tuner1_properties.UpdateValue("audio_codec", "");
                    _tuner1_properties.UpdateValue("audio_rate", "");

                    // stop recording if we lost lock
                    if (_recorder.record)
                    {
                        _recorder.record = false;    // stop recording
                        _tuner1_properties.UpdateRecordButtonColor("media_controls_0", Color.Transparent);
                    }

                }
                CultureInfo specific_culture = CultureInfo.CreateSpecificCulture("en-US");

                var source_data = new OTSourceData();
                source_data.frequency = GetFrequency(0, true);
                source_data.video_number = 0;
                source_data.mer = mer;

                source_data.db_margin = db_margin;
                source_data.symbol_rate = mm.packet.rx.symbolrate / 1000;
                source_data.demod_locked = (mm.packet.rx.demod_state == 3 || mm.packet.rx.demod_state == 4);
                if (source_data.demod_locked)
                {
                    source_data.demode_state = demod_state_lookup[mm.packet.rx.demod_state].Replace("Lock DVB-", "");
                }
                else
                {
                    source_data.demode_state = "";
                }

                source_data.service_name = mm.packet.ts.service_name;
                source_data.modcode = modcod_text;

                if (_media_player != null)
                    source_data.volume = _media_player.GetVolume();

                source_data.streaming = _streamer.stream;
                source_data.recording = _recorder.record;

                OnSourceData?.Invoke(0, source_data, "Tuner 0");
            }
            catch (Exception Ex)
            {
                Log.Warning(Ex, "Error");
            }
        }

        private void _genericContextStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ContextMenuStrip contextMenuStrip = (ContextMenuStrip)sender;
            Log.Information("Opening Context Menu :" + contextMenuStrip.SourceControl.Name);

            contextMenuStrip.Items.Clear();

            switch (contextMenuStrip.SourceControl.Name)
            {
                // change frequency
                case "requested_freq":
                    contextMenuStrip.Items.Add(ConfigureMenuItem("Change Frequency", LongmyndPropertyCommands.SETFREQUENCY, 0));
                    break;
                case "source_ts_ip":
                    // get local ip's
                    if (_LocalIp.Length == 0)
                    {
                        Log.Information("Warning: No Ip's detected");
                    }
                    else
                    {
                        contextMenuStrip.Items.Add(ConfigureMenuItem("Update TS to " + _LocalIp, LongmyndPropertyCommands.SETTSLOCAL, 0));
                    }
                    break;
            }

        }

        private ToolStripMenuItem ConfigureMenuItem(string Text, LongmyndPropertyCommands command, int option)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(Text);
            item.Click += (sender, e) =>
            {
                properties_OnPropertyMenuSelect(command, option);
            };
            return item;
        }

        public override Dictionary<string, string> GetSignalData(int device)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            if (device == 0)
            {
                data.Add("ServiceName", last_service_name_0);
                data.Add("ServiceProvider", last_service_provider_0);
                data.Add("dbMargin", last_dbm_0);
                data.Add("Mer", last_mer_0);
                data.Add("SR", current_sr_0.ToString());
                data.Add("Frequency", ((float)(current_frequency_0 + _settings.DefaultOffset) / 1000.0f).ToString("F", nfi));
            }

            return data;
        }


        private void properties_OnPropertyMenuSelect(LongmyndPropertyCommands command, int option)
        {
            Log.Information("Config Change: " + command.ToString() + " - " + option.ToString());

            switch (command)
            {
                case LongmyndPropertyCommands.SETFREQUENCY:
                    MessageBox.Show("Change Frequency");
                    break;

                case LongmyndPropertyCommands.SETTSLOCAL:

                    if (_LocalIp.Length > 0)
                    {
                        Log.Information("Updating TS Ip to " + _LocalIp);

                        if (_settings.DefaultInterface == 0)
                        {
                            // websocket
                            //string wh_command = ("U" + (option + 1).ToString() + "," + _LocalIp.ToString());
                            //Log.Information(wh_command);
                            //controlWS.Send(wh_command);

                            WSSetTS(_LocalIp, _settings.TS_Port);
                        }
                        else
                        {
                            // mqtt
                            MqttSetTS(_LocalIp, _settings.TS_Port);
                        }


                        // reset status
                        VideoChangeCB?.Invoke(option, false);
                        playing = false;
                        _tuner1_properties.UpdateColor("demodstate", Color.PaleVioletRed);
                        demodState = -1;
                    }
                    break;

                default:
                    Log.Information("Unconfigured Command Change - " + command.ToString());
                    break;
            }
        }

        public override void UpdateFrequencyPresets(List<StoredFrequency> FrequencyPresets)
        {
            _frequency_presets = FrequencyPresets;
        }

        public override void UpdateVolume(int device, int volume)
        {
            if (_media_player == null)
                return;

            int new_volume = _media_player.GetVolume() + volume;

            if (new_volume < 0) new_volume = 0;
            if (new_volume > 200) new_volume = 200;

            _media_player.SetVolume(new_volume);

            _tuner1_properties?.UpdateValue("volume_slider_0", new_volume.ToString());
        }

        public override void ToggleMute(int device)
        {
            if (!muted)
            {
                preMute = _media_player.GetVolume();
                _media_player.SetVolume(0);
                _tuner1_properties.UpdateValue("volume_slider_0", "0");
                _settings.DefaultVolume = (byte)preMute;
                _settings.DefaultMuted = muted = true;
                _tuner1_properties.UpdateMuteButtonColor("media_controls_0", Color.PaleVioletRed);
            }
            else
            {
                _media_player.SetVolume(preMute);
                _tuner1_properties.UpdateValue("volume_slider_0", preMute.ToString());
                _settings.DefaultMuted = muted = false;
                _tuner1_properties.UpdateMuteButtonColor("media_controls_0", Color.Transparent);
            }
        }

        public override bool GetMuteState(int device)
        {
            return muted;
        }

        public override int GetVolume(int device)
        {
            if (_media_player == null)
                return -1;

            return _media_player.GetVolume();
        }
    }
}
