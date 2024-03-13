using System;
using System.IO;


namespace Zeiterfassung
{
    public class LogFileHeaderSchreiben
    {
        public static void logFileHeaderSchreiben()
        {
/* 06       Neue LOGDATEI anlegen und HEADER schreiben                                                              
            //
            StreamWriter Objekt erstellen und Pfad in Konstruktor übergeben
            true = fügt den neuen Text am Ende ein
            false = überschreibt den alten Text mit dem neuen
            Durch "using" wir die Log-Datei am Ende automatisch geschlossen                                                                                 */
            //
            using (StreamWriter sw = new StreamWriter(ZeitManagement.completePath, true))
            {
                sw.WriteLine("**************************************************************************************");
                sw.WriteLine(ProgrammSettingsEinlesen.sFirma);
                sw.WriteLine("");
                sw.WriteLine("Erfassung der Arbeitszeit für: " + ProgrammSettingsEinlesen.sName + " | " + DateTime.Now.ToString("dd-MM-yyyy"));
                sw.WriteLine("Korrektur beim ersten Programmstart des Tages: " + ProgrammSettingsEinlesen.sKorrekturAZAnfang + " min.   Zeiterfassung (V: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")");
                sw.Write    ("**************************************************************************************");
            }
            //
            ZeitManagement.stopUhrStandBeiProgrammStart = "00:" + ProgrammSettingsEinlesen.sKorrekturAZAnfang;
            //
            //Der Header wurde geschrieben, also wird die Steuervariable wieder auf <false> gesetzt.
            ZeitManagement.isHeaderSchreiben = false;
        }
    }
}
