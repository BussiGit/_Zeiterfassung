using System;


namespace Zeiterfassung
{
    public class StopUhrStandBerechnen
    {
        public static void stopUhrStandBerechnen()
        {
            //Variablen
            DateTime stopZeit;

/*          Stoppuhr neu berechnen                                                                                                          */
            //
            //[Stopzeit - Startzeit] = zeitDiff + Stoppuhrstand zu Programmstart = neuer Stoppuhrstand - Pausenzeiten   = neuer Stoppuhrstand
            //
            if (ZeitManagement.fromWhere != "Stop")                                             
                stopZeit = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));        //Kommt aus der Zeitkorrektur
            //
            else
                stopZeit = ZeitManagement.arbeitsZeitStopTime;                                      //kommt von "STOP"
            //-
            DateTime startZeit          = ZeitManagement.arbeitsZeitStartTime;
            //=
            TimeSpan zeitDiff           = stopZeit - startZeit;
            //+
            TimeSpan alterStopuhrstand  = TimeSpan.Parse(ZeitManagement.stopUhrStandBeiProgrammStart); //Stoppuhrstand zu Beginn
            //=
            TimeSpan neuerStopuhrstand  = zeitDiff + alterStopuhrstand;
            //-
            //Pausenlänge
            //=
            //neuer Stopuhrstand


            //Pausenzeiten berechnen
            //
            for (int i = 0; i < ZeitManagement.zaehlerUpDown; i++)
            {
                DateTime pauseStop      = DateTime.Parse(frm_Zeiterfassung.pauseStopZeitListe[i].ToString("dd.MM.yyyy HH:mm:00"));
                DateTime pauseStart     = DateTime.Parse(frm_Zeiterfassung.pauseStartZeitListe[i].ToString("dd.MM.yyyy HH:mm:00"));

                //Länge der jeweiligen Pause berechnen
                TimeSpan pausenLaenge   = pauseStop - pauseStart;
                //
                //Stopuhrstand um jeweilige Pausenlänge reduzieren
                neuerStopuhrstand       = neuerStopuhrstand - pausenLaenge;
            }
            //
            //Stunden- und Minuten des neuen Stoppuhrstandes separieren und in entsprechende Variable schreiben
            ZeitManagement.lastHH = Convert.ToByte(neuerStopuhrstand.Hours);
            ZeitManagement.lastMM = Convert.ToByte(neuerStopuhrstand.Minutes);
        }
    }
}
