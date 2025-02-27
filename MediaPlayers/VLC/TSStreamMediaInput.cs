﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Serilog;

namespace opentuner
{
    public class TSStreamMediaInput : LibVLCSharp.Shared.MediaInput
    {

        CircularBuffer ts_data_queue;
        public bool ts_sync = false;
        public bool end = false;

        public TSStreamMediaInput(CircularBuffer _ts_data_queue )
        {
            // we can't seek live data
            CanSeek = false;
            ts_data_queue = _ts_data_queue;
        }

        public override bool Open(out ulong size)
        {
            Log.Information("Open Media Input");

            bool success = true;
            size = ulong.MaxValue;

            return success;
        }

        public override void Close()
        {
            Log.Information("Close Media Input");
        }


        public override int Read(IntPtr buf, uint len)
        {
            int timeout = 0;

            // wait for next data
            while (ts_data_queue.Count < 1010)
            {
                if (end == true)
                {
                    return 0;
                }
                //Log.Information("Waiting: " + timeout.ToString());
                // if we haven't received anything within a few seconds then most likely won't get anything
                if (timeout > 500000)
                {
                    Log.Information("TSStreamMediaInput : Read Timeout");
                    return 0;
                }

                Thread.Sleep(5);
                timeout += 50;
            }

            int queue_count = ts_data_queue.Count; 

            if (queue_count > 0)
            {
                //RawTSData raw_ts_data = null;
                byte raw_ts_data = 0;

                uint buildLen = len;
                if (queue_count < buildLen)
                {
                    buildLen = Convert.ToUInt32(queue_count);
                }

                byte[] vlc_data = new byte[buildLen];

                int counter = 0;

                while (counter < buildLen)
                {
                    //if (ts_data_queue.TryDequeue(out raw_ts_data))
                    //{
                    if (ts_data_queue.Count > 0)
                    {
                        raw_ts_data = ts_data_queue.Dequeue();

                        //vlc_data[counter++] = raw_ts_data.rawTSData[0];

                        if (ts_sync == false && raw_ts_data != 0x47)
                        {
                            buildLen--;
                            continue;
                        }
                        else
                        {
                            ts_sync = true;
                            vlc_data[counter++] = raw_ts_data;
                        }
                    }
                    else
                    {
                        Log.Warning("Warning: Failing to dequeue, nothing to dequeue: TSStream");
                        return 0;
                    }
                    //}
                }

                Marshal.Copy(vlc_data.ToArray(), 0, buf, vlc_data.Length);
                return vlc_data.Length;

            }

            Log.Information("TS StreamInput: Shouldn't be here");
            return 0;
        }

        public override bool Seek(ulong offset)
        {
            // seeking is not allowed/possible
            Log.Information("VLC Trying to Seek" + offset.ToString());
            return false;
        }

    }
}
