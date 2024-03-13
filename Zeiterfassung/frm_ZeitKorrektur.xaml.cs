using System;
using System.Windows;

namespace Zeiterfassung
{
    public partial class frm_ZeitKorrektur : Window
    {
        public frm_ZeitKorrektur()
        {
            //Variablen definieren
            int anzahlPausen = ZeitManagement.pauseZahl + 1;                 
            int i;                                                          //Listenzähler (0-basiert)

            InitializeComponent();

            //Arbeitsbeginn oder Arbeitswiederaufnahme?
            if (ZeitManagement.arbeitsZeitStartTime.ToString("HH") != "00" && ZeitManagement.arbeitsZeitStartTime.ToString("mm") != "00")
                lbl_ArbeitsZeitStart.Content = "Arbeitszeit Start:";
            //
            else
                lbl_ArbeitsZeitStart.Content = "Arbeit Wiederaufn.:";

            //Bisherige Zeiten in Formular eintragen            
            txt_ArbeitsZeitStart_HH.Text    = ZeitManagement.arbeitsZeitStartTime.ToString("HH");
            txt_ArbeitsZeitStart_MM.Text    = ZeitManagement.arbeitsZeitStartTime.ToString("mm");

            //Alte Pausenzeiten sichern - diese werden bei Abbruch wieder zurückgeschrieben
            for (i = 0; i < anzahlPausen;  i++)
            {
                frm_Zeiterfassung.pauseStartZeitListeAlt.Add(frm_Zeiterfassung.pauseStartZeitListe[i]);
                frm_Zeiterfassung.pauseStopZeitListeAlt.Add(frm_Zeiterfassung.pauseStopZeitListe[i]);
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            //Variablen, außerhalb des try/catch fefiniert werden können
            int         i;
            DateTime    alterArbeitsBeginn  = DateTime.Parse(ZeitManagement.arbeitsZeitStartTime.ToString("dd.MM.yyyy HH:mm:00"));
            TimeSpan    neuerStopuhrstand;

            //Definieren, welche Pause gerade ausgewählt ist
            ZeitManagement.zaehlerUpDown    = Convert.ToInt32(lbl_UpDown.Content);          

/*          ARBEITSBEGINN                                                                                                                                           */
            try
            {
                //Variablen, die innerhalb des try/catch definiert werden müssen
                DateTime korrigierterArbeitsBeginn  = DateTime.Parse(txt_ArbeitsZeitStart_HH.Text + ":" + txt_ArbeitsZeitStart_MM.Text);
                TimeSpan differenzArbeitsBeginn     = alterArbeitsBeginn - korrigierterArbeitsBeginn;

                //Wurde der Arbeitsbeginn geändert? Dann sind alter und neuer Beginn unterschiedlich         
                if (alterArbeitsBeginn != korrigierterArbeitsBeginn)
                {
                    //Neuen ArbeitsBeginn als arbeitsZeitStartTime definieren
                    ZeitManagement.arbeitsZeitStartTime = korrigierterArbeitsBeginn;
                    //
                    //Kennzeichnung an Startzeit anhängen
                    ZeitManagement.manuellStart     = Textbausteine.manuellStart;
                    ZeitManagement.isKommentarNoetig  = true;
                    //
                    //Stoppuhr neu berechnen
                    ///
                    ///Zeitspanne zwischen neuem und altem Stopuhrstand berechnen
                    neuerStopuhrstand = TimeSpan.Parse(ZeitManagement.aktuellerStopUhrStand) + differenzArbeitsBeginn;
                    ///
                    //Hilfsvariable zurücksetzen
                    ZeitManagement.isErsterAufrufAmTag = false;
                    /// 
                    ///Stunden- und Minuten des neuen Stoppuhrstandes separieren und in entsprechende Variable schreiben
                    ZeitManagement.lastHH   = Convert.ToByte(neuerStopuhrstand.Hours);
                    ZeitManagement.lastMM   = Convert.ToByte(neuerStopuhrstand.Minutes);
                }
            }
            //
            catch 
            {
                //Es wurde eine ungültige Uhrzeit eingegeben
                //
                //Hinweisfenster
                string titel        = Textbausteine.programmName;
                string nachricht    = Textbausteine.zeitAngabeFehlerhaft;
                MessageBoxes.ErrorBox(titel, nachricht);
                //
                //Zurück zum Eingabefenster
                return;
            }

/*          PAUSENZEIT (Pausenzeit der aktuellen Pausennummer eintragen)                                                                                                                                               */

            //Wurde eine Pause gemacht? 
            if (ZeitManagement.zaehlerUpDown > 0)
            {
                //Index der aktuellen Pausennummer
                i = ZeitManagement.zaehlerUpDown - 1;
                //
                try
                {
                    //Pausenzeit der vorherigen Pausennummer eintragen
                    if
                    (   frm_Zeiterfassung.pauseStartZeitListe[i] != DateTime.Parse(txt_PauseStartTime1_HH.Text + ":" + txt_PauseStartTime1_MM.Text) ||
                        frm_Zeiterfassung.pauseStopZeitListe[i]  != DateTime.Parse(txt_PauseStopTime1_HH.Text + ":" + txt_PauseStopTime1_MM.Text))
                    //
                    {
                        frm_Zeiterfassung.pauseStartZeitListe[i] = DateTime.Parse(txt_PauseStartTime1_HH.Text + ":" + txt_PauseStartTime1_MM.Text);
                        frm_Zeiterfassung.pauseStopZeitListe[i] = DateTime.Parse(txt_PauseStopTime1_HH.Text + ":" + txt_PauseStopTime1_MM.Text);
                        frm_Zeiterfassung.manuellListe[i] = Textbausteine.manuellPause;
                        ZeitManagement.isKommentarNoetig = true;
                    }

                    //PLAUSIBILITÄTSKONTROLLE
                    //liegt Pause hinter dem Arbeitsstart?
                    if (ZeitManagement.arbeitsZeitStartTime > frm_Zeiterfassung.pauseStartZeitListe[i])
                    {
                        //Pausenzeit liegt vor der Startzeit
                        //
                        //Hinweisfenster
                        string titel = Textbausteine.programmName;
                        string nachricht = Textbausteine.pausenZeitVorStartzeit;
                        MessageBoxes.ErrorBox(titel, nachricht);
                        //
                        //Zurück zum Eingabefenster
                        return;
                    }

                    //PLAUSIBILITÄTSKONTROLLE
                    //liegt der Pausenstop hinter dem Pausenstart?
                    if (frm_Zeiterfassung.pauseStopZeitListe[i] < frm_Zeiterfassung.pauseStartZeitListe[i])
                    {
                        //Pausenende liegt vor dem Pausenstart
                        //
                        //Hinweisfenster
                        string titel = Textbausteine.programmName;
                        string nachricht = Textbausteine.pausenEndeVorStartzeit;
                        MessageBoxes.ErrorBox(titel, nachricht);
                        //
                        //Zurück zum Eingabefenster
                        return;
                    }
                }
                //
                catch 
                {
                    //Es wurde eine ungültige Uhrzeit eingegeben
                    //
                    //Hinweisfenster
                    string titel = Textbausteine.programmName;
                    string nachricht = Textbausteine.zeitAngabeFehlerhaft;
                    MessageBoxes.ErrorBox(titel, nachricht);
                    //
                    //Zurück zum Eingabefenster
                    return;
                }

                //Stopuhr neu berechnen
                StopUhrStandBerechnen.stopUhrStandBerechnen();
            }
/*          BEGRÜNDUNG                                                                                                                                               */
            if (txt_Begruendung.Text == "" && ZeitManagement.isKommentarNoetig == true)
            {
                //Es wurde keine Begründung eingegeben, obwohl die Werte geändert wurden
                //
                //Hinweisfenster
                string titel = Textbausteine.programmName;
                string nachricht = Textbausteine.kommentarFehlt;
                MessageBoxes.ErrorBox(titel, nachricht);
                //
                //Zurück zum Eingabefenster
                return;
            }

            if (txt_Begruendung.Text != "")
                ZeitManagement.kommentar = "\r[* = Manuelle Korrektur: " + txt_Begruendung.Text + "]\r";
            //
            else
                ZeitManagement.kommentar = "";

            //Form schließen
            this.Close();
        }



        private void btn_Abbrechen_Click(object sender, RoutedEventArgs e)
        {
            //Variablen definieren
            int anzahlPausen = ZeitManagement.pauseZahl + 1;
            int i;                                                              //Listenzähler (0-basiert)

            //alte Pausenzeiten wiederherstellen
            for (i = 0; i < anzahlPausen; i++)
            {
                frm_Zeiterfassung.pauseStartZeitListe[i] = frm_Zeiterfassung.pauseStartZeitListeAlt[i];
                frm_Zeiterfassung.pauseStopZeitListe[i]  = frm_Zeiterfassung.pauseStopZeitListeAlt[i];
            }
            //Form schließen
            this.Close();
        }

        private void btn_Up_Click(object sender, RoutedEventArgs e)
        {
            //Variablen definieren
            int anzahlPausen    = ZeitManagement.pauseZahl;
            int zaehlerUpDown   = Convert.ToInt32(lbl_UpDown.Content);
            int i;                                                          //Listenzähler (0-basiert)

            //Nur soweit hochzählen, dass die Pausenanzahl nicht überschritten wird.
            if (zaehlerUpDown <= anzahlPausen)
            {
                //Zähler um 1 erhöhen
                zaehlerUpDown += 1;
                lbl_UpDown.Content  = zaehlerUpDown;
                //
                //Es muss mindestens eine zweite Pause gemacht worden sein...
                if (zaehlerUpDown > 1)
                {
                    //Index der vorherigen Pausennummer
                    i = zaehlerUpDown - 2;
                    //
                    //Pausenzeit der vorherigen Pausennummer eintragen
                    if
                    (   frm_Zeiterfassung.pauseStartZeitListe[i]    != DateTime.Parse(txt_PauseStartTime1_HH.Text + ":" + txt_PauseStartTime1_MM.Text) ||
                        frm_Zeiterfassung.pauseStopZeitListe[i]     != DateTime.Parse(txt_PauseStopTime1_HH.Text + ":" + txt_PauseStopTime1_MM.Text))
                    //
                    {
                        frm_Zeiterfassung.pauseStartZeitListe[i]    = DateTime.Parse(txt_PauseStartTime1_HH.Text + ":" + txt_PauseStartTime1_MM.Text);
                        frm_Zeiterfassung.pauseStopZeitListe[i]     = DateTime.Parse(txt_PauseStopTime1_HH.Text + ":" + txt_PauseStopTime1_MM.Text);
                        frm_Zeiterfassung.manuellListe[i]           = Textbausteine.manuellPause;
                        //
                        //OK-Button ausblenden
                        this.btn_OK.IsEnabled = false;
                    }
                }
                //
                //Index der aktuellen Pausennummer
                i = zaehlerUpDown - 1;
                //
                //Beginn der akutellen Pause eintragen
                txt_PauseStartTime1_HH.Text = frm_Zeiterfassung.pauseStartZeitListe[i].ToString("HH");
                txt_PauseStartTime1_MM.Text = frm_Zeiterfassung.pauseStartZeitListe[i].ToString("mm");
                //
                //Ende der aktuellen Pause eintragen
                txt_PauseStopTime1_HH.Text = frm_Zeiterfassung.pauseStopZeitListe[i].ToString("HH");
                txt_PauseStopTime1_MM.Text = frm_Zeiterfassung.pauseStopZeitListe[i].ToString("mm");
                //
            }
        }

        private void btn_Down_Click(object sender, RoutedEventArgs e)
        {
            //Variablen definieren
            int zaehlerUpDown   = Convert.ToInt32(lbl_UpDown.Content);
            int i;                                                              //Listenzähler (0-basiert)

            //Nur soweit runterzählen, dass 0 nicht erreicht wird
            if (zaehlerUpDown > 1)
            {
                //Zähler um 1 reduzieren
                zaehlerUpDown -= 1;
                lbl_UpDown.Content  = zaehlerUpDown;
                //
                //Es muss mindestens eine Pause gemacht worden sein...
                if (zaehlerUpDown > 0)
                {
                    //Index der vorherigen Pausennummer berechnen
                    i = zaehlerUpDown;
                    //
                    //Pausenzeit der vorherigen Pausennummer eintragen
                    frm_Zeiterfassung.pauseStartZeitListe[i]    = DateTime.Parse(txt_PauseStartTime1_HH.Text + ":" + txt_PauseStartTime1_MM.Text);
                    frm_Zeiterfassung.pauseStopZeitListe[i]     = DateTime.Parse(txt_PauseStopTime1_HH.Text + ":" + txt_PauseStopTime1_MM.Text);
                }
                //
                //Index der aktuellen Pausennummer
                i = zaehlerUpDown - 1;
                //
                //Beginn der aktuellen Pause eintragen
                txt_PauseStartTime1_HH.Text = frm_Zeiterfassung.pauseStartZeitListe[i].ToString("HH");
                txt_PauseStartTime1_MM.Text = frm_Zeiterfassung.pauseStartZeitListe[i].ToString("mm");
                //
                //Ende der aktuellen Pause eintragen
                txt_PauseStopTime1_HH.Text = frm_Zeiterfassung.pauseStopZeitListe[i].ToString("HH");
                txt_PauseStopTime1_MM.Text = frm_Zeiterfassung.pauseStopZeitListe[i].ToString("mm");
            }
        }
    }
}
