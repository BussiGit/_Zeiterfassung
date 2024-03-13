using System;


namespace Zeiterfassung
{
    static class ZeitManagement
    {
        public static byte      lastHH;
        public static byte      lastMM;

        public static string    aktuellerStopUhrStand = string.Empty;
        public static string    stopUhrStandBeiProgrammStart;

        public static string    arbeitsZeitStartText;
        public static DateTime  arbeitsZeitStartTime;

        public static DateTime  pauseStartTime;
        public static DateTime  pauseStopTime;

        public static string    arbeitsZeitStopText;
        public static DateTime  arbeitsZeitStopTime;

        public static string    fromWhere;
        //public static bool      neuenLogfileAnlegen;

        public static bool      isFehler;

        public static string    manuellStart = "";
        public static string    manuellPause = "";

        public static bool      isErsterAufrufAmTag;
        public static bool      isTimerStartStop;
        public static bool      isNeuStart;
        public static bool      isHeaderSchreiben;

        public static bool      isUhrStarten = false;
        
        public static string    pauseStartText;
        public static string    pauseStopText;

        public static string    completePath;

        public static int       pauseZahl =-1;

        public static int       zaehlerUpDown;

        public static bool      isKommentarNoetig = false;
        public static string    kommentar;

        public static string    timeOld;
    }
}
