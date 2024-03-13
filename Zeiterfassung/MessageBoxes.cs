using System.Windows;
using System.Windows.Forms;


namespace Zeiterfassung
{
    static class MessageBoxes
    {
        public static void WarningBox(string titel, string nachricht)
        {
            System.Windows.Forms.MessageBox.Show(nachricht, titel, 
                (MessageBoxButtons)MessageBoxButton.OK, MessageBoxIcon.Warning);
        }
        public static void ErrorBox(string titel, string nachricht)
        {
            System.Windows.Forms.MessageBox.Show(nachricht, titel, 
                (MessageBoxButtons)MessageBoxButton.OK, MessageBoxIcon.Error);
        }
        public static void InformationBox(string titel, string nachricht)
        {
            System.Windows.Forms.MessageBox.Show(nachricht, titel, 
                (MessageBoxButtons)MessageBoxButton.OK, MessageBoxIcon.Information);
        }
    }
}
