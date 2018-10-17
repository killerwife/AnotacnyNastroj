using System;
using System.Windows.Forms;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca formular s Progressbarom
    /// </summary>
    public partial class ProgressForm : Form
    {
        /// <summary>
        /// Celkovy pocet poloziek ktorych priebeh spracovania sa zobrazuje na progres bare.
        /// </summary>
        public int Count { get; set; }

        private int _actuall;
        /// <summary>
        /// Poradove cislo aktualne spracovavanej polozky.
        /// </summary>
        public int Actuall 
        {
            get { return _actuall; } 
            set 
            { 
                _actuall = value;
                SetProgressValue(Count, Actuall);
            } 
        }
        
        private bool _isCanceled;
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();
            _isCanceled = false;
        }

        /// <summary>
        /// Nastavenie nazvu progress form.
        /// </summary>
        /// <param name="name">nazov pre progressbar</param>
        public void SetName(string name)
        {
            gpBoxProgress.Text = name;
        }

        /// <summary>
        /// Nastavenie statusu.
        /// </summary>
        /// <param name="status">status pre progressbar</param>
        public void SetStatus(string status)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke((Action<string>)SetStatus, status);
                else
                {
                    lblStatus.Text = status;
                }
            }
            catch (ObjectDisposedException) { }
        }

        /// <summary>
        /// Nastavovanie hodnot progres baru.
        /// </summary>
        /// <param name="count">celkovy pocet(reprezentujuci 100 percent progressbaru)</param>
        /// <param name="actual">aktualny pocet(na zaklade ktoreho sa nastavi progressbar)</param>
        private void SetProgressValue(int count, int actual)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke((Action<int, int>)SetProgressValue, count, actual);
                else
                {
                    int value = (count == actual ? 100 : (int)Math.Ceiling((100.0 / count) * actual));
                    progressBar.Value = value > 100 ? 100 : value;
                    lblProgress.Text = "Progress: " + actual + "/" + count;

                    //ak sa spracovala posledna zmenime tlacidlo Cancel na OK.
                    if (actual == count)
                    {
                        btnOK.Visible = true;
                        btnCancel.Visible = false;
                    }
                }
            }
            catch (ObjectDisposedException) { }
        }

        /// <summary>
        /// Informacia o tom ci je okno zrusene.
        /// </summary>
        /// <returns>true ak bolo okno zrusene inak false</returns>
        public bool IsCanceled()
        {
            return _isCanceled;
        }

        /// <summary>
        /// Obsluha kliknutua na tlacidla cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelClick(object sender, EventArgs e)
        {
            _isCanceled = true;
            this.Visible = false;
        }

        /// <summary>
        /// Obsluha kliknutia na "x" (form close).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyFormClosed(object sender, FormClosedEventArgs e)
        {
            _isCanceled = true;
            this.Visible = false;
        }

        /// <summary>
        /// Obsluha kliknutia na tlacidlo OK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Zatvorenie Formularu "zvonku"
        /// </summary>
        public void MyClosed()
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(MyClosed));
                else
                {
                    this.Close();
                }
            }
            catch (ObjectDisposedException) { }
        }
    }
}
