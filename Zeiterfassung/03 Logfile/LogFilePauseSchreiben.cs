using System;
using System.IO;

namespace Zeiterfassung._03_Logfile
{
    public class LogFilePauseSchreiben
    {
        public static void logFilePauseSchreiben()
        {
/*          LOGFILE BESCHREIBEN                                                                      */
            //
            //Überprüfen, ob Pause gemacht wurde
            if (ZeitManagement.pauseStartText != null)
            {
                //StreamWriter Initialisieren                
                using (StreamWriter sw = new StreamWriter(ZeitManagement.completePath, true))
                {
                    //Schleife: Anzahl der gemachten Pausen.
                    for (int i = 0; i < ZeitManagement.pauseZahl + 1; i++)
                    {
/*                      PAUSENSTART eintragen                                                                     */
                        //
                        //Zunächst eine Leerzeile einfügen
                        sw.WriteLine("");
                        //
                        ///logText in die Datei schreiben (erster Teil ohne Uhrzeit)
                        sw.Write(ZeitManagement.pauseStartText);
                        //
                        //Uhrzeit anhängen
                        sw.WriteLine("  " + frm_Zeiterfassung.pauseStartZeitListe[i].ToString("HH:mm") + " Uhr");

/*                      PAUSENENDE eintragen                                                                      */
                        //
                        //logText in die Datei schreiben (erster Teil ohne Uhrzeit und Pausenlänge)
                        sw.Write(ZeitManagement.pauseStopText);
                        //
                        //UHRZEIT anhängen
                        sw.Write("  " + frm_Zeiterfassung.pauseStopZeitListe[i].ToString("HH:mm") + " Uhr");
                        ///
                        ///PAUSENLÄNGE anhängen
                        ///Zeitpunkte in String konvertieren und Sekunden abschneiden.
                        string pauseAnfang  = frm_Zeiterfassung.pauseStartZeitListe[i].ToString("dd.MM.yyyy HH:mm:00");
                        string pauseStop    = frm_Zeiterfassung.pauseStopZeitListe[i].ToString("dd.MM.yyyy HH:mm:00");
                        string manuell      = frm_Zeiterfassung.manuellListe[i];    //Kennzeichnung, wenn manuell korrigiert wurde
                        ///
                        ///Bereinigten String zurückk in Datumformat konvertieren.
                        ZeitManagement.pauseStartTime   = DateTime.Parse(pauseAnfang);
                        ZeitManagement.pauseStopTime    = DateTime.Parse(pauseStop);
                        ///
                        ///Pausenlänge berechnen...
                        TimeSpan pausenLaenge = ZeitManagement.pauseStopTime - ZeitManagement.pauseStartTime;
                        ///
                        ///...und eintragen.
                        if (pausenLaenge.Hours >= 0 && pausenLaenge.Minutes >= 0)
                        {
                            //DateTime PausenLaenge = Convert.ToDateTime(pausenLaenge);
                            sw.Write("  Länge: " + String.Format("{0:00}:{1:00}", pausenLaenge.Hours, pausenLaenge.Minutes) + manuell);
                        }else
                            sw.Write("  Fehler Pausenberechnung");
                        //
                        //noch eine Leerzeile anhängen
                        sw.WriteLine("");
                    }
                }
            }
        }
    }
}
