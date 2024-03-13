using System;


namespace Zeiterfassung
{
    internal class AutoStart
    {
        public static void autoStart()
        {
/* 07       AUTOSTART                                                                                   */
            //
            //07a  Beim WIEDERHOLTEN Aufruf
            if (ZeitManagement.isErsterAufrufAmTag        == false)
            {
                ///07aa Autostart = true
                if (ProgrammSettingsEinlesen.isSAutostart == true)
                {
                    ZeitManagement.fromWhere            = "AltAutoStart";
                    ZeitManagement.arbeitsZeitStartText = Textbausteine.arbeitAutoWiederaufnahme;
                    ZeitManagement.arbeitsZeitStartTime = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00"));
                    ZeitManagement.isUhrStarten           = true;
                }
                ///
                ///07ab Autostart = false
                else
                {
                    ZeitManagement.fromWhere            = "AltStart";
                    ZeitManagement.isUhrStarten           = false;
                }
            }
            //
            //07b Beim ERSTEN Aufruf
            else
            {
                ///07ba Autostart = true
                if (ProgrammSettingsEinlesen.isSAutostart == true)
                {
                    ZeitManagement.fromWhere            = "NeuAutoStart";
                    ZeitManagement.arbeitsZeitStartText = Textbausteine.arbeitAutoStart;
                    ZeitManagement.arbeitsZeitStartTime = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:mm:00")).AddMinutes((ProgrammSettingsEinlesen.sKorrekturAZAnfang) * -1);
                    ZeitManagement.isUhrStarten         = true;
                }
                ///
                ///07bb Autostart = false
                else
                {
                    ZeitManagement.fromWhere            = "NeuStart";
                    ZeitManagement.isUhrStarten         = false;
                }
            }
        }
    }
}
