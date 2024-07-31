using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace AstroNotation
{
    public partial class MainWindow : Window
    {
        private Image _image;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpeg;*.jpg)|*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                _image = Image.FromFile(openFileDialog.FileName);
                ImageCanvas.Children.Clear();
                ImageCanvas.Children.Add(new System.Windows.Controls.Image() {Source = ConvertDrawingImageToImageSource(_image), Width = _image.Width, Height = _image.Height});
            }
        }

        private void SaveImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG files (*.jpeg)|*.jpeg";
            if (saveFileDialog.ShowDialog() == true)
            {
                _image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void AddAnnotationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AnnotationWindow annotationWindow = new AnnotationWindow(_image);
            annotationWindow.ShowDialog();
        }
        
        public static ImageSource ConvertDrawingImageToImageSource(System.Drawing.Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the System.Drawing.Image to the memory stream
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            
                // Reset the stream position to the beginning
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Create a BitmapImage and begin its initialization
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
