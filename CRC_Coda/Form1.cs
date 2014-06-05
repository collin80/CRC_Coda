﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRC_Coda
{
    public partial class Form1 : Form
    {
        byte b2, b3, b4, crc;

        byte[] byte4Table = { 0xB, 0x16, 0x2C, 0x58, 0xB0, 0x60, 0xC0, 1 };
        byte[] byte3Table = { 0xAA, 0x7F, 0xFE, 0x29, 0x52, 0xA4, 0x9D, 0xEF };

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Try to do the whole algorithm based on two static tables.
        //Take in three bytes and return the proper CRC byte.
        private void button1_Click(object sender, EventArgs e)
        {
            
            b2 = byte.Parse(txtFullByte2.Text, System.Globalization.NumberStyles.HexNumber);
            b3 = byte.Parse(txtFullByte3.Text, System.Globalization.NumberStyles.HexNumber);
            b4 = byte.Parse(txtFullByte4.Text, System.Globalization.NumberStyles.HexNumber);

            crc = 0x86;

            //if b2 was 0x9? then add one to byte 3 otherwise leave it alone.
            if (((b2 & 0x90) == 0x90) || ((b2 & 0x50) == 0x50))
            {
                b3 += 1;
            }

            for (int i = 0; i < 8; i++)
            {
                if ((b3 & (1 << i)) == (1 << i))
                {
                    crc = (byte)(crc ^ byte3Table[i]);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if ((b4 & (1 << i)) == (1 << i))
                {
                    crc = (byte)(crc ^ byte4Table[i]);
                }
            }
            txtFullCRC.Text = crc.ToString("X2");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //little different here... take the two values from byte 3 and find the bit differences. Then take the value
        //of the first and try to turn it into the proper value for the second by XOR'ing by the proper
        //bit masks. 
        private void button2_Click(object sender, EventArgs e)
        {
            b2 = byte.Parse(txtB3First.Text, System.Globalization.NumberStyles.HexNumber);
            b3 = byte.Parse(txtB3Sec.Text, System.Globalization.NumberStyles.HexNumber);

            b4 = (byte)(b2 ^ b3);

            crc = byte.Parse(txtInCRC.Text, System.Globalization.NumberStyles.HexNumber);

            for (int i = 0; i < 8; i++)
            {
                if ((b4 & (1 << i)) == (1 << i))
                {
                    crc = (byte)(crc ^ byte3Table[i]);
                }
            }
            txtB3CRC.Text = crc.ToString("X2");
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}