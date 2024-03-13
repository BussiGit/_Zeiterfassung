using System;


namespace Zeiterfassung
{
    public class DemoVersionAbgelaufen
    {
        public static void demoVersionAbgelaufen()
        {
/*          DEMOVERSION? => Überprüfen, ob Demoversion abgelaufen ist
            Wenn die Demo-Version abgelaufen ist, ist das Programm hier zu Ende.                                                                */
            //
            //Aktuelles Datum
            DateTime altuelleZeit = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
            //
            //Ablaufdatum der Demo-Version (YYYY, MM, DD)
            DateTime ablaufDatum = new DateTime(2050, 12, 31);  //Hier das Datum für den Ablauf der Demoversion angeben.

            //Liegt das aktuelle Datum hinter dem Ablaufdatum der Demoversion?                                                                  */
            if (altuelleZeit > ablaufDatum)
            {
                //Dann wird ein Hinweis ausgegeben und das Programm beendet;
                string titel       = Textbausteine.programmName;
                string nachricht   = Textbausteine.demoVersionAbgelaufen;
                MessageBoxes.WarningBox(titel, nachricht);

                //Alles schließen
                Environment.Exit(0);
            }
        }   
    }
}
