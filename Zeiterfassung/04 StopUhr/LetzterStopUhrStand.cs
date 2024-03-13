using System;
using System.IO;
using System.Linq;


namespace Zeiterfassung
{
    public class LetzterStopUhrStandLesen
    {
        public static void letzterStopUhrStandLesen()

        {
            //Da ein LogFile besteht, gibt es auch einen Stopuhrstand. Dieser muss ausgelesen
            //und in die Stopuhrdatei eingetragen werden. Letzte Zeile auslesen (dort steht der aktuelle Stand der Stopuhr)
            string lastLine = File.ReadLines(ZeitManagement.completePath).Last();
            try
            {   //Stunden und Minuten einlesen und in Zahl konvertieren
                byte lastHH = Convert.ToByte(lastLine.Substring(0, 2));        ///zwei Zeichen ab Position 0
                byte lastMM = Convert.ToByte(lastLine.Substring(3, 2));        ///zwei Zeichen ab Position 3

                //Variablen übergeben
                ZeitManagement.lastHH = lastHH;
                ZeitManagement.lastMM = lastMM;
            }
            //
            //Beginnt die letze Zeile nicht mit Stunden:Minuten, entsteht ein Fehler. Dieser wird hier abgefangen.
            catch
            {
                //Letzten Stopuhrstand aus der Konfigurationsdatei auslesen
                try
                {
                    //Stunden und Minuten in Zahl konvertieren
                    byte lastHH = Convert.ToByte(ProgrammSettingsEinlesen.sLetzterStopuhrstand.Substring(0, 2));
                    byte lastMM = Convert.ToByte(ProgrammSettingsEinlesen.sLetzterStopuhrstand.Substring(3, 2));
                    
                    //Variablen übergeben
                    ZeitManagement.lastHH = lastHH;
                    ZeitManagement.lastMM = lastMM;
                }
                //
                catch
                {
                    //Liefert auch der Letzte Stopuhrstand keine verwertbare Zeit, wird sie auf 00:00 zurück gesetzt
                    byte lastHH = 00;
                    byte lastMM = 00;

                    //Variablen übergeben
                    ZeitManagement.lastHH = lastHH;
                    ZeitManagement.lastMM = lastMM;

                    //Hinweisfenster
                    string titel = Textbausteine.programmName;
                    string nachricht = Textbausteine.letzterStandNichtGefunden;
                    MessageBoxes.ErrorBox(titel, nachricht);
                }

                //Fehler in Logfile schreiben
                ZeitManagement.isFehler = true;
                //###############ZeitManagement.logText = Textbausteine.fehler;                                             //<logText> mit "Start" belegen
                //###############LogFile.WriteInLogFile(ZeitManagement.logText, aktuellerStopUhrStand, timerStartStop);    //Methode zum Eintrag in das Logfile starten
            }
            //Stopuhrstand bei Programmstart definieren
            ZeitManagement.stopUhrStandBeiProgrammStart = ZeitManagement.lastHH + ":" + ZeitManagement.lastMM + ":00";
        }
    }
}
