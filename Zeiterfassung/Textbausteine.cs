


namespace Zeiterfassung
{
    static class Textbausteine
    {
        //Datenteil der Klasse Textbaustein
        //Strings für logText
        public static string pauseStop                     = "Pausenende.......................................";
        public static string pauseAnfang                   = "Pausenbeginn.....................................";
        //
        public static string arbeitEnde                    = "Arbeitsende......................................";
        public static string arbeitsEndeDaBenutzerwechsel  = "Programmende, da Benutzerwechsel.................";
        public static string pausenEndeDaBenutzerwechsel   = "Programmende, da Benutzerwechsel.................";
        public static string arbeitEndeInPause             = "Arbeit nach Pausenende nicht mehr aufgenommen.";
        //
        public static string reset                         = "AZ auf 00:00 gestellt (Reset)....................";
        public static string resetInPause                  = "AZ innerh. der Pause auf 00:00 gestellt (Reset)..";
        public static string arbeitsEndeDaReset            = "Arbeitsende, da Reset............................";
        //
        public static string arbeitAutoStart               = "Arbeitsbeginn (Autostart)........................";
        public static string arbeitStart                   = "Arbeitsbeginn....................................";
        public static string arbeitWiederaufnahme          = "\rWiederaufnahme der Arbeit........................";
        public static string arbeitAutoWiederaufnahme      = "\rWiederaufnahme der Arbeit (Autostart)............";
        public static string arbeitWiederaufnahmeNachOpt   = "\r\rWiederaufnahme der Arbeit nach Optionseingabe....";
        //
        public static string fehler                        = "Fehler: AZ auf letzten bekannten Wert gesetzt....";
        //
        public static string manuellStart                  = "                *";
        public static string manuellPause                  = "  *";

        //Programm
        public static string programmName                  = "Demo-Zeiterfassung";
        public static string version                       = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string impressumKKK                  = "rempe@kaiser-karl-klinik.de";
        public static string impressumSonstige             = "markus.rempe@e.mail.de";
        public static string dateiName                     = "ProgrammSettings.xml";

        //Textbausteine
        public static string demoVersionAbgelaufen            = "Demoversion abgelaufen\r\r" +
                                                                "Der Zeitraum für das Testen der Demoversion ist abgelaufen. Wenn das Programm gefällt, bitte die Vollversion erwerben!";

        public static string fehlerhafteBeendigung            = "Fehlerhafte Beendigung\r\r" +
                                                                "Das Programm wurde nach dem letzten Start nicht ordnungsgemäß beendet!\r" +
                                                                "Die Uhr wird auf den letzten bekannten Stand gesetzt.";

        public static string letzterStandNichtGefunden        = "Kein letzter Stand gefunden\r\r" +
                                                                "Es konnte kein letzter Stopuhrstand gefunden werden.\r" +
                                                                "Die Uhr wird nun auf <<00:00>> zurück gesetzt.";

        public static string benutzernameGeaendert            = "Optionen\r\r" +
                                                                "Da der Benutzername geändert wurde, wird das Programm nun automatisch geschlossen.\r" +
                                                                "Es kann direkt wieder gestartet werden. Der neue Benutzer wird dann automatisch angelegt.";

        public static string resetAbfrage                     = "Reset\r\r" +
                                                                "Soll die Arbeitszeiterfassung wirklich auf <<00:00>> zurückgesetzt werden?\r" +
                                                                "In diesem Fall wird das Programm automatisch beendet.\r\r" +
                                                                "Danach kann es wieder neu gestartet werden!";

        public static string benutzerFehlt                    = "Benutzer fehlt\r\r" +
                                                                "Es wurde noch kein Benutzer angelegt.\r" +
                                                                "Bitte die Felder im Optionsfenster ausfüllen! \r" +
                                                                "Anschließend wird das Programm automatisch beendet.\r" +
                                                                "Danach kann es wieder neu gestartet werden!";

        public static string hinweisNeustart                  = "Neustart notwendig\r\r" +
                                                                "Das Programm wird nun beendet.\r" +
                                                                "Anschließend bitte wieder neu starten!";

        public static string benutzerFehltImmerNoch           = "Benutzer fehlt immer noch\r\r" +
                                                                "Es wurde kein Benutzer eingegeben.\r" +
                                                                "Das Programm wird nun beendet!";

        public static string logFileDateiFehler               = "Dateifehler\r\r" +
                                                                "Das LogFile konnte nicht gelesen werden. Das Programm wird nun geschlossen.\r" +
                                                                "Bitte anschließend wieder starten. Wenn der Fehler erneut auftritt, bitte das\r" +
                                                                "aktuelle Logfile löschen und erneut versuchen.";

        public static string optionenSpeicherFehler           = "\r\rFehler \r\r" +
                                                                "Die Optionseinstellung konnte nicht gespeichert werden!\r" +
                                                                "Ggf. warten und noch einmal versuchen!";

        public static string konfigurationsDateiFehlerhaft    = "Konfigurationsdatei fehlerhaft\r\r" +
                                                                "Es ist ein Fehler aufgetreten. Wahrscheinlich ist die Konfigurationsdatei defekt.\r" +
                                                                "Das Programm versucht nun, diese zu löschen und sich danach selbst zu schließen.\r\r" +
                                                                "Anschließend kann das Programm neu gestartet und der Benutzer neu angelegt werden.";

        public static string konfigurationsDateiDelete        = "Konfigurationsdatei fehlerhaft\r\r" +
                                                                "Die Datei <<ProgrammSettings.xml>> konnte nicht wiederhergestellt werden. \r\r" +
                                                                "Diese muss manuell gelöscht werden. Sie befindet sich im Verzeichnis\r\r" +
                                                                System.Windows.Forms.Application.StartupPath + "\r\r" +
                                                                "Anschließend kann das Programm neu gestartet und der Benutzer wieder angelegt werden.";

        public static string zeitAngabeFehlerhaft             = "Zeitangabe fehlerhaft\r\r" +
                                                                "Die Eintragung für die Startzeit oder die Pausenzeit scheint fehlerhaft zu sein. \r" +
                                                                "Bitte korrekte Uhrzeiten eingeben!";

        public static string pausenZeitVorStartzeit           = "Zeitangabe fehlerhaft\r\r" +
                                                                "Die PAUSENzeit liegt vor der ArbeitsBEGINN. \r" +
                                                                "Bitte korrigieren!";
        
        public static string pausenEndeVorStartzeit           = "Zeitangabe fehlerhaft\r\r" +
                                                                "Das PausenENDE liegt vor dem PausenSTART. \r" +
                                                                "Bitte korrigieren!";

        public static string kommentarFehlt                   = "Begründung fehlt\r\r" +
                                                                "Es fehlt eine Begründung für die Veränderung der Zeiten. \r" +
                                                                "Bitte eingeben!";
    }
}

