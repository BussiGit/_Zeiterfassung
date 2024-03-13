using System.IO;


namespace Zeiterfassung
{
    public class LogFileArbeitsbeginnSchreiben
    {
        public static void logFileArbeitsbeginnSchreiben()
        {
/*01        ARBEITSBEGINN schreiben                                                                                                                        */
            //
            //Variablen definieren
            //b
            if (ZeitManagement.isErsterAufrufAmTag == true)
            {
                //ba
                if (ProgrammSettingsEinlesen.isSAutostart == true)
                {
                    //mit Autorstart
                    //
                    ///ohne Korrektur
                    if (ProgrammSettingsEinlesen.sKorrekturAZAnfang == 0)
                        ZeitManagement.arbeitsZeitStartText = Textbausteine.arbeitAutoStart;
                    ///
                    ///mit 5 min. Korrektur
                    else if (ProgrammSettingsEinlesen.sKorrekturAZAnfang == 5)
                        ZeitManagement.arbeitsZeitStartText = "Arbeitsbeginn (-" + ProgrammSettingsEinlesen.sKorrekturAZAnfang + "min. / Autostart)...............";
                    ///
                    ///ab 10 min. Korrektur
                    else
                        ZeitManagement.arbeitsZeitStartText = "Arbeitsbeginn (-" + ProgrammSettingsEinlesen.sKorrekturAZAnfang + "min. / Autostart)..............";
                }
                //
                //bc
                else
                {
                    //ohne Autostart
                    ///
                    ///ohne Korrektur
                    if (ProgrammSettingsEinlesen.sKorrekturAZAnfang == 0)
                        ZeitManagement.arbeitsZeitStartText = Textbausteine.arbeitStart;
                    ///
                    ///mit 5 min. Korrektur
                    else if (ProgrammSettingsEinlesen.sKorrekturAZAnfang == 5)
                        ZeitManagement.arbeitsZeitStartText = "Arbeitsbeginn (-" + ProgrammSettingsEinlesen.sKorrekturAZAnfang + "min.)...........................";
                    ///
                    ///ab 10 min. Korrektur
                    else
                        ZeitManagement.arbeitsZeitStartText = "Arbeitsbeginn (-" + ProgrammSettingsEinlesen.sKorrekturAZAnfang + "min.)..........................";
                }
            }

/*          LOGFILE BESCHREIBEN                                                                      */
            //
            using (StreamWriter sw = new StreamWriter(ZeitManagement.completePath, true))
            {
                //Zunächst drei Leerzeilen einfügen
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                //
                //logText in die Datei schreiben (erster Teil ohne Uhrzeit)
                sw.Write(ZeitManagement.arbeitsZeitStartText);
                //
                //Uhrzeit anhängen
                sw.WriteLine("  " + ZeitManagement.arbeitsZeitStartTime.ToString("HH:mm") + " Uhr" + ZeitManagement.manuellStart);
            }
        }
    }
}
