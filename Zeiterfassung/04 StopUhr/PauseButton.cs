using System;


namespace Zeiterfassung
{
    public class PauseButton
    {
        public static void pauseButton()
        {
/*10        PAUSE                                                                                                                       */
            //
            //Wocher kommt der Aufruf?
            ZeitManagement.fromWhere            = "Pause";

            //Zeit hinzufügen und Manuell-Liste füllen

            DateTime aktuelleZeit = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
            
            frm_Zeiterfassung.pauseStartZeitListe.Add(aktuelleZeit);
            frm_Zeiterfassung.manuellListe.Add("");                     //Für jeden Eintrag in der pauseStartZeitListe wird ein entsprechener manuellListe-Eintrag generiert (dieser wird auf "" gesetzt).
            
            ZeitManagement.pauseStartText   = Textbausteine.pauseAnfang;
            ZeitManagement.pauseZahl        += 1;

            //Hilfsvarialben setzen
            ZeitManagement.isTimerStartStop   = false;                    //Stopuhr als angehalten markieren
        }
    }
}
