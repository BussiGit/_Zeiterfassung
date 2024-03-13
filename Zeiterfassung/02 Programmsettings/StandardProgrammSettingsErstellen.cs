using System.Xml.Linq;


namespace Zeiterfassung
{
    public static class StandardProgrammSettingsErstellen
    {
        public static void standardProgrammSettingsErstellen()
        {
/*          STANDARDWERTE in PROGRAMMSETTINGS.XML schreiben.                                */
            //
            //XML-Datei mit Standardwerten anlegen...
            XDocument settings = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                                new XElement("ProgrammSettings",
                                                    new XElement("Firma",               ""),
                                                    new XElement("Name",                ""),
                                                    new XElement("KorrekturAZAnfang",   "0"),
                                                    new XElement("Transparenz",         "80"),
                                                    new XElement("Vordergrund",         "true"),
                                                    new XElement("Autostart",           "true"),

                                                    new XElement("AlterName",           ""),
                                                    new XElement("LetzterStopuhrstand", "00:00"),

                                                    new XElement("PositionTop",         "0"),
                                                    new XElement("PositionLeft",        "0")));

            //...und abspeichern.
            settings.Save("ProgrammSettings.xml");
        }
    }
}
