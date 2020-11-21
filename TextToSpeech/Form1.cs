using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace TextToSpeech
{
    public partial class Form1 : Form
    {
        bool Maximized = false;
        string GetAudioUrl(string Text, string Language)
        {
            return $@"http://translate.google.com/translate_tts?client=tw-ob&q={Text}&tl={Language}";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (!Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
                Maximized = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Maximized = false;
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor =  System.Drawing.Color.FromArgb(255, 128/2, 128/2);
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor =  System.Drawing.Color.FromArgb(255, 255, 128/2);
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor =  System.Drawing.Color.FromArgb(128/2, 255, 128/2);
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = System.Drawing.Color.FromArgb(255,128,128);
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor =  System.Drawing.Color.FromArgb(255, 255, 128);
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor =  System.Drawing.Color.FromArgb(128, 255, 128);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MediaPlayer player = new MediaPlayer(); 
            player.Open(new Uri(GetAudioUrl(richTextBox1.Text,comboBox1.Text.Split(' ')[0]), UriKind.RelativeOrAbsolute)); 
            player.Play();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Audio File(*.mp3)|*.mp3";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                new WebClient().DownloadFile(GetAudioUrl(richTextBox1.Text, comboBox1.Text.Split(' ')[0]), saveFileDialog1.FileName);
                MessageBox.Show("Файл сохранен");
            }
        }
    }
}
