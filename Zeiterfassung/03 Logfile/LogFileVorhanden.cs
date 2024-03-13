using System;
using System.IO;
using System.Xml.Linq;


namespace Zeiterfassung
{
    public class LogFileVorhanden
    {
        public static void logFileVorhanden() 
        {
/*          LOGFILE-DATEI => Überprüfen, ob eine aktuelle LogFile-Datei vorhanden ist.
            =>  Damit wird überprüft, ob die Datei am heutigen Tag bereits gestartet wurde.
                Entsprechend werden die Hilfsvariablen gesetzt, um das Programm weiter zu steuern.                                                                               */
            //
            //Um die Datei öffnen zu können, muss zuerst der aktuelle Dateiname generiert und im ZeitManagement abgelegt werden.
            string fileName                 = DateTime.Now.ToString("yyyy-MM-dd") + " - " + ProgrammSettingsEinlesen.sName + ".txt";
            string completePath             = Directory.GetCurrentDirectory() + @"\LogFiles\" + fileName;
            ZeitManagement.completePath     = completePath;
            ///
            if (File.Exists(ZeitManagement.completePath))
            {
                //Da ein Logfile besteht, ist es eine <Arbeit-Wiederaufnahme>.
                //Der Header besteht bereits (Hilfsvariablen setzen).
                ZeitManagement.isErsterAufrufAmTag        = false;
                ZeitManagement.isHeaderSchreiben          = false;
            }
            //
            else
            {
                //Da kein Logfile besteht, ist es eine Arbeitsaufnahme an diesem Tag. 
                //Der Header muss noch geschrieben werden (Hilfsvariablen setzen).
                ZeitManagement.isErsterAufrufAmTag        = true;
                ZeitManagement.isHeaderSchreiben          = true;
                //
                //Letzten Stopuhrstand in den ProgrammSettings.xml auf OO:00 setzen
                //(für den Fall, dass das Programm hier schon nicht durchläuft...)
                XElement settings = XElement.Load("ProgrammSettings.xml");
                //
                settings.SetElementValue("LetzterStopuhrstand", "00:00");
                //
                settings.Save("ProgrammSettings.xml");
            }
        }
    }
}
