using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using iTunesLib;

namespace itunes_monitor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        int size = 2;
	    float realsize = 0.0f;
        bool shrink = false;
        bool requestclose = false;
        double percentoffull = 0.0;
        int maxsize;
        bool found = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            percentoffull = this.Opacity;

            if (this.Height - output.Height < this.Width)
                maxsize = this.Height - output.Height;
            else
                maxsize = this.Width;

            this.Region = new Region(RoundedRectangle.Create(0, 0, this.Width, this.Height, 50, RoundedRectangle.RectangleCorners.All));

            tmrBlink.Start();
            iTunesProcessManager.startiTunesServices();
            if (iTunesProcessManager.startiTunes())
            {
                requestclose = true;
            }
            tmriTunesGrow.Start();
            tmriTunesCheck.Start();

            NativeWIN32.RegisterHotKeys(this.Handle);
        }

        void iTunes_OnQuittingEvent()
        {
            this.Close();
        }

        private void tmrBlink_Tick(object sender, EventArgs e)
        {
            output.Visible ^= true;
        }

        private void tmriTunesGrow_Tick(object sender, EventArgs e)
        {
                if (size >= maxsize || size <= 0) 
                {
					tmriTunesGrow.Stop();
				}
				else 
                {
					if (!shrink)
						realsize += (int)(1.7f * (float)Math.Log(0.1f * (maxsize - size) + 1.0f));
					else
						realsize -= (int)(2.0f * (float)Math.Log(0.1f * (size) + 1.0f));
					size = (int)realsize;

					percentoffull = ((double)size/(double)maxsize);
					this.Opacity = percentoffull;

					pictureBox1.Top = ((this.Height - output.Height) / 2) - (size/2);
					pictureBox1.Left = (this.Width/2) - (size/2);
					pictureBox1.Width = size;
					pictureBox1.Height = size;
				}
                this.pictureBox1.Visible = true;
        }


        bool executeKill = true;
        private void tmriTunesCheck_Tick(object sender, EventArgs e)
        {
            	Process[] process  = Process.GetProcessesByName("iTunes");

				if(requestclose)
				{
                    NativeWIN32.UnregisterHotkeys(this.Handle);
					this.Close();
				}
				else
				{
					if(process.Length == 0) {
						Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
						if (found) {
                            this.tmriTunesGrow.Start();
							this.tmriTunesCheck.Interval = 3000;
							shrink = true;
							this.output.Text = "Cleaning Up Services...";
							tmrBlink.Start();
							if (iTunesProcessManager.isiTunesServicesRunning())
                            {
                                if (executeKill)
                                {
                                    this.Opacity = 1;
                                    executeKill = false;
                                    this.TopMost = true;
                                    this.TopMost = false;   
                                    this.Show();

                                    iTunesProcessManager.killiTunesServices();
                                }
							}
							else 
                            {
                                NativeWIN32.UnregisterHotkeys(this.Handle);
								this.Close();
							}
						}
						else 
                        {
                            if (executeKill)
                            {
                                this.Opacity = 1;
                                this.TopMost = true;
                                this.Show();
                                this.output.Text = "iTunes not found!";
                            }
						}
					}
					else 
                    {
						tmrBlink.Stop();
						tmriTunesGrow.Stop();
						this.Opacity = 0;
                        this.Hide();
						found = true;

						Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
					}
				}
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if ((int)m.WParam == (int)Keys.MediaNextTrack)
                    {
                        iTunesAppClass iTunes = new iTunesAppClass();
                        iTunes.NextTrack();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(iTunes);
                    }
                    else if ((int)m.WParam == (int)Keys.MediaPreviousTrack)
                    {
                        iTunesAppClass iTunes = new iTunesAppClass();
                        iTunes.PreviousTrack();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(iTunes);
                    }
                    else if ((int)m.WParam == (int)Keys.MediaPlayPause)
                    {
                        iTunesAppClass iTunes = new iTunesAppClass();
                        iTunes.PlayPause();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(iTunes);
                    }
                    else if ((int)m.WParam == (int)Keys.MediaStop)
                    {
                        iTunesAppClass iTunes = new iTunesAppClass();
                        iTunes.Stop();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(iTunes);
                    }
                    else
                    {
                        MessageBox.Show("Something seems to be wrong. The ID was: " + m.WParam.ToString());
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
