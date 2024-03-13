using System;
using System.Windows;
using System.Xml.Linq;


namespace Zeiterfassung
{
    public partial class frm_Optionen : Window
    {
        public frm_Optionen()
        {
/* 01       Fenster INITIALISIEREN                                                              */
            //
            InitializeComponent();


/* 02       PROGRAMMSETTINGS einlesen                                                           */
            //
            ProgrammSettingsEinlesen.programmSettingsEinlesen();
            //
            //Optionsfenster füllen (Vorbereitung)
            ///Versionsnummer
            lbl_Versionsnummer.Content      = Textbausteine.version;
            ///
            ///Werte in das Optionsfenster schreiben
            txt_Firma.Text                  = ProgrammSettingsEinlesen.sFirma;
            txt_Name.Text                   = ProgrammSettingsEinlesen.sName;
            sld_KorrekturAZAnfang.Value     = ProgrammSettingsEinlesen.sKorrekturAZAnfang;
            sld_Transparenz.Value           = ProgrammSettingsEinlesen.sTransparenz;
            cbx_Vordergrund.IsChecked       = ProgrammSettingsEinlesen.isSVordergrund;
            cbx_Autostart.IsChecked         = ProgrammSettingsEinlesen.isSAutostart;


/* 03       OPTIONSFENSTER mit Werten füllen und ausführen?                                                               */
            //
            if (ProgrammSettingsEinlesen.sName == "kein Eintrag" || ProgrammSettingsEinlesen.sName == null || ProgrammSettingsEinlesen.sName == "")
            {
                //Wenn noch kein Benutzer angelegt ist:
                ///
                ///Hinweisfenster ausgeben
                string titel        = Textbausteine.programmName;
                string nachricht    = Textbausteine.benutzerFehlt;
                MessageBoxes.InformationBox(titel, nachricht);
                //
                //Optionen Aufrufen
                ShowDialog();

                //Nach Eingabe des Benutzers muss das Programm geschlossen werden
                ///
                ///Hinweisfenster
                titel       = Textbausteine.programmName;
                nachricht   = Textbausteine.hinweisNeustart;
                MessageBoxes.InformationBox(titel, nachricht);

                //Alles schließen
                Environment.Exit(0);
            }
            //
            else
            {
                //Alter Name speichern
                XElement settings = XElement.Load("ProgrammSettings.xml");
                ///
                settings.SetElementValue("AlterName", ProgrammSettingsEinlesen.sName);
                ///
                settings.Save("ProgrammSettings.xml");
                //
                //Optionen aufrufen
                ShowDialog();
            }           
        }

        private void btn_Abbrechen_Click(object sender, RoutedEventArgs e)
        {
            //Optionsfenster Schließen
            this.Close();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            //Werte aus dem Optionen-Fenster speichern
            try
            {
                //ProgrammSettings.xml speichern
                XElement settings = XElement.Load("ProgrammSettings.xml");
                ///
                settings.SetElementValue("Firma", txt_Firma.Text);
                settings.SetElementValue("Name", txt_Name.Text);
                settings.SetElementValue("KorrekturAZAnfang", Convert.ToByte(sld_KorrekturAZAnfang.Value));
                settings.SetElementValue("Transparenz", Convert.ToInt32(sld_Transparenz.Value));
                settings.SetElementValue("Vordergrund", (bool)cbx_Vordergrund.IsChecked);
                settings.SetElementValue("Autostart", (bool)cbx_Autostart.IsChecked);
                ///
                settings.Save("ProgrammSettings.xml");
            }
            //
            catch (Exception ex)
            {
                //Hinweisfenster
                string titel        = Textbausteine.programmName;
                string nachricht    = Textbausteine.optionenSpeicherFehler;
                MessageBoxes.ErrorBox(titel, ex.Message + nachricht);
            }

            //Optionsfenster Schließen
            this.Close ();
        }
    }
}
