using System;
using System.Xml.Linq;


namespace Zeiterfassung
{
    public static class ProgrammSettingsEinlesen
    {
        //Setting-Variablen
        ///Optionsfenstervariablen
        public static string    sFirma;
        public static string    sName;
        public static byte      sKorrekturAZAnfang;
        public static double    sTransparenz;
        public static bool      isSVordergrund;
        public static bool      isSAutostart;
        
        //Weitere Setting-Variablen
        public static double    sPositionLeft;
        public static double    sPositionTop;
        public static string    sLetzterStopuhrstand;
        public static string    sAlterName;


        public static void programmSettingsEinlesen()
        {
/*          AKTUELLE EINSTELLUNGEN lesen                                                                            */
            //
            //XML-Datei einlesen...
            XElement settings = XElement.Load("ProgrammSettings.xml");
            //
            //Variablen mit den Setting-Werten belegen
            sFirma                  = settings.Element("Firma").Value;
            sName                   = settings.Element("Name").Value;
            sKorrekturAZAnfang      = Convert.ToByte(settings.Element("KorrekturAZAnfang").Value);
            sTransparenz            = Convert.ToDouble(settings.Element("Transparenz").Value);
            isSVordergrund          = Convert.ToBoolean(settings.Element("Vordergrund").Value);
            isSAutostart            = Convert.ToBoolean(settings.Element("Autostart").Value);
            //
            sAlterName              = settings.Element("AlterName").Value;
            try
            { sLetzterStopuhrstand  = settings.Element("LetzterStopuhrstand").Value; }
            catch
            { sLetzterStopuhrstand  = "00:00"; }
            //
            sPositionLeft           = Convert.ToDouble(settings.Element("PositionLeft").Value);
            sPositionTop            = Convert.ToDouble(settings.Element("PositionTop").Value);
            }
    }
}
