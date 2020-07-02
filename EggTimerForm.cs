using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeApp {
    public partial class EggTimerForm : Form {
        public EggTimerForm() {
            InitializeComponent();
        }

        private TimeSpan remainingTime;

        private void startButton_Click(object sender, EventArgs e) {
            if (actionButton.Text == "Start") {
                StartTimer();
            } else if (actionButton.Text == "Reset") {
                ReinitialiseTimer();
            } else {
                StopTimer();
            }
        }

        private void StartTimer() {
            string[] minutesAndSeconds = timeTextBox.Text.Split(':');
            remainingTime = new TimeSpan(0,
                int.Parse(minutesAndSeconds[0]),
                int.Parse(minutesAndSeconds[1]));
            timer.Start();
            actionButton.Text = "Stop";
        }

        private void timer_Tick(object sender, EventArgs e) {
            // Subtract one second.
            remainingTime = remainingTime.Subtract(new TimeSpan(0, 0, 1));
            // Any time left?
            if (remainingTime.TotalSeconds > 0) {
                timeTextBox.Text = remainingTime.ToString(@"mm\:ss");
            } else {
                TimerIsFinished();
            }
        }

        private void TimerIsFinished() {
            timer.Stop();
            timeTextBox.Text = "-00:00-";
            actionButton.Text = "Reset";
        }

        private void ReinitialiseTimer() {
            timeTextBox.Text = "03:00";
            actionButton.Text = "Start";
        }

        private void StopTimer() {
            timer.Stop();
            actionButton.Text = "Start";
        }

        private void timeTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)
               || (e.KeyChar == ':' && !timeTextBox.Text.Contains(':'))) {
                // Do nothing, letting the KeyChar be processed as normal.
            } else {
                e.Handled = true;
            }
        }
    }
}
