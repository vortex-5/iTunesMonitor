using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

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
    }
}
