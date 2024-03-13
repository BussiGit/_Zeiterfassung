using System;
using Zeiterfassung._03_Logfile;


namespace Zeiterfassung
{
    public class StopButton
    {
        public static void stopButton()
        {
/*11        STOP                                                                                                                       */
            //
            if (ZeitManagement.isTimerStartStop           == true)
            {
                if (ZeitManagement.fromWhere == "Reset")
                    ZeitManagement.arbeitsZeitStopText = Textbausteine.arbeitsEndeDaReset;
                else if (ZeitManagement.fromWhere == "AndererNameLaufendeUhr")
                    ZeitManagement.arbeitsZeitStopText = Textbausteine.arbeitsEndeDaBenutzerwechsel;
                else
                {
                    ZeitManagement.fromWhere = "Stop";
                    ZeitManagement.arbeitsZeitStopText = Textbausteine.arbeitEnde;
                }
            }
            //
            else
            {
                if (ZeitManagement.fromWhere == "AndererNameInPause")
                    ZeitManagement.arbeitsZeitStopText = Textbausteine.pausenEndeDaBenutzerwechsel;
                else
                {
                    ZeitManagement.fromWhere = "Stop";
                    ZeitManagement.arbeitsZeitStopText = Textbausteine.arbeitEndeInPause;
                }
            }

/*          LOGFILE schreiben                                                                                                         */
            LogFileArbeitsbeginnSchreiben.  logFileArbeitsbeginnSchreiben();
            LogFilePauseSchreiben.          logFilePauseSchreiben();
            LogFileArbeitsEndeSchreiben.    logFileArbeitsEndeSchreiben();


            //Variable zum Neuaufruf des Fensters wieder zurückseten
            ZeitManagement.isTimerStartStop               = false;            //Stopuhr als angehalten markieren
            ZeitManagement.isErsterAufrufAmTag            = false;            //Das Fenster als schon einmal aufgerufen markieren
        }
    }
}