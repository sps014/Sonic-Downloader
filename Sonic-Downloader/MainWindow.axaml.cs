using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Drawing;

namespace Sonic_Downloader
{
    public class MainWindow : Window
    {
        Panel panel;
        public MainWindow()
        {
            InitializeComponent();
            HasSystemDecorations = false;
#if DEBUG
            this.AttachDevTools();
#endif

            panel = this.FindControl<Panel>("titlePane");
            panel.PointerPressed += Panel_PointerPressed;
        }

        private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Close();
        }

        private void Panel_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
