using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{

    class pcap_hdr
    {
        public int magic_number;   /* magic number */
        public short version_major;  /* major version number */
        public short version_minor;  /* minor version number */
        public int thiszone;       /* GMT to local correction */
        public int sigfigs;        /* accuracy of timestamps */
        public int snaplen;        /* max length of captured packets, in octets */
        public int network;        /* data link type */
    };


    class pcaprec_hdr
    {
        public int ts_sec;         /* timestamp seconds */
        public int ts_usec;        /* timestamp microseconds */
        public int incl_len;       /* number of octets of packet saved in file */
        public int orig_len;       /* actual length of packet */
    };

    class ethernet_hdr
    {
        private int dst_mac; /* destination mac address */
        private int src_mac; /* source mac address */
        private short type; /* high protocol type */
    };

    class ethernet_ip_udp_hdr
    {
        private byte[] pcap_hdr = { (byte) 0xD4, (byte) 0xC3, (byte) 0xB2,
            (byte) 0xA1, 0x02, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, (byte) 0xFF, (byte) 0xFF, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00 };
        /*		private byte[] pcaprec_hdr = {(byte)0xB9, (byte)0xBD, (byte)0x8D, 0x52, 0x6B, (byte)0x8B, 0x07, 0x00, 
                    (byte)0xAE, 0x00, 0x00, 0x00, (byte)0xAE, 0x00, 0x00, 0x00};*/

        private pcaprec_hdr m_pcaprec_hdr = new pcaprec_hdr();

        private byte[] ethernet_hdr = { 0x34, 0x51, (byte) 0xC9, (byte) 0xE0,
            (byte) 0x85, 0x09, (byte) 0xA4, (byte) 0xD1, (byte) 0xD2, 0x28,
            0x54, (byte) 0xEA, 0x08, 0x00 };

        private byte[] ip_hdr = { 0x45, 0x00, 0x00, (byte) 0xA0, 0x71, 0x26,
            0x00, 0x00, 0x40, 0x11, 0x59, 0x4F, 0x0A, (byte) 0xE6, 0x4C,
            (byte) 0xBC, 0x0A, (byte) 0xE6, 0x4D, 0x50 };

        private byte[] udp_hdr = { 0x0C, (byte) 0x9E, 0x0C, (byte) 0x9E, 0x00,
            (byte) 0x8C, (byte) 0xBE, 0x72 };

        public ethernet_ip_udp_hdr()
        {
        }

        public ethernet_ip_udp_hdr(int rtplen)
        {
            fillHeader(rtplen);
        }

        public void fillHeader(int rtplen)
        {
            var now = DateTime.Now.Millisecond;
            m_pcaprec_hdr.ts_sec = (int)(now / 1000);                    /* timestamp seconds */
            m_pcaprec_hdr.ts_usec = (int)(now % 1000);     /* timestamp microseconds */
            m_pcaprec_hdr.incl_len = rtplen + 42;    /*ethernet_hdr:14 + ip_hdr:20 + udp_hdr:8*/
            m_pcaprec_hdr.orig_len = rtplen + 42;

            short ip_len = (short)(rtplen + 28); /*ip_hdr:20 + udp_hdr:8*/
            ip_hdr[3] = (byte)ip_len;
            ip_hdr[2] = (byte)(ip_len >> 8);

            short udp_len = (short)(rtplen + 8); /*udp_hdr:8*/
            udp_hdr[5] = (byte)udp_len;
            udp_hdr[4] = (byte)(udp_len >> 8);
        }

        public byte[] intToByteArray(int value)
        {
            return new byte[] {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)};
            /*
             * return new byte[] {
                (byte)value,
                (byte)(value >>> 8),
                (byte)(value >>> 16),
                (byte)(value >>> 24)};
             */
        }

        public byte[] getObjectByte(bool isNeedGlobalHeader)
        {
            byte[] bytes = null;
            try
            {
                using (var ms = new MemoryStream())
                {
                    using(var sw=new StreamWriter(ms))
                    {
                        if (isNeedGlobalHeader)
                        {
                            sw.Write(pcap_hdr);
                        }

                        sw.Write(intToByteArray(m_pcaprec_hdr.ts_sec));
                        sw.Write(intToByteArray(m_pcaprec_hdr.ts_usec));
                        sw.Write(intToByteArray(m_pcaprec_hdr.incl_len));
                        sw.Write(intToByteArray(m_pcaprec_hdr.orig_len));
                        sw.Write(ethernet_hdr);
                        sw.Write(ip_hdr);
                        sw.Write(udp_hdr);

                        bytes = ms.GetBuffer();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("translation" + ex.Message);
                throw ex;
            }
            return (bytes);
        }
    };

}
