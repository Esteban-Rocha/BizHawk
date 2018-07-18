﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.Emulation.Cores.Computers.AmstradCPC
{
    /// <summary>
    /// CPC464
    /// * Port *
    /// </summary>
    public partial class CPC464 : CPCBase
    {
        /// <summary>
        /// Reads a byte of data from a specified port address
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public override byte ReadPort(ushort port)
        {
            BitArray portBits = new BitArray(BitConverter.GetBytes(port));
            byte portUpper = (byte)(port >> 8);
            byte portLower = (byte)(port & 0xff);

            int result = 0xff;

            if (DecodeINPort(port) == PortDevice.GateArray)
            {
                GateArray.ReadPort(port, ref result);
            }
            else if (DecodeINPort(port) == PortDevice.RAMManagement)
            {

            }
            else if (DecodeINPort(port) == PortDevice.CRCT)
            {
                CRCT.ReadPort(port, ref result);
            }
            else if (DecodeINPort(port) == PortDevice.ROMSelect)
            {

            }
            else if (DecodeINPort(port) == PortDevice.Printer)
            {

            }
            else if (DecodeINPort(port) == PortDevice.PPI)
            {
                PPI.ReadPort(port, ref result);
            }
            else if (DecodeINPort(port) == PortDevice.Expansion)
            {

            }

            return (byte)result;

            if ((port & 0x8000) == 0)
            {
                if ((port & 0x4000) != 0)
                {
                    GateArray.ReadPort(port, ref result);
                }
                else
                {
                    // ram
                }
            }
            else if ((port & 0x4000) == 0)
            {
                CRCT.ReadPort(port, ref result);
            }
            else if ((port & 0x0300) == 0)
            {
                // rom
            }
            else if ((port & 0x1000) == 0)
            {
                // printer
            }
            else if ((port & 0x0800) == 0)
            {
                PPI.ReadPort(port, ref result);
            }
            else if ((port & 0x0400) == 0)
            {
                // exp
            }

            return (byte)result;


            if (CRCT.ReadPort(port, ref result))
            {
                return (byte)result;
            }
            else if (PPI.ReadPort(port, ref result))
            {
                return (byte)result;
            }
            else if (GateArray.ReadPort(port, ref result))
            {
                return (byte)result;
            }

            return (byte)result;
        }

        /// <summary>
        /// Writes a byte of data to a specified port address
        /// Because of the port decoding, multiple devices can be written to
        /// </summary>
        /// <param name="port"></param>
        /// <param name="value"></param>
        public override void WritePort(ushort port, byte value)
        {
            BitArray portBits = new BitArray(BitConverter.GetBytes(port));
            BitArray dataBits = new BitArray(BitConverter.GetBytes(value));
            byte portUpper = (byte)(port >> 8);
            byte portLower = (byte)(port & 0xff);

            var devs = DecodeOUTPort(port);

            foreach (var d in devs)
            {
                if (d == PortDevice.GateArray)
                {
                    GateArray.WritePort(port, value);
                }
                else if (d == PortDevice.RAMManagement)
                {

                }
                else if (d == PortDevice.CRCT)
                {
                    CRCT.WritePort(port, value);
                }
                else if (d == PortDevice.ROMSelect)
                {

                }
                else if (d == PortDevice.Printer)
                {

                }
                else if (d == PortDevice.PPI)
                {
                    PPI.WritePort(port, value);
                }
                else if (d == PortDevice.Expansion)
                {

                }
            }

            return;

            if (GateArray.WritePort(port, value))
            { }

            if (CRCT.WritePort(port, value))
            { }

            // rom select
            // printer port

            if (PPI.WritePort(port, value))
            { }
        }
    }
}