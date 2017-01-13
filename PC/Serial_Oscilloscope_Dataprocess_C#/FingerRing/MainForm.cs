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
        private TextBoxBuffer textBoxBuffer_Shock = new TextBoxBuffer(4096);
        private TextBoxBuffer textBoxBuffer_Prox = new TextBoxBuffer(4096);

        /// <summary>
        /// ASCII buffer for decoding CSVs in serial stream.
        /// </summary>
        private string asciiBuf_Shock = "";
        private string asciiBuf_Prox = "";

        /// <summary>
        /// Oscilloscope channel values decoded from serial stream.
        /// </summary>
        private double[] channels_Shock = new double[3] { 0.0f, 0.0f, 0.0f };
        private double[] channels_Prox = new double[3] { 0.0f, 0.0f, 0.0f };

        /// <summary>
        /// Oscilloscope for channels 1, 2 and 3.
        /// </summary>
        private Oscilloscope oscilloscope = Oscilloscope.CreateScope("Oscilloscope/Oscilloscope_settings.ini", "");

        /// <summary>
        /// Sample counter to calculate performance statics.
        /// </summary>
        private SampleCounter sampleCounter_Shock = new SampleCounter();
        private SampleCounter sampleCounter_Prox = new SampleCounter();

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
                    serialPortShock.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived_Shock);
                    serialPortProx.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived_Prox);
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

        private void serialPort_DataReceived_Shock(object sender, SerialDataReceivedEventArgs e)
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
                        textBoxBuffer_Shock.Put(".");
                    }
                    else if (b == '\r')     // replace carriage return with '↵' and valid new line
                    {
                        textBoxBuffer_Shock.Put("↵" + Environment.NewLine);
                    }
                    else
                    {
                        textBoxBuffer_Shock.Put(((char)b).ToString());
                    }

                    // Extract CSVs and parse to Oscilloscope
                    if (asciiBuf_Shock.Length > 128)
                    {
                        asciiBuf_Shock = "";  // prevent memory leak
                    }
                    if ((char)b == '\r')
                    {
                        // Split string to comma separated variables (ignore non numerical characters)
                        string[] csvs = (new Regex(@"[^0-9\-,.]")).Replace(asciiBuf_Shock, "").Split(',');

                        // Extract each CSV as oscilloscope channel 
                        int channelIndex = 0;
                        foreach (string csv in csvs)
                        {
                            if (csv != "" && channelIndex < 3)
                            {
                                channels_Shock[channelIndex++] = float.Parse(csv, CultureInfo.InvariantCulture);
                            }
                        }

                        // Update oscilloscopes if channel values changed
                        if (channelIndex > 0)
                        {
                            //This will be where I process the data {   by MIne77
                            //Add the data to buf
                            //DataProcess(channels);

                            // } This will be where I process the data   by MIne77
                            //oscilloscope.AddScopeData(channels[0], channels[1], channels[2]);
                            sampleCounter_Shock.Increment();
                        }

                        // Reset buffer
                        asciiBuf_Shock = "";
                    }
                    else
                    {
                        asciiBuf_Shock += (char)b;
                    }
                }
            }
            //catch { }
        }

        private void serialPort_DataReceived_Prox(object sender, SerialDataReceivedEventArgs e)
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
                        textBoxBuffer_Prox.Put(".");
                    }
                    else if (b == '\r')     // replace carriage return with '↵' and valid new line
                    {
                        textBoxBuffer_Prox.Put("↵" + Environment.NewLine);
                    }
                    else
                    {
                        textBoxBuffer_Prox.Put(((char)b).ToString());
                    }

                    // Extract CSVs and parse to Oscilloscope
                    if (asciiBuf_Prox.Length > 128)
                    {
                        asciiBuf_Prox = "";  // prevent memory leak
                    }
                    if ((char)b == '\r')
                    {
                        // Split string to comma separated variables (ignore non numerical characters)
                        string[] csvs = (new Regex(@"[^0-9\-,.]")).Replace(asciiBuf_Prox, "").Split(',');

                        // Extract each CSV as oscilloscope channel 
                        int channelIndex = 0;
                        foreach (string csv in csvs)
                        {
                            if (csv != "" && channelIndex < 3)
                            {
                                channels_Prox[channelIndex++] = float.Parse(csv, CultureInfo.InvariantCulture);
                            }
                        }

                        // Update oscilloscopes if channel values changed
                        if (channelIndex > 0)
                        {
                            //This will be where I process the data {   by MIne77
                            //Add the data to buf
                            //DataProcess(channels);

                            // } This will be where I process the data   by MIne77
                            //oscilloscope.AddScopeData(channels[0], channels[1], channels[2]);
                            sampleCounter_Prox.Increment();
                        }

                        // Reset buffer
                        asciiBuf_Prox = "";
                    }
                    else
                    {
                        asciiBuf_Prox += (char)b;
                    }
                }
            }
            //catch { }
        }

        void formUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Print textBoxBuffer to terminal
            if (txtboxReceive_Shock.Enabled && !textBoxBuffer_Shock.IsEmpty())
            {
                txtboxReceive_Shock.AppendText(textBoxBuffer_Shock.Get());
                if (txtboxReceive_Shock.Text.Length > txtboxReceive_Shock.MaxLength)    // discard first half of textBox when number of characters exceeds length
                {
                    txtboxReceive_Shock.Text = txtboxReceive_Shock.Text.Substring(txtboxReceive_Shock.Text.Length / 2, txtboxReceive_Shock.Text.Length - txtboxReceive_Shock.Text.Length / 2);
                }
            }
            else
            {
                textBoxBuffer_Shock.Clear();
            }

            if (txtboxReceive_Prox.Enabled && !textBoxBuffer_Prox.IsEmpty())
            {
                txtboxReceive_Prox.AppendText(textBoxBuffer_Prox.Get());
                if (txtboxReceive_Prox.Text.Length > txtboxReceive_Prox.MaxLength)    // discard first half of textBox when number of characters exceeds length
                {
                    txtboxReceive_Prox.Text = txtboxReceive_Prox.Text.Substring(txtboxReceive_Prox.Text.Length / 2, txtboxReceive_Shock.Text.Length - txtboxReceive_Shock.Text.Length / 2);
                }
            }
            else
            {
                textBoxBuffer_Prox.Clear();
            }
            
            // Update sample counter values
            lblSampleRate_Shock.Text = "Samples Recieved: " + sampleCounter_Shock.SamplesReceived.ToString();
            lblSampleReceived_Shock.Text = "Sample Rate: " + sampleCounter_Shock.SampleRate.ToString();
            lblSampleRate_Prox.Text = "Samples Recieved: " + sampleCounter_Prox.SamplesReceived.ToString();
            lblSampleReceived_Prox.Text = "Sample Rate: " + sampleCounter_Prox.SampleRate.ToString();
        }

        private void btnScopeControl_Click(object sender, EventArgs e)
        {
            if(isOscilloscopeOpened)
            {
                oscilloscope.HideScope();
                btnScopeControl.Text = "Open Oscilloscope";
                isOscilloscopeOpened = false;
            }
            else
            {
                oscilloscope.ShowScope();
                btnScopeControl.Text = "Close Oscilloscope";
                isOscilloscopeOpened = true;
            }
        }
    }
}
