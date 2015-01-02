using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using CustomerService.Properties;

namespace CustomerService.Bear
{  
    public class BearInjection
    {
        private readonly MainWindow _control;
        private static BearInjection _resource = new BearInjection();
        private bool _activate;
        private const string Password = "BEAR";
        private int _level;

        private BearInjection() { }
        public BearInjection(MainWindow control)
        {
            _control = control;

            _control.Focusable = true;
            _control.KeyUp += (sender, args) =>
            {
                var currentChar = Password.Substring(_level, 1);
                var releasedKey = args.Key.ToString();
                if (releasedKey.Equals(currentChar))
                {
                    _level++;
                }
                else
                {
                    _level = 0;
                }

                if (_level == Password.Length)
                {
                    ShowBear();
                }
            };
        }

        private void ShowBear()
        {
            if (_activate)
            {
                var targetTextBlock = _control.CommunicationOutput;
                
                var bitmap = Resources.DancingBear;
                var ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Bmp);
                var myImageSource = new BitmapImage();
                myImageSource.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                myImageSource.StreamSource = ms;
                myImageSource.EndInit();

                var image = new Image
                {
                    Source = myImageSource, 
                    Visibility = Visibility.Visible,
                    Width = 331.8, //2655,
                    Height = 248.8 //1991
                };

                var container = new InlineUIContainer(image);
                targetTextBlock.Inlines.Add(container);
                _level = 0;
            }
        }

        public void Activate()
        {
            _activate = true;
        }

        
    }
}
