using System;
using System.Collections.Generic;
using System.Diagnostics;                                           //Using-Direktive für Stopwatch
using System.IO;                                                    //Using-Direktive für Datei und Verzeichnisoperationen
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;                                     //Using-Direktive für die Stopuhr
using System.Xml.Linq;


namespace Zeiterfassung
{
    public partial class frm_Zeiterfassung : Window
    {
        //DispatcherTimer und Stopwatch initialisieren
        readonly DispatcherTimer     Timer                  = new DispatcherTimer();
        readonly Stopwatch           StopWatch              = new Stopwatch();

        //Liste für die PausenStart- und die PausenStopZeit initialisieren
        //Aktuelle Pausen-Liste, die auch verändert werden kann
        public static List<DateTime> pauseStartZeitListe    = new List<DateTime>();
        public static List<DateTime> pauseStopZeitListe     = new List<DateTime>();
        //Liste für das Sichern der originalen Pausenzeit
        public static List<DateTime> pauseStartZeitListeAlt = new List<DateTime>();
        public static List<DateTime> pauseStopZeitListeAlt  = new List<DateTime>();
        //Hilfsliste, die abspeichert, ob die Pause manuell geändert wurde
        public static List<String>   manuellListe           = new List<String>();

        public frm_Zeiterfassung()
        {
/* 01       DEMOVERSION? => Überprüfen, ob Demoversion abgelaufen ist.
            Wenn die Demo-Version abgelaufen ist, ist das Programm hier zu Ende.                                                                    */
            //
            DemoVersionAbgelaufen.demoVersionAbgelaufen();


/* 02       PROGRAMMSETTINGS.XML => Überprüfen, ob <<ProgrammSettings.xml>> existiert.  
            Wenn nicht vorhanden, Datei mit Standard-Settings erstellen.                                                                            */
            //
            if (!File.Exists(Textbausteine.dateiName))
                StandardProgrammSettingsErstellen.standardProgrammSettingsErstellen();


/* 03       BENUTZER VORHANDEN? => Überprüfen, ob bereits ein Benutzer angelegt wurde.
            Wenn nicht, dann Optionen aufrufen und Benutzer anlegen.                                                                                */
            //
            //Name aus ProgrammSettings.xml auslesen (weitere Variablen werden zunächst nicht benötigt)
            string sName = (string)XElement.Load("ProgrammSettings.xml").Element("Name");
            //
            //Ist ein Nutzer angelegt?
            if (sName == "" || sName == null || sName == "kein Eintrag")
            {
                //Wenn nicht, dann Optionen aufrufen
                frm_Optionen optionen = new frm_Optionen();
            }


/*          PROGRAMMSETTINGS EINLESEN => Spätestens ab hier existiert ein Benutzer
            Daher werden hier die ProgrammSettings eingelesen                                                                                       */
            //
            ProgrammSettingsEinlesen.programmSettingsEinlesen();

            
/* 04       LOGFILE-ORDNER => Überprüfen, ob LogFile-Ordner vorhanden ist           
            Wenn nicht vorhanden, Ordner erstellen.                                                                                                 */
            //
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\LogFiles\"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\LogFiles\");


/* 05       LOGFILE-DATEI => Überprüfen, ob LogFile-Datei vorhanden ist                                                                             */
            //Hier werden die entsprechenden Hilfsvariablen ermittelt und gesetzt.
            //
            LogFileVorhanden.logFileVorhanden();


/* 06       Wenn erster Aufruf des Tages, LOGFILE anlegen und HEADER in LOGFILE schreiben                                                   */
            //
            if (ZeitManagement.isHeaderSchreiben == true)
            {
                LogFileHeaderSchreiben.logFileHeaderSchreiben();            //=> Stopuhrstand bei Programmstart definieren
            }
            //
            //Wenn schon mal gelaufen, dann letzten Stopuhrstand auslesen
            else
                LetzterStopUhrStandLesen.letzterStopUhrStandLesen();        //=> Stopuhrstand bei Programmstart definieren


/*          StopuhrFENSTER vorbereiten                                                                                                     */
            //
            InitializeComponent();
            StopuhrFensterFormatieren();
            StopUhrDispatcherBereitstellen();


/* a        StopuhrSTART                                                                                                                   */
            //
            //Diese Stelle wird nur beim Neustart des Programms erreicht... entsprechend wird die Hilfsvariable gesetzt
            ZeitManagement.isNeuStart = true;


/* 07       AUTOSTART Ja/Nein                                                                                                               */
            //
            //Zuordnung der Variablen
            AutoStart.autoStart();                                          //=> Arbeitszeit Startzeit

            
/* 08       Uhr starten, wenn <sAutostart> = <true> (in der autoStart-Methode wurde <uhrStarten> auf <true> gesetzt                         */
            if (ZeitManagement.isUhrStarten == true)
            {
                //Uhr Starten
                StopWatch.Start();
                Timer.Start();

                //Buttonformatierung / Farben / Hilfsvariablen
                UhrLaeuft();
            }
            //
            else
            {
                //Buttonformatierung / Farben / Hilfsvariablen
                UhrSteht();
            }
        }


/*      ***********************************************************************************************************
        BUTTONS                                                                                                     */

/* 09   ___________________________________________________________________________________________________________
        Start-Button                                                                                               */   
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            //SicherheitsCheck: Wird nur ausgeführt, wenn Uhr steht
            //
            if (ZeitManagement.isTimerStartStop == false)
            { 
                //Vorbereitung von Uhr und LogFile
                StartButton.startButton();

                //Uhr starten
                StopWatch.Start();
                Timer.Start();

                //Buttonformatierung / Farben / Hilfsvariablen
                UhrLaeuft();
            }
        }
        //Start-Button
        //===========================================================================================================|



/* 10   ___________________________________________________________________________________________________________
        Pause-Button                                                                                               */
        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            //SicherheitsCheck: Wird nur ausgeführt, wenn Uhr läuft
            //
            if (ZeitManagement.isTimerStartStop == true)
            { 
                //Vorbereitung von Uhr und LogFile
                PauseButton.pauseButton();

                //Uhr anhalten
                StopWatch.Stop();

                //Buttonformatierung / Farben / Hilfsvariablen
                UhrSteht();
            }
        }
        //Pause-Button
        //===========================================================================================================|


/* 11   ___________________________________________________________________________________________________________
        Stop-Button                                                                                               */
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            //Wenn kein Reset und wenn die Uhr lief, ist es ein einfacher "Stop"
            if (ZeitManagement.isTimerStartStop       == true)
                ZeitManagement.fromWhere            = "Stop";
            //
            //Wenn kein Reset und wenn die Uhr stand, ist es ein "Stop in Pause"
            else
            {
                ZeitManagement.fromWhere            = "StopInPause";
                //
                //Den Stopzeitpunkt gleichzeitig als Ende der Pause definieren
                frm_Zeiterfassung.pauseStopZeitListe.Add(Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00")));
            }

            //Aktuelle Zeit als Stopp-Zeit festlegen
            ZeitManagement.arbeitsZeitStopTime      = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));

            //Uhr anhalten
            StopWatch.Stop();

            //Letzte Variablen definieren und Logfile schreiben
            StopButton.stopButton();

/* b        Fensterposition auslesen und in der Konfiguration ablegen                                                   */
            double sPositionTop   = Convert.ToInt32(Top);             ///Obere Fensterposition
            double sPositionLeft  = Convert.ToInt32(Left);            ///Linke Fensterposition
            
            //Settings speichern
            XElement settings = XElement.Load("ProgrammSettings.xml");
            //
            settings.SetElementValue("LetzterStopuhrstand", ZeitManagement.aktuellerStopUhrStand);
            settings.SetElementValue("KorrekturAZAnfang",   ProgrammSettingsEinlesen.sKorrekturAZAnfang);
            settings.SetElementValue("PositionLeft",        sPositionLeft);
            settings.SetElementValue("PositionTop",         sPositionTop);
            //
            settings.Save("ProgrammSettings.xml");

            //Fenster schließen
            this.Close();

            //Programm beenden
            Environment.Exit(0);
        }
        //Stop-Button
        //===========================================================================================================|


        //___________________________________________________________________________________________________________
        //Korrektur-Button                                                                                           |
        private void btn_ZeitKorrektur_Click(object sender, RoutedEventArgs e)
        {
            ZeitManagement.fromWhere        = "ZeitKorrektur";

            //Options-Formular initialisieren
            frm_ZeitKorrektur Korrektur = new frm_ZeitKorrektur();

            //frm_Optionen öffnen
            Korrektur.ShowDialog();
        }
        //Korrektur-Button
        //___________________________________________________________________________________________________________|


        //___________________________________________________________________________________________________________
        //Reset-Button                                                                                               |
        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            //Wenn die Uhr lief, ist es ein einfacher "Reset"
            if (ZeitManagement.isTimerStartStop == true)
                ZeitManagement.fromWhere = "Reset";
            //
            //Stand die Uhr, ist es ein "Reset in Pause"
            else
                ZeitManagement.fromWhere = "ResetInPause";
          
            //Sicherheitsabfrage
            string titel     = Textbausteine.programmName;
            string nachricht = Textbausteine.resetAbfrage;
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(nachricht, titel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                //wenn Uhr läuft, dann anhalten und Variablen setzen
                if (StopWatch.IsRunning)
                {
                    StopWatch.Stop();
                    ZeitManagement.arbeitsZeitStopTime      = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                    ZeitManagement.arbeitsZeitStopText      = Textbausteine.reset;
                }
                //
                //wenn sie nicht läuft, dann nur Variablen setzen
                else
                {
                    ZeitManagement.pauseStopTime            = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                    ZeitManagement.arbeitsZeitStopText      = Textbausteine.resetInPause;
                }

                //Letzte Variablen definieren und Logfile schreiben
                StopButton.stopButton();

/* b            Fensterposition auslesen und in der Konfiguration ablegen                                                   */
                double sPositionTop = Convert.ToInt32(this.Top);             ///Obere Fensterposition
                double sPositionLeft = Convert.ToInt32(this.Left);            ///Linke Fensterposition

                //Settings speichern
                XElement settings = XElement.Load("ProgrammSettings.xml");
                //
                settings.SetElementValue("LetzterStopuhrstand", "00:00");
                settings.SetElementValue("KorrekturAZAnfang", ProgrammSettingsEinlesen.sKorrekturAZAnfang);
                settings.SetElementValue("PositionLeft", sPositionLeft);
                settings.SetElementValue("PositionTop", sPositionTop);
                //
                settings.Save("ProgrammSettings.xml");

                //Fenster schließen
                this.Close();

                //Programm beenden
                Environment.Exit(0);
            }
        }
        //Reset-Button
        //___________________________________________________________________________________________________________|


        //___________________________________________________________________________________________________________
        //Options-Button                                                                                             |
        private void btn_Optionen_Click(object sender, RoutedEventArgs e)
        {
            ZeitManagement.fromWhere        = "Optionen";

            //Settings speichern
            XElement settings = XElement.Load("ProgrammSettings.xml");
            //
            settings.SetElementValue("AlterName", ProgrammSettingsEinlesen.sName);
            //
            settings.Save("ProgrammSettings.xml");

            //Options-Formular initialisieren (öffnen)
            frm_Optionen optionen = new frm_Optionen();

            //Die (möglicherweise) geänderten Settings wieder einlesen
            ProgrammSettingsEinlesen.programmSettingsEinlesen();

            //Wenn Name geändert wurde und der alte Name nicht leer war
            if (ProgrammSettingsEinlesen.sAlterName != ProgrammSettingsEinlesen.sName && ProgrammSettingsEinlesen.sAlterName != "")
            {
                //logText anpassen, je nach dem, ob die Optionen aus der Pause oder der Arbeitszeit gestartet wurcden
                if (StopWatch.IsRunning)
                {
                    //Wenn Stopuhr läuft (Arbeitszeit)
                    ZeitManagement.arbeitsZeitStopText  = Textbausteine.arbeitsEndeDaBenutzerwechsel;
                    ZeitManagement.fromWhere            = "AndererNameLaufendeUhr";
                    ZeitManagement.arbeitsZeitStopTime  = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                }
                //
                else
                {
                    //Wenn Stopuhr steht (Pause)
                    ZeitManagement.arbeitsZeitStopText  = Textbausteine.pausenEndeDaBenutzerwechsel;
                    ZeitManagement.fromWhere            = "AndererNameInPause";
                    ZeitManagement.pauseStopTime        = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                }
                
                //Den alten Benutzer ins Logfile schreiben
                StopButton.stopButton();

                //Hinweis, dass das Programm nun geschlossen wird
                string titel     = Textbausteine.programmName;
                string nachricht = Textbausteine.benutzernameGeaendert;
                MessageBoxes.WarningBox(titel, nachricht);

                //Den Stopuhrstand in den Settings wieder auf 0 setzen den alten und den neuen Namen angleichen
                ZeitManagement.aktuellerStopUhrStand            = string.Empty;
                ProgrammSettingsEinlesen.sLetzterStopuhrstand   = ZeitManagement.aktuellerStopUhrStand;
                ProgrammSettingsEinlesen.sAlterName             = ProgrammSettingsEinlesen.sName;
                //
                //Settings speichern
                settings = XElement.Load("ProgrammSettings.xml");
                //
                settings.SetElementValue("LetzterStopuhrstand",     ProgrammSettingsEinlesen.sLetzterStopuhrstand);
                settings.SetElementValue("PositionTop",             ProgrammSettingsEinlesen.sPositionTop);
                settings.SetElementValue("PositionLeft",            ProgrammSettingsEinlesen.sPositionLeft);
                //
                settings.Save("ProgrammSettings.xml");

                //Alles beenden
                Environment.Exit(0);
            }
            //
            //Wenn Name nicht geändert wurde
            else
            {
                //Fenstereinstellungen anpassen
                //Transparenz des Fensters einstellen
                this.Opacity = ProgrammSettingsEinlesen.sTransparenz / 100;
                //
                //Fenster im Vordergrund halten oder auch nicht
                if (ProgrammSettingsEinlesen.isSVordergrund == true)
                    this.Topmost = true;
                else
                    this.Topmost = false;
            }
        }
        //Options-Button
        //___________________________________________________________________________________________________________|


/*      **************************************************************************************************
        STOPWATCH-Funktionen                                                                            */

        public void StopuhrFensterFormatieren()
        {
/*          StopuhrFENSTER formatieren                                                                 */
            //
            //Name in Titel schreiben (wird aber derzeit ausgeblendet)
            this.Title = "Zeiterfassung für: " + ProgrammSettingsEinlesen.sName;
            //
            //Transparenz des Fensters einstellen
            this.Opacity = ProgrammSettingsEinlesen.sTransparenz / 100;
            //
            //Fenster im Vordergrund halten oder auch nicht
            if (ProgrammSettingsEinlesen.isSVordergrund == true)
                this.Topmost = true;
            else
                this.Topmost = false;
            //
            //Startposition des Fensters festlegen
            ///Untere-rechte Ecke berechnen
            ///Monitorauflösung auslesen
            double monitorHight = SystemParameters.FullPrimaryScreenHeight;
            double monitorWidth = SystemParameters.FullPrimaryScreenWidth;
            ///Fenstergröße
            double windowHight = 100;
            double windowWidth = 170;
            ///Position aus Auflösung und Fenstergrö0e berechnen
            double standardTop = monitorHight - windowHight;
            double standardLeft = monitorWidth - windowWidth;
            //
            //Wenn Fenster noch nie verschoben wurde, dann das Fenster in der unteren/rechten Ecke anordnen
            if (ProgrammSettingsEinlesen.sPositionLeft == 0 || ProgrammSettingsEinlesen.sPositionTop == 0)
            {
                //Standardposition des Fensters
                this.Top = standardTop;
                this.Left = standardLeft;
            }
            //
            //Ansonsten die letzte Position aus der Konfigurationsdatei auslesen
            else
            {
                //Wenn die gespeicherte Fensterposition nicht über den Bildschirmrand hinausragt, diese benutzen.
                if (standardTop > ProgrammSettingsEinlesen.sPositionTop && standardLeft > ProgrammSettingsEinlesen.sPositionLeft)
                {
                    this.Top = ProgrammSettingsEinlesen.sPositionTop;
                    this.Left = ProgrammSettingsEinlesen.sPositionLeft;
                }
                //
                //Ansonsten Fenster in Standardposition öfnen
                else
                {
                    this.Top = standardTop;
                    this.Left = standardLeft;
                }
            }
            //
            //Fenster mit der Maus verschiebbar machen (Wichtig, da die Titelleiste ausgeblendet ist!)
            MouseDown += (sender, args) => DragMove();
        }


        public void StopUhrDispatcherBereitstellen()
        {
/*      Stopuhr-DISPATCHER bereitstellen                                                               */
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
        }


        void Timer_Tick(object sender, EventArgs e)
        {
/*          STOPPWATCH-Programm                                                                         */
            //
            
            
            if (StopWatch.IsRunning)
            {
                //Den alten Stopuhrstand in der Variable timeOld ablegen
                //
                //Wenn erster Aufruf am Tag, zusätzlich Korrekturwert einlesen.
                if (ZeitManagement.isErsterAufrufAmTag == true)
                {
                    ZeitManagement.timeOld = ZeitManagement.lastHH + ":" + (ZeitManagement.lastMM + ProgrammSettingsEinlesen.sKorrekturAZAnfang); //+ ":" + lastSS;
                }
                //
                //Wenn schon mal gelaufen, ohne Korrektur.
                else
                {
                    ZeitManagement.timeOld = ZeitManagement.lastHH + ":" + (ZeitManagement.lastMM); //+ ":" + lastSS;
                }

                //Die aktuelle Stopuhrzeit in der Variable timeNew ablegen
                TimeSpan ts1 = StopWatch.Elapsed;
                string timeNew = String.Format("{0:00}:{1:00}", ts1.Hours, ts1.Minutes);

                //Den aktuellen Wert der Arbeitszeit aus altem Stopuhrstad und aktueller Stopuhrzeit berechnen
                TimeSpan ts2 = TimeSpan.Parse(timeNew) + TimeSpan.Parse(ZeitManagement.timeOld);

                //...und in der Variable aktuellerStopUhrStand ablegen.
                ZeitManagement.aktuellerStopUhrStand = String.Format("{0:00}:{1:00}", ts2.Hours, ts2.Minutes);

                //Current Time in das Formular schreiben.
                lbl_Zeit.Content = ZeitManagement.aktuellerStopUhrStand;
            }
        }
    
    
        public void UhrLaeuft()
        {
/*          Stopuhr läuft                                                                              */
            //
            //Buttons und Einfärbung festlegen
            btn_Start.Visibility            = Visibility.Hidden;
            btn_Pause.Visibility            = Visibility.Visible;
            btn_ZeitKorrektur.IsEnabled     = true;
            btn_ZeitKorrektur.Opacity       = 1;
            lbl_Zeit.Foreground             = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4789C8"));
            //
            //Hilfsvariablen setzen
            ZeitManagement.isTimerStartStop   = true;       //Stopuhr läuft
        }

        public void UhrSteht()
        {
/*          Stopuhr steht                                                                              */
            //
            //Buttons und Einfärbung festlegen
            btn_Start.Visibility            = Visibility.Visible;
            btn_Pause.Visibility            = Visibility.Hidden;
            btn_ZeitKorrektur.IsEnabled     = false;
            btn_ZeitKorrektur.Opacity       = 0.50;
            lbl_Zeit.Foreground             = Brushes.Red;
            //
            //Hilfsvariablen setzen
            ZeitManagement.isTimerStartStop   = false;        //Stopuhr steht
        }
    }
}