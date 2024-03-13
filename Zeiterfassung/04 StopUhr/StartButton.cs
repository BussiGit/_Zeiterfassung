using System;


namespace Zeiterfassung
{
    public class StartButton
    {
        public static void startButton()
        {
/*09        START (erster Start des Tages / erneuter Start / Ende der Pause)                                                                                           */
            //
            //Wocher kommt der Aufruf?
            //
/*09ab      Programmneustart, es gab aber schon einen Programmdurchlauf an diesem Tag (manueller Start).                                                                */
            if (ZeitManagement.fromWhere                == "AltStart")
            {
                ZeitManagement.arbeitsZeitStartTime     = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                ZeitManagement.arbeitsZeitStartText     = Textbausteine.arbeitWiederaufnahme;
            }
            //
/*09bb      Programmneustart, es noch keinen Programmdurchlauf an diesem Tag (manueller Start).                                                                         */
            else if (ZeitManagement.fromWhere           == "NeuStart")
            {
                ZeitManagement.arbeitsZeitStartTime     = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00")).AddMinutes((ProgrammSettingsEinlesen.sKorrekturAZAnfang) * -1);
                ZeitManagement.arbeitsZeitStartText     = Textbausteine.arbeitStart;
            }
            //
/* 09d      Arbeit Wiederaufnahme nach Optionen ######### => kann gelöscht werden?? Nach Optionen wird Programm geschlossen                                                                                                                         */
            else if (ZeitManagement.fromWhere           == "Optionen")
            {
                ZeitManagement.arbeitsZeitStartTime     = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                ZeitManagement.arbeitsZeitStartText     = Textbausteine.arbeitWiederaufnahmeNachOpt;
            }
            //
/* 09e      in allen anderen Fällen:                                                                                                                                    */
            else
            {
                //Zeit hinzufügen
                frm_Zeiterfassung.pauseStopZeitListe.Add(Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00")));
                ZeitManagement.pauseStopText            =  Textbausteine.pauseStop;
            }
        }
    }
}
