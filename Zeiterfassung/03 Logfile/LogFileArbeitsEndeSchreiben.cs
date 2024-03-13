using System.IO;


namespace Zeiterfassung._03_Logfile
{
    internal class LogFileArbeitsEndeSchreiben
    {
        public static void logFileArbeitsEndeSchreiben()
        {
            using (StreamWriter sw = new StreamWriter(ZeitManagement.completePath, true))
            {
/*              ARBEITSENDE schreiben                                                                   */                
                //
                //eine Leerzeile voranstellen
                sw.WriteLine("");
                //
                //logText in die Datei schreiben (erster Teil ohne Uhrzeit)
                sw.Write(ZeitManagement.arbeitsZeitStopText);
                //
                //UHRZEIT anhängen (=> wenn es NICHT ein Arbeitsende in der Pause war...)
                if (ZeitManagement.arbeitsZeitStopText != Textbausteine.arbeitEndeInPause)
                    sw.WriteLine("  " + ZeitManagement.arbeitsZeitStopTime.ToString("HH:mm") + " Uhr");
                //
                //Wenn das Arbeitsende in der Pause erfolgte, keine Uhrzeit eintragen, da diese schon beim Pausenende steht...
                else
                    sw.WriteLine("");
                //
                //Wenn manuell korrigiert wurde, Kommentar einfügen
                if (ZeitManagement.kommentar != "")
                    sw.WriteLine(ZeitManagement.kommentar);
                //
                //Korrekten Stoppuhrstand berechnen
                StopUhrStandBerechnen.stopUhrStandBerechnen();
                //
                //Länge der ARBEITSZEIT anhängen
                sw.WriteLine("--------------------------------------------------------------------------------------");
                sw.Write(ZeitManagement.aktuellerStopUhrStand + "                                              Heutige AZ in HH:MM");
                //
                //Wenn ein Reset durchgeführt wurde, Hinweis mit genullter Stoppuhr druntersetzen
                if (ZeitManagement.fromWhere == "Reset" || ZeitManagement.fromWhere == "ResetInPause")
                {
                    sw.WriteLine("");
                    sw.WriteLine("--------------------------------------------------------------------------------------");
                    sw.Write("00:00                                              Stoppuhr zurückgesetzt(Reset)");
                }
            }
        }
    }
}
