using Microsoft.Win32;   // For file dialogs
using System.IO;         // For file handling (System.IO.Path)
using System.Windows;    // Core WPF library

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        private string currentFile = null;

        public MainWindow()
        {
            InitializeComponent();
            TextEditor.TextChanged += TextEditor_TextChanged;
        }

        // Event handler for "New" file menu option
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Clear();
            currentFile = null;
            this.Title = "Untitled - Text Editor";
            CharCount.Text = "Characters: 0";  // Reset character count
        }

        // Event handler for "Open" file menu option
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
                currentFile = openFileDialog.FileName;
                this.Title = System.IO.Path.GetFileName(currentFile) + " - Text Editor";
            }
        }

        // Event handler for "Save" file menu option
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile == null)
            {
                SaveAsFile_Click(sender, e);  // No file has been saved yet, so invoke Save As
            }
            else
            {
                File.WriteAllText(currentFile, TextEditor.Text);
            }
        }

        // Event handler for "Save As" file menu option
        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, TextEditor.Text);
                currentFile = saveFileDialog.FileName;
                this.Title = System.IO.Path.GetFileName(currentFile) + " - Text Editor";
            }
        }

        // Event handler for "Exit" file menu option
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Event handler for "About" in the Help menu
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Text Editor\nVersion 1.0\nUttam Arora", "About Text Editor", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for when text changes, updates the character count
        private void TextEditor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CharCount.Text = "Characters: " + TextEditor.Text.Length.ToString();
        }
    }
}
