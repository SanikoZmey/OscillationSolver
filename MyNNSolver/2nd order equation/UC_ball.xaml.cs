using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace MyNNSolver._2nd_order_equation
{
    /// <summary>
    /// Interaction logic for UC_ball.xaml
    /// </summary>
    public partial class UC_ball : UserControl, INotifyPropertyChanged
    {
        private int _timestep;
        private double[] _displacements;
        private double _helixZeroDisp;
        private bool askedCancellation;

        public double T;

        private BackgroundWorker backgroundWorker;

        public UC_ball()
        {
            DataContext = this;
            _timestep = 0;

            InitializeComponent();

            T = sliderLenght.Maximum;
            _helixZeroDisp = helixHelix.Diameter * helixHelix.Turns;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(playAnimProgress);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(playAnimProgressUpdate);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(endAnimPlay);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler timestepChanged;

        public double[] displacements
        {
            set { _displacements = value; }
        }

        public int timestep
        {
            get { return _timestep; }
            set 
            { 
                _timestep = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(helixLenght)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(spherePos)));
                timestepChanged(this, EventArgs.Empty);
            }
        }

        public double helixLenght
        {
            get { return -(_helixZeroDisp + Math.Abs(_displacements[_timestep])); }
        }

        public Point3D spherePos
        {
            get { return new Point3D(x:0.0, y:0, z: -(_helixZeroDisp + Math.Abs(_displacements[_timestep]))); }
        }

        private void buttonAnimPlay_Click(object sender, System.Windows.RoutedEventArgs e)
        {            
            if(!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void buttonAnimReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }

        private void playAnimProgress(object sender, DoWorkEventArgs e)
        {
            for (int t = 0; t < T; t++)
            {
                backgroundWorker.ReportProgress(t);
                System.Threading.Thread.Sleep(100);

                if(backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                GC.Collect();
            }
            backgroundWorker.ReportProgress(0);
        }

        private void playAnimProgressUpdate(object sender, ProgressChangedEventArgs e)
        {
            sliderLenght.Value = e.ProgressPercentage;
        }

        private void endAnimPlay(object sender, RunWorkerCompletedEventArgs e)
        {
            sliderLenght.Value = 0;
        }
    }
}
