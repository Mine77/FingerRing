using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Globalization;
using Serial_Oscilloscope;

namespace FingerRing
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Timer to update terminal textbox at fixed interval.
        /// </summary>
        private System.Windows.Forms.Timer formUpdateTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// TextBoxBuffer containing text printed to terminal.
        /// </summary>
        private TextBoxBuffer textBoxBuffer = new TextBoxBuffer(4096);

        /// <summary>
        /// ASCII buffer for decoding CSVs in serial stream.
        /// </summary>
        private string asciiBuf = "";

        /// <summary>
        /// Oscilloscope channel values decoded from serial stream.
        /// </summary>
        private double[] channels = new double[3] { 0.0f, 0.0f, 0.0f };

        /// <summary>
        /// Oscilloscope for channels 1, 2 and 3.
        /// </summary>
        private Oscilloscope oscilloscope = Oscilloscope.CreateScope("Oscilloscope/Oscilloscope_settings.ini", "");

        /// <summary>
        /// Sample counter to calculate performance statics.
        /// </summary>
        private SampleCounter sampleCounter = new SampleCounter();

        /// <summary>
        /// indicate if the oscilloscope is opened. 
        /// </summary>
        private bool isOscilloscopeOpened = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //set port list
            string[] ports = SerialPort.GetPortNames();
            cboShockPort.Items.AddRange(ports);
            cboShockPort.SelectedIndex = 0;
            cboProxPort.Items.AddRange(ports);
            cboProxPort.SelectedIndex = 0;

            // Set oscilloscope captions
            oscilloscope.Caption = "Channels 1, 2 and 3";

            //setup form update timer
            formUpdateTimer.Interval = 50;
            formUpdateTimer.Tick += new EventHandler(formUpdateTimer_Tick);
            formUpdateTimer.Start();
        }

        private void btnPortControl_Click(object sender, EventArgs e)
        {
            if (serialPortShock.IsOpen && serialPortProx.IsOpen)
            {
                serialPortShock.Close();
                serialPortProx.Close();
                btnPortControl.Text = "Open Ports";
            }
            else if ((!serialPortShock.IsOpen) && (!serialPortProx.IsOpen))
            {
                try
                {
                    serialPortShock.PortName = cboShockPort.Text;
                    serialPortProx.PortName = cboProxPort.Text;
                    serialPortShock.BaudRate = 115200;
                    serialPortProx.BaudRate = 115200;
                    serialPortShock.Open();
                    serialPortProx.Open();

                    serialPortShock.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnPortControl.Text = "Close Ports";
            }
            else
            {
                MessageBox.Show("One of the ports is being using", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(serialPortShock.IsOpen)
            {
                serialPortShock.Close();
            }
            if(serialPortProx.IsOpen)
            {
                serialPortProx.Close();
            }
        }

        private void btnRefreshPort_Click(object sender, EventArgs e)
        {
            cboProxPort.Items.Clear();
            cboShockPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cboShockPort.Items.AddRange(ports);
            cboShockPort.SelectedIndex = 0;
            cboProxPort.Items.AddRange(ports);
            cboProxPort.SelectedIndex = 0;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //try
            {
                // Get bytes from serial port
                SerialPort serialPort = (SerialPort)sender;
                int bytesToRead = serialPort.BytesToRead;
                byte[] readBuffer = new byte[bytesToRead];
                serialPort.Read(readBuffer, 0, bytesToRead);

                // Process each byte
                foreach (byte b in readBuffer)
                {
                    // Parse character to textBoxBuffer
                    if ((b < 0x20 || b > 0x7F) && b != '\r')    // replace non-printable characters with '.'
                    {
                        textBoxBuffer.Put(".");
                    }
                    else if (b == '\r')     // replace carriage return with '↵' and valid new line
                    {
                        textBoxBuffer.Put("↵" + Environment.NewLine);
                    }
                    else
                    {
                        textBoxBuffer.Put(((char)b).ToString());
                    }

                    // Extract CSVs and parse to Oscilloscope
                    if (asciiBuf.Length > 128)
                    {
                        asciiBuf = "";  // prevent memory leak
                    }
                    if ((char)b == '\r')
                    {
                        // Split string to comma separated variables (ignore non numerical characters)
                        string[] csvs = (new Regex(@"[^0-9\-,.]")).Replace(asciiBuf, "").Split(',');

                        // Extract each CSV as oscilloscope channel 
                        int channelIndex = 0;
                        foreach (string csv in csvs)
                        {
                            if (csv != "" && channelIndex < 3)
                            {
                                channels[channelIndex++] = float.Parse(csv, CultureInfo.InvariantCulture);
                            }
                        }

                        // Update oscilloscopes if channel values changed
                        if (channelIndex > 0)
                        {
                            //This will be where I process the data {   by MIne77
                            //Add the data to buf
                            //DataProcess(channels);

                            // } This will be where I process the data   by MIne77
                            oscilloscope.AddScopeData(channels[0], channels[1], channels[2]);
                            sampleCounter.Increment();
                        }

                        // Reset buffer
                        asciiBuf = "";
                    }
                    else
                    {
                        asciiBuf += (char)b;
                    }
                }
            }
            //catch { }
        }

        void formUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Print textBoxBuffer to terminal
            if (txtboxReceive.Enabled && !textBoxBuffer.IsEmpty())
            {
                txtboxReceive.AppendText(textBoxBuffer.Get());
                if (txtboxReceive.Text.Length > txtboxReceive.MaxLength)    // discard first half of textBox when number of characters exceeds length
                {
                    txtboxReceive.Text = txtboxReceive.Text.Substring(txtboxReceive.Text.Length / 2, txtboxReceive.Text.Length - txtboxReceive.Text.Length / 2);
                }
            }
            else
            {
                textBoxBuffer.Clear();
            }
            
            // Update sample counter values
            lblSampleRate.Text = "Samples Recieved: " + sampleCounter.SamplesReceived.ToString();
            lblSampleReceived.Text = "Sample Rate: " + sampleCounter.SampleRate.ToString();
        }

        private void btnScopeControl_Click(object sender, EventArgs e)
        {
            if(isOscilloscopeOpened)
            {
                oscilloscope.HideScope();
            }
            else
            {
                oscilloscope.ShowScope();
            }
        }
    }
}
