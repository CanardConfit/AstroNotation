using System.Drawing;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;

namespace AstroNotation
{
    public partial class AnnotationWindow : Window
    {
        private Image _bitmapImage;

        public AnnotationWindow(Image bitmap)
        {
            InitializeComponent();
            BitmapImage = bitmap;
        }

        public Image BitmapImage
        {
            get => _bitmapImage;
            set => _bitmapImage = value;
        }

        private void AddAnnotationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AnnotationTextBox.Text)) return;
            
            using (Graphics g = Graphics.FromImage(BitmapImage))
            {
                g.DrawString(AnnotationTextBox.Text, new Font("Arial", 20), System.Drawing.Brushes.Black, new PointF(10, 10));
            }
            DialogResult = true;
            Close();
        }
    }
}